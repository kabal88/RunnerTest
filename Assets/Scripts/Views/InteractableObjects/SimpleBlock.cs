using Interfaces;
using UnityEngine;

namespace Views
{
    public class SimpleBlock : SimpleInteractableObject, IOwner
    {
        [SerializeField] private int _currentNumber;
        
        public int CurrentNumber => _currentNumber;

        private void Awake()
        {
            var textHolder = GetComponentInChildren<BlockNumberTextHolder>();
            textHolder.SetNumber(_currentNumber);
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
            var textHolder = GetComponentInChildren<BlockNumberTextHolder>();
            
            if (textHolder!= null)
            {
                textHolder.SetNumber(_currentNumber);
            }
        }
#endif
    }
}