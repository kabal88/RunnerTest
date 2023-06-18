﻿using System;
using Controllers.UnitStates;
using Data;
using Interfaces;
using Models;
using Services;
using UnityEngine;
using Views;

namespace Controllers
{
    public class UnitController : IUpdatable, IActivatable, IUnitContext, IDisposable
    {
        public event Action Dead;

        private UnitStateBase _currentState;

        public bool IsAlive => Model.IsAlive;
        public Vector3 Position => Model.Position;
        public IdleState IdleState { get; private set; }
        public DeadState DeadState { get; private set; }
        public MovingState MovingState { get; private set; }
        public UnitModel Model { get; private set; }
        public UnitView View { get; private set; }


        public UnitController(UnitModel model, GameObject prefab ,  Transform parant, SpawnData spawnData)
        {
            Model = model;
            View = GameObject.Instantiate(prefab, spawnData.Position, spawnData.Rotation).GetComponent<UnitView>();
            View.transform.SetParent(parant);
            View.Init();

            IdleState = new IdleState(this);
            DeadState = new DeadState(this);
            MovingState = new MovingState(this);

            Model.SetPosition(View.Position);
            SetState(IdleState);

            ServiceLocator.Get<UpdateLocalService>().RegisterObject(this);
        }
        
        public void SetActive(bool isOn)
        {
            Model.SetIsActive(isOn);
            HandleState(isOn ? MovingState : IdleState);
        }

        public void UpdateLocal(float deltaTime)
        {
            if (Model.IsActive)
            {
                Model.SetPosition(View.Position);
                _currentState.UpdateLocal(deltaTime);
            }
        }

        public void SetState(UnitStateBase newState)
        {
            _currentState = newState;
            _currentState.StartState();
        }

        public void HandleState(UnitStateBase newState)
        {
            _currentState.HandleState(newState);
        }

        public void OnDead()
        {
            Dead?.Invoke();
        }

        public void Dispose()
        {
            ServiceLocator.Get<UpdateLocalService>().UnRegisterObject(this);

            IdleState.Dispose();
            IdleState = null;

            DeadState.Dispose();
            DeadState = null;

            _currentState.Dispose();
            _currentState = null;

            Model = null;

            GameObject.Destroy(View.gameObject);
            View = null;
        }
    }
}