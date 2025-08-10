using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace PROTOTYPE_2
{
    public class PlayerController : GameBehaviour
    {
        [Header("Managers")]
        [SerializeField] private SongPlayer songPlayer;
        [SerializeField] private GameManager gameManager;

        [Header("PlayerReferences")]
        [SerializeField] private SpriteRenderer splatSprite;
        [SerializeField] private GameObject paintGun;

        [SerializeField] private float splatLifetime;
        private Coroutine fadeCoroutine;


        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) { HitNote(); }
        }

        private void HitNote()
        {
            NoteBehaviour noteHit = songPlayer.CheckCenterNote();

            if (noteHit != null)
            {
                if (noteHit.BeenHit()) { return; }

                // Define original scale
                Vector3 originalScale = new Vector3(2.5f, 2.5f, 1f);

                // Kill any ongoing tweens
                paintGun.transform.DOKill();

                // Ensure it starts at its original scale
                paintGun.transform.localScale = originalScale;

                // Create the squeeze scale (slightly squashed horizontally, stretched vertically)
                Vector3 squeezedScale = new Vector3(2.0f, 3.0f, 1f);

                // Animate the squeeze and return
                paintGun.transform
                    .DOScale(squeezedScale, 0.1f)
                    .SetEase(Ease.OutQuad)
                    .OnComplete(() =>
                    {
                        paintGun.transform
                            .DOScale(originalScale, 0.1f)
                            .SetEase(Ease.InQuad);
                    });

                noteHit.NoteHit();
            }
        }


        public void PlayerBeenHit()
        {
            splatSprite.color = ColorX.GetRandomColour();

            splatSprite.gameObject.SetActive(true);

            // Reset alpha to fully visible
            SetAlpha(1f);

            // Restart the fade coroutine
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
            }
            fadeCoroutine = StartCoroutine(FadeOutCoroutine());
        }

        private IEnumerator FadeOutCoroutine()
        {
            float elapsed = 0f;
            while (elapsed < splatLifetime)
            {
                float alpha = Mathf.Lerp(1f, 0f, elapsed / splatLifetime);
                SetAlpha(alpha);
                elapsed += Time.deltaTime;
                yield return null;
            }

            SetAlpha(0f);
            splatSprite.gameObject.SetActive(false);
            fadeCoroutine = null;
        }

        private void SetAlpha(float alpha)
        {
            Color color = splatSprite.color;
            color.a = alpha;
            splatSprite.color = color;
        }
    }

}
