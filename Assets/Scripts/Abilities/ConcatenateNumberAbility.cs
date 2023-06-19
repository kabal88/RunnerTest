using System;
using Interfaces;

namespace Abilities
{
    [Serializable]
    public class ConcatenateNumberAbility : IAbility
    {
        public void Execute(IOwner owner = null, ITarget target = null)
        {
            if (owner == null || target == null)
                return;

            if (owner.CurrentNumber <= target.CurrentNumber)
            {
                target.AddToCurrentNumber(owner.CurrentNumber);
                owner.Die();
            }
            else
            {
                target.Die();
            }
        }
    }
}