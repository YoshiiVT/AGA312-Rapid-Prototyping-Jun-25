using System.Collections;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifespan = 5;

    private void Start()
    {
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
