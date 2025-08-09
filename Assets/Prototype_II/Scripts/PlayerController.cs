using UnityEngine;
using System.Collections;

namespace PROTOTYPE_2
{
    public class PlayerController : GameBehaviour
    {
        [Header("Managers")]
        [SerializeField] private SongPlayer songPlayer;
        [SerializeField] private GameManager gameManager;

        [Header("PlayerReferences")]
        [SerializeField] private SpriteRenderer splatSprite;

        [SerializeField] private float splatLifetime;
        private Coroutine fadeCoroutine;


        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) { HitNote(); }
        }

        private void HitNote()
        {
            NoteBehaviour noteHit = songPlayer.CheckCenterNote();

            noteHit.NoteHit();
        }

        public void PlayerBeenHit()
        {
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
