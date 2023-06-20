﻿using Interfaces;

namespace Abilities
{
    public class HoleAbility :IAbility
    {
        public void Execute(IOwner owner = null, ITarget target = null)
        {
            if (target is IUnitContext context)
            {
                context.HandleState(context.FallingState);
            }
        }
    }
}