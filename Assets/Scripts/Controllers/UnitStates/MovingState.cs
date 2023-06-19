using Interfaces;
using UnityEngine;
using Views;

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
                    EndState();
                    Unit.SetState(deadState);
                    break;
                case IdleState idleState:
                    EndState();
                    Unit.SetState(idleState);
                    break;
            }
        }

        public override void StartState()
        {
            Unit.View.CollisionProvider.CollisionEnter += OnCollisionEnter;
            Unit.View.CollisionProvider.TriggerEnter += OnTriggerEnter;
        }
        
        private void OnTriggerEnter(Collider obj)
        {
            if (obj.TryGetComponent(out InteractableObject interactableObject))
            {
                interactableObject.Interact(null, Unit.Target);
            }
        }

        private void OnCollisionEnter(Collision obj)
        {
            if (obj.gameObject.TryGetComponent(out InteractableObject interactableObject))
            {
                interactableObject.Interact(null, Unit.Target);
            }
        }

        private void EndState()
        {
            Unit.View.CollisionProvider.CollisionEnter -= OnCollisionEnter;
            Unit.View.CollisionProvider.TriggerEnter -= OnTriggerEnter;
        }   

        public override void UpdateLocal(float deltaTime)
        {
        }

        public override void Dispose()
        {
            base.Dispose();
            EndState();
        }
    }
}