using System;
using Controllers;
using Identifier;
using Libraries;
using UnityEngine;
using Views;

public class GameRoot : MonoBehaviour
{
    [SerializeField] private GameIdentifier _gameID;
    [SerializeField] private Library _library;
    [SerializeField] private GameUIView _gameUI;

    private GameController _gameController;

    private void Start()
    {
        _library.Init();
        var description = _library.GetGameDescription(_gameID.Id);
        _gameController = new GameController(description.Model, _library, _gameUI);
        _gameController.Init();
    }

    private void Update()
    {
        _gameController.UpdateLocal(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        _gameController.FixedUpdateLocal();
    }
}