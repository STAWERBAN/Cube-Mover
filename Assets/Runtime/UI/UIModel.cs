using System;

using Runtime.Cube;

namespace Runtime.UI
{
    public class UIModel
    {
        public event Action ChangeCube = delegate { };
        public event Action<CubeView> ChangeCubeColor = delegate { };
        public event Action SetDefaultBackgroundColor = delegate { };
        public event Action DeletePathButtonClicked = delegate { };
        public event Action DeletePathSegmentButtonClicked = delegate { };
        public event Action StopButtonClick = delegate { };
        public event Action StartButtonClick = delegate { };

        public void ChangeCubeButtonClick()
        {
            ChangeCube.Invoke();
        }

        public void ChangeColor(CubeView cubeView)
        {
            ChangeCubeColor.Invoke(cubeView);
        }

        public void SetBackgroundDefaultColor()
        {
            SetDefaultBackgroundColor.Invoke();
        }

        public void OnDeleteSegmentButtonClick()
        {
            DeletePathSegmentButtonClicked.Invoke();
        }

        public void OnDeletePathButtonClicked()
        {
            DeletePathButtonClicked();
        }

        public void OnStopButtonClick()
        {
            StopButtonClick();
        }

        public void OnStartButtonClick()
        {
            StartButtonClick();
        }
    }
}