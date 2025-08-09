using DG.Tweening;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] int moveDistance;

    public void HitNote(float _SPB)
    {
        gameObject.transform.DOMoveY(moveDistance, _SPB).OnComplete(() => gameObject.transform.DOMoveY(-moveDistance, _SPB));
    }
}
