using System;
using Interfaces;

namespace Controllers.UnitStates
{
    public class MovingState : UnitStateBase
    {
        public MovingState(IUnitContext unit) : base(unit)
        {
        }

        public override void HandleState(UnitStateBase newState)
        {
            switch (newState)
            {
                case DeadState deadState:
                    Unit.SetState(deadState);
                    break;
                case IdleState idleState:
                    Unit.SetState(idleState);
                    break;
            }
        }

        public override void StartState()
        {
            
        }

        public override void UpdateLocal(float deltaTime)
        {
            
        }
    }
}