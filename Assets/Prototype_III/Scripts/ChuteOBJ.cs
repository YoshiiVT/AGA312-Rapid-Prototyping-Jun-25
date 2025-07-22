using UnityEngine;

public class ChuteOBJ : MonoBehaviour
{
    [SerializeField] private ChuteObjData chuteObjData;
    [SerializeField] private Mesh meshPrefab;
    [SerializeField] private Material materialPrefab;
    public FlavourID objectType;
    public void Initialize(ChuteObjData _chuteObjData)
    {
        chuteObjData = _chuteObjData;
        //materialPrefab = chuteObjData.materialPrefab;
        //meshPrefab = chuteObjData.meshPrefab;
        gameObject.GetComponent<Renderer>().material = materialPrefab;
        gameObject.GetComponent<MeshFilter>().mesh = meshPrefab;
        objectType = chuteObjData.flavourID;
    }
}
