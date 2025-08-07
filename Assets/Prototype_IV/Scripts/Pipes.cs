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
        if ( gameManager == null ) { Debug.LogError("GAMEMANAGER NOT FOUND!!!"); }

        // Adjust y-position
        float rndHeight = Random.Range(-1f, 2f);

        Vector3 newPos = gameObject.transform.position;
        newPos.y += rndHeight;
        gameObject.transform.position = newPos;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            gameManager.addpoint();
        }
    }
}
