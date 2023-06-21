using System;
using Interfaces;

namespace Abilities
{
    [Serializable]
    public class BlockNumberAbility : IAbility
    {
        public void Execute(IOwner owner = null, ITarget target = null)
        {
            if (owner == null || target == null)
                return;


            target.AddToCurrentNumber(owner.CurrentNumber);
            if (target.CurrentNumber > 0)
            {
                owner.Die();
            }
        }
    }
}