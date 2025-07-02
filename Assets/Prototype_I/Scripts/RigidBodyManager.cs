using UnityEngine;

namespace PROTOTYPE_1
{
    public class RigidBodyManager : MonoBehaviour
    {

        [Header("References")]
        [SerializeField, ReadOnly] private BattleSystem _BS;

        void Awake()
        {
            GameObject gmObject = GameObject.Find("GameManager");
            _BS = gmObject.GetComponent<BattleSystem>();
        }

        /// <summary>
        /// This Method goes through all the rigidbodys in the list and see if any of them are moving.
        /// </summary>
        /// <returns></returns>
        public bool SomethingMoving()
        {
            foreach (GameObject gameObject in _BS.unitList)
            {
                Rigidbody rb = gameObject.GetComponent<Rigidbody>();
                if (rb == null) continue;

                if (RigidBodyX.IsStillMovingRB(rb) == true)
                {
                    return true; //Something is still moving
                }
            }
            return false; //All are stationary

        }
    }
}
