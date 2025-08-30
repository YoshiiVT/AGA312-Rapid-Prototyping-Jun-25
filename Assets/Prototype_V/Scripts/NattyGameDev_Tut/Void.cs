using UnityEngine;
using UnityEngine.SceneManagement;

namespace PROTOTYPE_5
{
    public class Void : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        }
    }
}

