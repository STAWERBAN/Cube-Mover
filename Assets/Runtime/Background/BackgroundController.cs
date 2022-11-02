using System;

using Runtime.Cube;
using Runtime.UI;

namespace Runtime.Background
{
    public class BackgroundController : IDisposable
    {
        private readonly UIModel _uiModel;
        private readonly BackgroundView _backgroundView;

        public BackgroundController(UIModel uiModel, BackgroundView backgroundView)
        {
            _uiModel = uiModel;
            _backgroundView = backgroundView;
        }

        public void Initialize()
        {
            _uiModel.ChangeCubeColor += OnChangeColor;
            _uiModel.SetDefaultBackgroundColor += _backgroundView.SetDefaultColor;
        }

        public void Dispose()
        {
            _uiModel.ChangeCubeColor -= OnChangeColor;
            _uiModel.SetDefaultBackgroundColor -= _backgroundView.SetDefaultColor;
        }

        private void OnChangeColor(CubeView cubeView)
        {
            _backgroundView.ChangeColor(cubeView.BackgroundColor);
        }
    }
}