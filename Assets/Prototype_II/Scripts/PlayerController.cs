using PROTOTYPE_2;
using UnityEngine;

namespace PROTOTYPE_2
{
    public class PlayerController : GameBehaviour
    {
        [Header("Managers")]
        [SerializeField] private SongPlayer songPlayer;
        [SerializeField] private GameManager gameManager;


        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) { HitNote(); }
        }

        private void HitNote()
        {
            NoteBehaviour noteHit = songPlayer.CheckCenterNote();

            noteHit.NoteHit();
        }
    }

}
