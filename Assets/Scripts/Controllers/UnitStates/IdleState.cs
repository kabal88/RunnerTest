using Interfaces;

namespace Controllers.UnitStates
{
    public class IdleState : UnitStateBase
    {
        public IdleState(IUnitContext unit) : base(unit)
        {
        }

        public override void HandleState(UnitStateBase newState)
        {
        }

        public override void StartState()
        {
        }

        public override void UpdateLocal(float deltaTime)
        {
        }
    }
}