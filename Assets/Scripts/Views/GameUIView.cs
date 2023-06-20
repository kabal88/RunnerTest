using System;
using UI.Windows;
using UnityEngine;

namespace Views
{
    public class GameUIView : MonoBehaviour
    {
        public event Action RestartButtonClicked
        {
            add => _winWindow.NextButtonClicked += value;
            remove => _winWindow.NextButtonClicked -= value;
        }
        
        public event Action NextButtonClicked
        {
            add => _loseWindow.RestartButtonClicked += value;
            remove => _loseWindow.RestartButtonClicked += value;
        }
        
        private WindowBase[] _windows;

        private WinWindow _winWindow;
        private LoseWindow _loseWindow;
        
        public void Init()
        {
            _winWindow = GetComponentInChildren<WinWindow>();
            _loseWindow = GetComponentInChildren<LoseWindow>();
            _windows = GetComponentsInChildren<WindowBase>();
        }
        
        public void OpenWindow(int windowId)
        {
            foreach (var w in _windows)
            {
                if (w.ID== windowId)
                {
                    w.Show();
                }
            }
        }

        public void CloseWindow(int windowId)
        {
            foreach (var w in _windows)
            {
                if (w.ID== windowId)
                {
                    w.Hide();
                }
            }
        }

        public void SetActive(bool isOn)
        {
            gameObject.SetActive(isOn);
        }
    }
}