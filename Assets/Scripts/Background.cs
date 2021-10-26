using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using LanguageExt;
using LanguageExt.SomeHelp;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Yuuta.Halloween
{
    public class Background : MonoBehaviour
    {
        private const float SHAKE_DURATION = 0.1f;
        
        [SerializeField, GetComponent] private AudioSource _soundAudioSource;
        [SerializeField] private Score _score;
        [SerializeField] private int _scoreAmount = -5;
        
        void Start()
        {
            this.OnMouseDownAsObservable()
                .Subscribe(_ =>
                {
                    _soundAudioSource.Play();
                    transform.DOShakePosition(SHAKE_DURATION).Play();
                    _score.AddScore(_scoreAmount);
                })
                .AddTo(this);
        }
    }
}