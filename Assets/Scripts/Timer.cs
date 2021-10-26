using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Yuuta.Halloween
{
    public class Timer : MonoBehaviour
    {
        private const string RESULT_SCENE_NAME = "ResultScene";
        private const int UPDATE_INTERVAL = 1;
        
        [SerializeField, GetComponent] private Text _text;
        [SerializeField] private int _seconds = 60;
        [SerializeField] private Score _score;
        
        void Start()
        {
            int currentSeconds = _seconds;
            Observable.Interval(TimeSpan.FromSeconds(UPDATE_INTERVAL))
                .Subscribe(_ =>
                {
                    currentSeconds -= UPDATE_INTERVAL;
                    _text.text = $"Time: {currentSeconds / 60}:{(currentSeconds % 60).ToString().PadLeft(2, '0')}";

                    if (currentSeconds <= 0f)
                    {
                        Saver.SetScore(_score.ScoreAmount);
                        SceneManager.LoadScene(RESULT_SCENE_NAME);
                    }
                })
                .AddTo(this);
        }
    }
}