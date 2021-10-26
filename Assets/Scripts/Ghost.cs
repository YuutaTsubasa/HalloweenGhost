using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using LanguageExt;
using LanguageExt.SomeHelp;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Yuuta.Halloween
{
    public class Ghost : MonoBehaviour
    {
        [SerializeField, GetComponent] private SpriteRenderer _spriteRenderer;
        [SerializeField, GetComponent] private AudioSource _soundAudioSource;
        [SerializeField, GetComponent] private BoxCollider2D _collider;
        [SerializeField] private Sprite _clickedSprite;
        [SerializeField] private float _fadeInTime = 2f;
        [SerializeField] private float _keepTimeMinDuration = 1f;
        [SerializeField] private float _keepTimeMaxDuration = 3f;
        [SerializeField] private float _fadeOutTime = 2f;
        [SerializeField] private float _clickedKeepTime = 0.5f;
        [SerializeField] private int _scoreAmount = 5;
        
        void Start()
        {
            Func<Sequence> keepAndFadeOutAnimationCreator = () => 
                DOTween.Sequence()
                    .Append(_spriteRenderer.DOFade(0, _fadeOutTime))
                    .OnComplete(() => Destroy(this.gameObject));
            
            var idleAnimation = DOTween.Sequence()
                .Append(_spriteRenderer.DOFade(1, _fadeInTime))
                .AppendInterval(Random.Range(_keepTimeMinDuration, _keepTimeMaxDuration))
                .Append(keepAndFadeOutAnimationCreator())
                .Play();

            this.OnMouseDownAsObservable()
                .Subscribe(_ =>
                {
                    _collider.enabled = false;
                    
                    idleAnimation.Kill();
                    _spriteRenderer.sprite = _clickedSprite;
                    _spriteRenderer.color = new Color(0.5f, 0.25f, 0.25f);

                    FindObjectOfType<Score>().AddScore(_scoreAmount);
                    _soundAudioSource.Play();
                    DOTween.Sequence()
                        .Append(transform.DOShakePosition(_clickedKeepTime))
                        .Append(keepAndFadeOutAnimationCreator())
                        .Play();
                })
                .AddTo(this);
        }
    }
}