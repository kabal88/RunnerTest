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

            if (owner.CurrentNumber <= target.CurrentNumber)
            {
                owner.Die();
            }
            else
            {
                target.Die();
            }
        }
    }
}