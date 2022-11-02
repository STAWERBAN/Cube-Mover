using System;
using System.Collections.Generic;

using Runtime.Cube;
using Runtime.UI;

using UnityEngine;

namespace Runtime
{
    public class Game : IDisposable
    {
        private Dictionary<CubeView, Path> _cubePathDictionary = new Dictionary<CubeView, Path>();

        private readonly Camera _camera;
        private readonly UIModel _uiModel;

        private MouseController _mouseController;
        private CubeView _currentCube;
        private Queue<CubeView> _cubeQueue = new Queue<CubeView>();

        public Game(Camera camera, UIModel uiModel)
        {
            _camera = camera;
            _uiModel = uiModel;
        }

        public void Initilize()
        {
            _mouseController = new MouseController(_camera);

            _mouseController.MouseDragging += OnMouseDragging;
            _mouseController.MousePressed += OnMousePressed;
            _mouseController.MouseUpped += OnMouseUpped;

            _uiModel.ChangeCube += OnChangeCube;
        }

        public void AddNewCube(CubeView cube, Path path)
        {
            if (_cubePathDictionary.ContainsKey(cube))
            {
                Debug.LogWarning($@"You add {cube.name} twice");
                return;
            }

            _cubeQueue.Enqueue(cube);

            if (_currentCube is null)
            {
                _currentCube = cube;

                _cubeQueue.Dequeue();

                _uiModel.ChangeColor(_currentCube);
            }

            _cubePathDictionary.Add(cube, path);
        }

        public void Dispose()
        {
            UnsubscribeEvents();
        }

        private void CreateCubeQueue()
        {
            foreach (var cube in _cubePathDictionary.Keys)
            {
                _cubeQueue.Enqueue(cube);
            }
        }

        private void OnChangeCube()
        {
            if (_cubeQueue.Count == 0)
                CreateCubeQueue();

            _currentCube = _cubeQueue.Dequeue();

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
            throw new System.NotImplementedException();
        }

        private void OnMousePressed()
        {
            throw new System.NotImplementedException();
        }

        private void OnMouseDragging(Vector3 arg1, Vector3 arg2)
        {
            throw new System.NotImplementedException();
        }
    }
}