using UnityEngine;

public class Playground : GameBehaviour
{
    public GameObject player;
    void Start()
    {
        ObjectX.ScaleObjectToZero(player);
        ExecuteAfterSeconds(5, () => { SetupPlayer(); });
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) player.GetComponent<Renderer>().material.color = ColorX.GetRandomColour();
    }

    private void SetupPlayer()
    {
        player.GetComponent<Renderer>().material.color = ColorX.GetRandomColour();
        ObjectX.ScaleObjectToValue(player, 3);
    }
}
