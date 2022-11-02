using System;
using System.Collections.Generic;

using Runtime.Cube;
using Runtime.UI;

using UnityEngine;

namespace Runtime
{
    public class Game : IDisposable
    {
        private Dictionary<CubeView, CubeModel> _cubeDictionary = new Dictionary<CubeView, CubeModel>();

        private readonly UIModel _uiModel;
        private readonly FinishView _finishView;
        private readonly MouseController _mouseController;

        private CubeView _currentCube;
        private CubeModel _currentModel;
        private Queue<CubeView> _cubeQueue = new Queue<CubeView>();

        public Game(UIModel uiModel, FinishView finishView, MouseController mouseController)
        {
            _uiModel = uiModel;
            _finishView = finishView;
            _mouseController = mouseController;
        }

        public void Initilize()
        {
            _uiModel.StopButtonClick += OnStopButtonClick;
            _uiModel.StartButtonClick += OnStartButtonClick;
            
            SubscribeToAction();
        }

        public void Dispose()
        {
            _uiModel.StopButtonClick -= OnStopButtonClick;
            _uiModel.StartButtonClick -= OnStartButtonClick;

            UnsubscribeEvents();
        }

        public void AddNewCube(CubeView cube, CubeModel model)
        {
            if (_cubeDictionary.ContainsKey(cube))
            {
                Debug.LogWarning($@"You add {cube.name} twice");
                return;
            }

            _cubeQueue.Enqueue(cube);

            if (_currentCube is null)
            {
                _currentCube = cube;
                _currentModel = model;

                _cubeQueue.Dequeue();

                _uiModel.ChangeColor(_currentCube);
            }

            _cubeDictionary.Add(cube, model);
        }

        private void CreateCubeQueue()
        {
            foreach (var cube in _cubeDictionary.Keys)
            {
                _cubeQueue.Enqueue(cube);
            }
        }

        private void OnDeletePathSegmentButtonClicked()
        {
            _currentModel.OnRemoveCubeLastPathSegment();
        }

        private void OnDeletePathButtonClicked()
        {
            _currentModel.OnRemoveCubePath();
        }

        private void OnChangeCube()
        {
            if (_cubeQueue.Count == 0)
                CreateCubeQueue();

            _currentCube = _cubeQueue.Dequeue();
            _currentModel = _cubeDictionary[_currentCube];

            _uiModel.ChangeColor(_currentCube);
        }

        private void OnMouseUpped()
        {
            _currentModel.OnSegmentChangeComplete();
        }

        private void OnMousePressed()
        {
            _currentModel.OnAddNewSegment();
        }

        private void OnMouseDragging(Vector3 offset)
        {
            _currentModel.OnMoveLastSegment(offset);
        }

        private void OnStartButtonClick()
        {
            UnsubscribeEvents();

            _uiModel.SetBackgroundDefaultColor();

            foreach (var cubeModel in _cubeDictionary.Values)
            {
                cubeModel.OnCubeStartMoving();
                cubeModel.CubeMovingPosition += OnCubeMoving;
            }
        }

        private void OnStopButtonClick()
        {
            _finishView.SetDefaultColor();
            _uiModel.ChangeColor(_currentCube);

            SubscribeToAction();

            foreach (var cubeModel in _cubeDictionary.Values)
            {
                cubeModel.OnCubeStopMoving();
                cubeModel.CubeMovingPosition -= OnCubeMoving;
            }
        }

        private void OnCubeMoving(Vector3 position, CubeView cubeView)
        {
            if ((position.x < _finishView.FinishPosition().x))
                return;

            _cubeDictionary[cubeView].CubeMovingPosition -= OnCubeMoving;
            _finishView.SetColor(cubeView.CubeColor);
        }

        private void SubscribeToAction()
        {
            _mouseController.MouseDragging += OnMouseDragging;
            _mouseController.MousePressed += OnMousePressed;
            _mouseController.MouseUpped += OnMouseUpped;

            _uiModel.ChangeCube += OnChangeCube;
            _uiModel.DeletePathButtonClicked += OnDeletePathButtonClicked;
            _uiModel.DeletePathSegmentButtonClicked += OnDeletePathSegmentButtonClicked;
        }

        private void UnsubscribeEvents()
        {
            _mouseController.MouseDragging -= OnMouseDragging;
            _mouseController.MousePressed -= OnMousePressed;
            _mouseController.MouseUpped -= OnMouseUpped;

            _uiModel.ChangeCube -= OnChangeCube;
            _uiModel.DeletePathButtonClicked -= OnDeletePathButtonClicked;
            _uiModel.DeletePathSegmentButtonClicked -= OnDeletePathSegmentButtonClicked;
        }
    }
}