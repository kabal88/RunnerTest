using Data;
using Interfaces;
using UnityEngine;

namespace Controllers
{
    public class CameraController : IInputListener, IUpdatable
    {
        private CameraView _view;
        private CameraModel _model;
        
        public bool IsAlive { get; }

        public CameraController(CameraView view, CameraModel model)
        {
            _view = view;
            _model = model;
            IsAlive = true;
        }

        public void UpdateLocal(float deltaTime)
        {
            if (_model.CanMove)
            {
                MoveCamera(deltaTime);
            }
           
        }

        public void CommandReact(InputStartedCommand command)
        {
            if (command.Index == InputIdentifierMap.Fire)
            {
                _model.SetCanMove(true);
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
            var currentSpeed = _model.Speed * deltaTime;
            
            var transform = _view.transform;
            transform.Translate(Vector3.forward * currentSpeed);
        }

        public void ResetCameraPosition()
        {
            _view.transform.position = _model.StartPosition;
        }
    }
}