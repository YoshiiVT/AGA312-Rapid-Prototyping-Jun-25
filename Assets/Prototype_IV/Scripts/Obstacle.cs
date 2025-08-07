using PROTOTYPE_4;
using System.Collections;
using UnityEngine;

public class Obstacle : GameBehaviour
{
    [Header("Manager")]
    [SerializeField, ReadOnly] private GameManager gameManager;

    [Header("Attributes")]
    [SerializeField, ReadOnly] private float speed;
    [SerializeField, ReadOnly] private float lifespan;

    private void Start()
    {
        GameObject gameManagerobj = GameObject.Find("GameManager");
        gameManager = gameManagerobj.GetComponent<GameManager>();
        if (gameManager == null) { Debug.LogError("GAMEMANAGER NOT FOUND!!!"); }

        speed = gameManager.speed;
        lifespan = gameManager.obstacleLifespan;

        StartCoroutine(LifeSpan());
    }

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }

    private IEnumerator LifeSpan()
    {
        yield return new WaitForSeconds(lifespan);
        Destroy(gameObject);
    }
}
