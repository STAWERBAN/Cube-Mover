using System;
using Runtime.Cube;

namespace Runtime.UI
{
    public class UIModel
    {
        public event Action ChangeCube = delegate {  };
        public event Action<CubeView> ChangeCubeColor = delegate {  };

        public void ChangeCubeButtonClick()
        {
            ChangeCube.Invoke();
        }

        public void ChangeColor(CubeView cubeView)
        {
            ChangeCubeColor.Invoke(cubeView);
        }
    }
}