using Abilities;
using Interfaces;
using UnityEngine;

namespace Views
{
    public class SimpleInteractableObject : InteractableObject
    {
        [SerializeField] protected AbilityDescription Ability;
        
        public override void Interact(IOwner owner = null, ITarget target = null)
        {
            Ability.GetAbility.Execute(owner, target);
        }
    }
}