using System;

namespace Runtime.UI
{
    public class UIController : IDisposable
    {
        private readonly UIView _view;
        private readonly UIModel _uiModel;

        public UIController(UIView view, UIModel uiModel)
        {
            _view = view;
            _uiModel = uiModel;
        }

        public void Initialize()
        {
            _view.ChangeBlockButton.onClick.AddListener(_uiModel.ChangeCubeButtonClick);
            _view.RemoveSegmentButton.onClick.AddListener(_uiModel.OnDeleteSegmentButtonClick);
            _view.RemovePathButton.onClick.AddListener(_uiModel.OnDeletePathButtonClicked);
            _view.StopButton.onClick.AddListener(() =>
            {
                _uiModel.OnStopButtonClick();
                _view.StopButton.gameObject.SetActive(false);
                _view.PlayButton.gameObject.SetActive(true);
            });
            _view.PlayButton.onClick.AddListener(() =>
            {
                _uiModel.OnStartButtonClick();
                _view.StopButton.gameObject.SetActive(true);
                _view.PlayButton.gameObject.SetActive(false);
            });
        }

        public void Dispose()
        {
            _view.ChangeBlockButton.onClick.RemoveAllListeners();
            _view.RemoveSegmentButton.onClick.RemoveAllListeners();
            _view.RemovePathButton.onClick.RemoveAllListeners();
            _view.StopButton.onClick.RemoveAllListeners();
            _view.PlayButton.onClick.RemoveAllListeners();
        }
    }
}