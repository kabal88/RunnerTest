using System;
using Components;
using DG.Tweening;
using Interfaces;
using Tweens;
using UnityEngine;

namespace Views
{
    public class UnitView : MonoBehaviour
    {
        [SerializeField] private ScaleParams _collectScaleParams;

        private NumberMeshView _numberMeshView;
        private CollisionProvider _collisionProvider;
        private Tween _tween;

        public Vector3 Position => transform.position;

        public ICollisionProvider CollisionProvider => _collisionProvider;

        public void Init()
        {
            _numberMeshView = GetComponentInChildren<NumberMeshView>();
            _collisionProvider = GetComponentInChildren<CollisionProvider>();
        }

        public void SetNumber(int value)
        {
            _numberMeshView.SetNumber(value);
            PlayCollectAnimation();
        }

        private void PlayCollectAnimation()
        {
            _numberMeshView.transform.localScale = Vector3.one * _collectScaleParams.StartScale;
            _tween?.Kill();
            _tween = _numberMeshView.transform.DOScale(_collectScaleParams.Target, _collectScaleParams.Duration)
                .SetEase(_collectScaleParams.Ease);
        }

        private void OnDestroy()
        {
            _numberMeshView = null;
            _collisionProvider = null;
            _tween?.Kill();
        }
    }
}