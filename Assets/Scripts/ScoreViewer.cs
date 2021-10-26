using UnityEngine;
using UnityEngine.UI;

namespace Yuuta.Halloween
{
    public class ScoreViewer : MonoBehaviour
    {
        [SerializeField, GetComponent] private Text _scoreText;
        [SerializeField] private bool _isBest;
        
        public void Start()
        {
            _scoreText.text = 
                $"{(_isBest ? "Best Score" : "Your Score")}:{(_isBest ? Saver.GetBestScore() : Saver.GetCurrentScore())}";
        }
    }
}