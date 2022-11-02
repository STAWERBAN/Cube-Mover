using System;
using System.Collections.Generic;
using Runtime.Cube;
using Runtime.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime
{
    public class Game : IDisposable
    {
        private Dictionary<CubeView, CubeModel> _cubeDictionary = new Dictionary<CubeView, CubeModel>();

        private readonly Camera _camera;
        private readonly UIModel _uiModel;
        private readonly EventSystem _eventSystem;

        private MouseController _mouseController;
        private CubeView _currentCube;
        private CubeModel _currentModel;
        private Queue<CubeView> _cubeQueue = new Queue<CubeView>();

        public Game(Camera camera, UIModel uiModel, EventSystem eventSystem)
        {
            _camera = camera;
            _uiModel = uiModel;
            _eventSystem = eventSystem;
        }

        public void Initilize()
        {
            _mouseController = new MouseController(_camera, _eventSystem);

            _mouseController.MouseDragging += OnMouseDragging;
            _mouseController.MousePressed += OnMousePressed;
            _mouseController.MouseUpped += OnMouseUpped;

            _uiModel.ChangeCube += OnChangeCube;
            _uiModel.DeletePathButtonClicked += OnDeletePathButtonClicked;
            _uiModel.DeletePathSegmentButtonClicked += OnDeletePathSegmentButtonClicked;
        }

        public void Update()
        {
            _mouseController?.Update();
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

        public void Dispose()
        {
            UnsubscribeEvents();
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

        private void UnsubscribeEvents()
        {
            _mouseController.MouseDragging -= OnMouseDragging;
            _mouseController.MousePressed -= OnMousePressed;
            _mouseController.MouseUpped -= OnMouseUpped;
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
    }
}