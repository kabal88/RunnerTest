using System;
using Interfaces;

namespace Abilities
{
    [Serializable]
    public class FinishLineAbility : IAbility
    {
        public void Execute(IOwner owner = null, ITarget target = null)
        {
            if (target is IUnitContext context)
            {
                context.HandleState(context.CrossFinishLineState);
            }
        }
    }
}
