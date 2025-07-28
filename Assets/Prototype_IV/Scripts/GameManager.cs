using UnityEngine;
using TMPro;

namespace PROTOTYPE_4
{
    public class GameManager : GameBehaviour
    {
        [SerializeField, ReadOnly] private int points;
        [SerializeField] private TMP_Text pointText;

        public void Start()
        {
            UpdatePoints();
        }

        public void addpoint()
        {
            points++;
            print(points);
            UpdatePoints();
        }

        private void UpdatePoints()
        {
            pointText.text = "Score : " + points;
        }
    }
}

