using System;
using System.Collections.Generic;
using Views;

namespace Controllers
{
    public class GameUIController : IDisposable
    {
        private GameUIView _view;

        public GameUIController(GameUIView view)
        {
            _view = view;
            _view.Init();
        }
        
        public void SetUIActive(bool isOn)
        {
            _view.SetActive(isOn);
        }
        
        public void Dispose()
        {
        }
    }
}