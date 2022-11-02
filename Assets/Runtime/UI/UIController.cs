﻿namespace Runtime.UI
{
    public class UIController
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
        }
    }
}