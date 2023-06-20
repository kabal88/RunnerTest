using Data;
using Interfaces;
using UnityEngine;

namespace Views
{
    public class SimpleNumber : SimpleInteractableObject, IOwner, IColorableNumber
    {
        [SerializeField] private int _currentNumber;
        private NumberMeshView _numberMeshView;
        public int CurrentNumber => _currentNumber;

        public void SetColor(NumberColor colors)
        {
            _numberMeshView.SetColor(colors);
        }

        private void Awake()
        {
            if (_numberMeshView == null)
            {
                _numberMeshView = GetComponentInChildren<NumberMeshView>();
            }

            _numberMeshView.SetNumber(_currentNumber);
        }

        public override void Interact(IOwner owner = null, ITarget target = null)
        {
            Ability.GetAbility.Execute(this, target);
        }

        public void Die()
        {
            gameObject.SetActive(false);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_numberMeshView == null)
            {
                _numberMeshView = GetComponentInChildren<NumberMeshView>();
            }

            _numberMeshView.SetNumber(_currentNumber);
        }
#endif
    }
}