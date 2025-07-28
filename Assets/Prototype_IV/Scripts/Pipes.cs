using PROTOTYPE_4;
using UnityEngine;

public class Pipes : GameBehaviour
{
    //Temp
    private GameManager gameManager;

    public void Start()
    {
        //Temp
        GameObject gameManagerobj = GameObject.Find("GameManager");
        gameManager = gameManagerobj.GetComponent<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            gameManager.addpoint();
        }
    }
}
