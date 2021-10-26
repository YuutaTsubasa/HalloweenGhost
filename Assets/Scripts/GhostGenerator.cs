using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Yuuta.Halloween
{
    public class GhostGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject _ghostPrefab;
        [SerializeField] private Transform _ghostSetTransform;
        [SerializeField] private float _ghostGeneratedMinInterval = 0.5f;
        [SerializeField] private float _ghostGeneratedMaxInterval = 2f;
        [SerializeField] private int _ghostMinAmount = 1;
        [SerializeField] private int _ghostMaxAmount = 5;

        
        void Start()
        {
            var minX = transform.position.x - transform.localScale.x / 2;
            var maxX = transform.position.x + transform.localScale.x / 2;
            var minY = transform.position.y - transform.localScale.y / 2;
            var maxY = transform.position.y + transform.localScale.y / 2;

            _GenerateTimer(minX, maxX, minY, maxY);
        }

        private float _GetInterval()
            => Random.Range(_ghostGeneratedMinInterval, _ghostGeneratedMaxInterval);

        private int _GetAmount()
            => Random.Range(_ghostMinAmount, _ghostMaxAmount);

        private IDisposable _GenerateTimer(
            float minX, float maxX, float minY, float maxY)
            => Observable.Timer(
                    TimeSpan.FromSeconds(_GetInterval()))
                .Subscribe(_ =>
                {
                    int amount = _GetAmount();
                    for (int i = 0; i < amount; ++i)
                    {
                        _GenerateGhost(minX, maxX, minY, maxY);
                    }

                    _GenerateTimer(minX, maxX, minY, maxY);
                })
                .AddTo(this);
        
        private void _GenerateGhost(
            float minX, float maxX, float minY, float maxY)
        {
            var ghostX = Random.Range(minX, maxX);
            var ghostY = Random.Range(minY, maxY);
            Instantiate(
                _ghostPrefab, 
                new Vector3(ghostX, ghostY, ghostY),
                Quaternion.Euler(Vector3.zero),
                _ghostSetTransform);
        }
    }
}