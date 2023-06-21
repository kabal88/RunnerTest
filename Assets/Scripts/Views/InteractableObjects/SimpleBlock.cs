using Interfaces;
using UnityEngine;

namespace Views
{
    public class SimpleBlock : SimpleInteractableObject, IOwner
    {
        [SerializeField] private int _currentNumber;
        [SerializeField] private float _thickness = 0.2f;
        [SerializeField] private Transform _meshTransform;

        public int CurrentNumber => _currentNumber;

        private void Awake()
        {
            var textHolder = GetComponentInChildren<BlockNumberTextHolder>();
            textHolder.SetNumber(_currentNumber);
            UpdateThickness();
        }

        public override void Interact(IOwner owner = null, ITarget target = null)
        {
            Ability.GetAbility.Execute(this, target);
        }

        public void Die()
        {
            gameObject.SetActive(false);
        }

        private void UpdateThickness()
        {
            var absNum = Mathf.Abs(_currentNumber);
            var scale = _meshTransform.localScale;
            var multiplier = absNum / 10f;
            scale.z = _thickness * multiplier;
            _meshTransform.localScale = scale;
            var position = _meshTransform.localPosition;
            position.z = scale.z / 2f;
            _meshTransform.localPosition = position;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            var textHolder = GetComponentInChildren<BlockNumberTextHolder>();

            if (textHolder != null)
            {
                textHolder.SetNumber(_currentNumber);
            }

            if (_meshTransform != null)
            {
                UpdateThickness();
            }
        }
#endif
    }
}