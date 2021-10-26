using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Yuuta.Halloween
{
    public class Score : MonoBehaviour
    {
        private int _score = 0;
        
        [SerializeField, GetComponent] private Text _text;

        public int ScoreAmount => _score;
        
        public void AddScore(int score)
        {
            _score += score;
            _text.text = $"Score: {_score}";
        }
    }
}