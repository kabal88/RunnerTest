using UnityEngine;
using Data;
using Interfaces;
using Services;
using UnityEngine.InputSystem;

namespace Controllers.UnitStates
{
    public class MovingState : UnitStateBase, IInputListener
    {
        private Vector2 _startPosition;
        private Vector2 _currentPosition;
        private Vector3 _startUnitPos;
        private InputAction _positionUpdateAction;
        private Camera _camera;
        private UnitHolderMonoComponent _unitHolder;


        public MovingState(IUnitContext unit) : base(unit)
        {
            _unitHolder = unit.View.Holder;
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
            _camera = Camera.main;
            var input = ServiceLocator.Get<InputListenerService>();
            input.RegisterObject(this);
            input.TryGetInputAction(IdentifierToStringMap.Point, out _positionUpdateAction);
        }

        public override void UpdateLocal(float deltaTime)
        {
        }

        public void CommandReact(InputStartedCommand command)
        {
            if (command.Index != InputIdentifierMap.Fire)
                return;

            _startPosition = _positionUpdateAction.ReadValue<Vector2>();
            _startUnitPos = _unitHolder.transform.localPosition;
        }

        public void CommandReact(InputCommand command)
        {
            if (command.Index != InputIdentifierMap.Fire)
                return;

            _currentPosition = _positionUpdateAction.ReadValue<Vector2>();

            if (_startPosition == Vector2.zero)
                _startPosition = _currentPosition;

            var startToSpace = _camera.ScreenToWorldPoint(new Vector3(_startPosition.x, _startPosition.y,
                _camera.nearClipPlane));
            var currentToSpace = _camera.ScreenToWorldPoint(new Vector3(_currentPosition.x,
                _currentPosition.y, _camera.nearClipPlane));
            
            var dir = currentToSpace - startToSpace;
            var newPos = _unitHolder.transform.localPosition;
            newPos.x = _startUnitPos.x + dir.magnitude * Unit.Model.Speed * dir.normalized.x;
            newPos.x = Mathf.Clamp(newPos.x, Unit.Model.LeftBorder, Unit.Model.RightBorder);
            _unitHolder.transform.localPosition = newPos;
        }

        public void CommandReact(InputEndedCommand command)
        {
        }

        private void EndState()
        {
            var input = ServiceLocator.Get<InputListenerService>();
            input.UnRegisterObject(this);
            _positionUpdateAction = null;
        }

        public override void Dispose()
        {
            base.Dispose();
            EndState();
            _camera = null;
            _unitHolder = null;
        }
    }
}