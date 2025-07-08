using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PROTOTYPE_2
{
    public class PlayerBeat : MonoBehaviour
    {
        [SerializeField] private GraphicRaycaster raycaster;
        [SerializeField, ReadOnly] private PointerEventData pointerEventData;
        [SerializeField] private EventSystem eventSystem;
        [SerializeField] private SongPlayer _SP;

        [SerializeField, ReadOnly] private bool canHit = false;
        [SerializeField] private Image buttonImage;

        void Start()
        {
            raycaster = Object.FindFirstObjectByType<GraphicRaycaster>();
            eventSystem = Object.FindFirstObjectByType<EventSystem>();

            if (raycaster == null)
                Debug.LogError("GraphicRaycaster not found! Make sure your Canvas has one.");
            if (eventSystem == null)
                Debug.LogError("EventSystem not found! Add one to your scene.");
        }

        void Update()
        {
            if (canHit)
            {
                buttonImage.color = Color.white;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    HitNote();
                }
            }
            else { buttonImage.color = Color.gray; }
        }

        private void HitNote()
        {
            Debug.Log("Hitting Centre");

            pointerEventData = new PointerEventData(eventSystem)
            {
                position = new Vector2(Screen.width / 2, Screen.height / 2)
            };

            List<RaycastResult> results = new List<RaycastResult>();
            raycaster.Raycast(pointerEventData, results);

            foreach (RaycastResult result in results)
            {
                GameObject hitObj = result.gameObject;
                Debug.Log("Checking " + hitObj);

                BeatBehaviour beat = hitObj.GetComponentInParent<BeatBehaviour>();
                if (beat != null)
                {
                    if (beat.IsPlayerBeat())
                    {
                        Debug.LogWarning("Has Hit player note");
                        _SP.DestroyNote(beat.GetBeatOrder());
                        return;
                    }
                    else { Debug.Log("Hit was not playable note"); return; }
                }
                Debug.LogError("Note component returned null"); return;
            }
            Debug.Log("No notes detected");
        }

        public bool CentreNote()
        {
            //Debug.Log("Checking Centre");
            
            pointerEventData = new PointerEventData(eventSystem)
            {
                position = new Vector2(Screen.width / 2, Screen.height / 2)
            };

            List<RaycastResult> results = new List<RaycastResult>();
            raycaster.Raycast(pointerEventData, results);

            foreach (RaycastResult result in results)
            {
                GameObject hitObj = result.gameObject;
                //Debug.Log("Checking " + hitObj);

                BeatBehaviour beat = hitObj.GetComponentInParent<BeatBehaviour>();
                if (beat != null) 
                {
                    if (beat.IsSpeedUp())
                    {
                        Debug.Log("IS Speed up Note");
                        _SP.IncreaseBPM();
                    }
                    if (beat.IsSpeedDown())
                    {
                        Debug.Log("IS Down up Note");
                        _SP.DecreaseBPM();
                    }
                    if (beat.IsPlayerBeat()) 
                    {
                        //Debug.LogWarning("Is player note");
                        return true;
                    }
                    else { /*Debug.Log("Was not playable note"); */ return false; }
                }
                //Debug.LogError("Note component returned null");
                
            }
            //Debug.Log("No notes detected");
            return false;
        }
        public void ButtonToggle()
        {
            canHit = !canHit;
        }
    }
}
