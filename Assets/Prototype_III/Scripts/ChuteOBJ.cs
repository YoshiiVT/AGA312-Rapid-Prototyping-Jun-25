using UnityEngine;

public class ChuteOBJ : MonoBehaviour
{
    [SerializeField] private ChuteObjData chuteObjData;
    [SerializeField] private Color renderColour;
    public ChuteObject objectType;
    public void Initialize(ChuteObjData _chuteObjData)
    {
        chuteObjData = _chuteObjData;
        renderColour = chuteObjData.meshColour;
        gameObject.GetComponent<Renderer>().material.color = renderColour;
        objectType = chuteObjData.chuteObj;
    }
}
