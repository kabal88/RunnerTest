using Data;
using Interfaces;
using UnityEngine;

namespace Controllers
{
    public class CameraController : IInputListener, IUpdatable
    {
        private CameraView _view;

        private float _currentSpeed = 1;
        private bool _canMove;

        public bool IsAlive { get; }

        public CameraController(CameraView view)
        {
            _view = view;
            IsAlive = true;
        }

        public void UpdateLocal(float deltaTime)
        {
            if (_canMove)
            {
                MoveCamera(deltaTime);
            }
           
        }

        public void CommandReact(InputStartedCommand command)
        {
            if (command.Index == InputIdentifierMap.Fire)
            {
                _canMove = true;
            }
        }

        public void CommandReact(InputCommand command)
        {
        }

        public void CommandReact(InputEndedCommand command)
        {
        }

        private void MoveCamera(float deltaTime)
        {
            var currentSpeed = _currentSpeed * deltaTime;
            
            var transform = _view.transform;
            transform.Translate(Vector3.forward * currentSpeed);
        }
    }
}