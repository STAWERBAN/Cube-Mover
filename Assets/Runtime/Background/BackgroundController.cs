using Runtime.Cube;
using Runtime.UI;

namespace Runtime
{
    public class BackgroundController
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
        }

        private void OnChangeColor(CubeView cubeView)
        {
            _backgroundView.ChangeColor(cubeView.BackgroundColor);
        }
    }
}