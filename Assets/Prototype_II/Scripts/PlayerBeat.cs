using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerBeat : MonoBehaviour
{
    [SerializeField] private GraphicRaycaster raycaster;
    [SerializeField, ReadOnly] private PointerEventData pointerEventData;
    [SerializeField] private EventSystem eventSystem;

    void Start()
    {
        raycaster = Object.FindFirstObjectByType<GraphicRaycaster>();
        eventSystem = Object.FindFirstObjectByType<EventSystem>();

        if (raycaster == null)
            Debug.LogError("GraphicRaycaster not found! Make sure your Canvas has one.");
        if (eventSystem == null)
            Debug.LogError("EventSystem not found! Add one to your scene.");

        //Im not good at raycasting.... Thanks ChatGPT
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckColorAtCenter();
        }
    }

    void CheckColorAtCenter()
    {
        // Create pointer data for the center of the screen
        pointerEventData = new PointerEventData(eventSystem)
        {
            position = new Vector2(Screen.width / 2, Screen.height / 2)
        };

        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointerEventData, results);

        foreach (RaycastResult result in results)
        {
            // Check tag or attached script to identify panel color
            GameObject hitObj = result.gameObject;

            switch (hitObj.tag)
            {
                case "Red":
                    Debug.Log("UI Panel: RED!");
                    return;
                case "Yellow":
                    Debug.Log("UI Panel: YELLOW!");
                    return;
                case "Green":
                    Debug.Log("UI Panel: GREEN!");
                    return;
                default:
                    Debug.Log("UI Panel: Unknown or untagged object.");
                    return;
            }
        }

        Debug.Log("No UI panel at center.");
    }
}