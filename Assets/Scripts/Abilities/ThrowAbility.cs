using Interfaces;

namespace Abilities
{
    public class ThrowAbility : IAbility
    {
        public void Execute(IOwner owner = null, ITarget target = null)
        {
            if (target is IUnitContext context)
            {
                context.HandleState(context.JumpState);
            }
        }
    }
}