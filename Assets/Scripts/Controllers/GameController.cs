﻿using Interfaces;
using Libraries;
using Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Systems;
using Views;


namespace Controllers
{
    public class GameController : IUpdatable, IFixUpdatable, IDisposable
    {
        private GameModel _model;
        private PlayerModel _playerModel;
        private Library _library;
        private UpdateLocalService _updateLocalService;
        private FixUpdateLocalService _fixUpdateLocalService;
        private SpawnService _spawnService;
        private GameUIController _gameUIController;
        private LevelGenerator _levelGenerator;
        private InputListenerService _inputListenerService;
        private CameraController _cameraController;
        private UnitController _unitController;
        private NumbersColorController _numbersColorController;


        public bool IsAlive { get; }

        public GameController(GameModel gameModel,
            Library library,
            GameUIView gameUi,
            CameraView cameraView)
        {
            IsAlive = true;
            _model = gameModel;
            _library = library;
            _gameUIController = new GameUIController(gameUi);
            _cameraController = new CameraController(cameraView,
                _library.GetCameraDescription(_model.CameraDescriptionId).Model);
            _playerModel = new PlayerModel();
        }

        public void Init()
        {
            InitServices();

            var levelGeneratorDescription = _library.GetLevelGeneratorDescription(_model.LevelGeneratorId);
            _levelGenerator = new LevelGenerator(levelGeneratorDescription.Model, levelGeneratorDescription.Prefab);
            _levelGenerator.LevelGenerationFinished += OnLevelGenerationFinished;
            _numbersColorController =
                new NumbersColorController(_library.GetColorPalletDescription(_model.ColorPalletId).Model);
            _unitController = CreateUnit();
            _levelGenerator.GenerateLevel(_playerModel.Level);

            _cameraController.Init();

            _updateLocalService.RegisterObject(_cameraController);
            _inputListenerService.RegisterObject(_cameraController);

            _unitController.HandleState(_unitController.MovingState);
            _cameraController.SetActive(true);
            _gameUIController.HideAllWindows();
            _gameUIController.SetLevel(_playerModel.Level);
            _gameUIController.SetMoney(_playerModel.Money);
        }

        public void UpdateLocal(float deltaTime)
        {
            _updateLocalService.UpdateLocal(deltaTime);
        }

        public void FixedUpdateLocal()
        {
            _fixUpdateLocalService.FixedUpdateLocal();
        }
        
        private void InitServices()
        {
            _updateLocalService = new UpdateLocalService();
            ServiceLocator.SetService(_updateLocalService);
            _fixUpdateLocalService = new FixUpdateLocalService();
            ServiceLocator.SetService(_fixUpdateLocalService);
            _spawnService = new SpawnService();
            ServiceLocator.SetService(_spawnService);
            _spawnService.Init();
            _inputListenerService =
                new InputListenerService(_library.GetInputDescription(_model.InputDescriptionId).Model);
            ServiceLocator.SetService(_inputListenerService);
        }

        private void OnLose()
        {
            _cameraController.SetActive(false);
            _gameUIController.OpenWindow(WindowIdentifiersMap.LoseWindow);
            _gameUIController.RestartButtonClicked += Restart;
        }

        private void OnWin()
        {
            _cameraController.SetActive(false);
            _playerModel.SetLevel(_playerModel.Level + 1);
            _playerModel.AddMoney(_unitController.CurrentNumber);
            _gameUIController.SetLevel(_playerModel.Level);
            _gameUIController.SetMoney(_playerModel.Money);
            _gameUIController.OpenWindow(WindowIdentifiersMap.WinWindow);
            _gameUIController.NextButtonClicked += NextLevel;
        }

        private void NextLevel()
        {
            PrepareLevel();
            _gameUIController.NextButtonClicked -= NextLevel;
        }


        private void Restart()
        {
            PrepareLevel();
            _gameUIController.RestartButtonClicked -= Restart;
        }

        private void PrepareLevel()
        {
            _levelGenerator.GenerateLevel(_playerModel.Level);
            _cameraController.ResetCamera();
            _unitController.Reset();
            _unitController.SetNumber(_playerModel.StartNumber);
            _unitController.HandleState(_unitController.MovingState);
            _gameUIController.HideAllWindows();
        }

        private void OnLevelGenerationFinished()
        {
            var colorNumbers = new List<IColorableNumber>();

            foreach (var r in _levelGenerator.RoadSegmentHolders)
            {
                foreach (var number in r.ColorableNumbers)
                {
                    colorNumbers.Add(number);
                }
            }

            _numbersColorController.Init(colorNumbers, _unitController.CurrentNumber);
            _unitController.NumberChanged += _numbersColorController.ColorNumbers;
        }
        
        private UnitController CreateUnit()
        {
            var description = _library.GetUnitDescription(_model.UnitDescriptionId);
            var spawnPoint = _spawnService.GetObjectsByPredicate(
                x => x.Data.Id == SpawnPointIdentifierMap.UnitSpawnPoint).First();
            var controller =
                new UnitController(description.Model, description.Prefab, spawnPoint.Parent, spawnPoint.Data);
            controller.SetNumber(_playerModel.StartNumber);
            controller.Dead += OnLose;
            controller.CrossFinishLine += OnWin;
            return controller;
        }

        public void Dispose()
        {
        }
    }
}