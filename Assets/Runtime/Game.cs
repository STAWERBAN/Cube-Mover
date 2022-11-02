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
        private readonly BackgroundView _backgroundView;

        private MouseController _mouseController;

        public Game(Camera camera, BackgroundView backgroundView, UIModel uiModel)
        {
            _camera = camera;
            _backgroundView = backgroundView;
            _uiModel = uiModel;
        }

        public void Initilize()
        {
            _mouseController = new MouseController(_camera);

            _mouseController.MouseDragging += OnMouseDragging;
            _mouseController.MousePressed += OnMousePressed;
            _mouseController.MouseUpped += OnMouseUpped;
        }

        public void AddNewCube(CubeView cube, Path path)
        {
            if (_cubePathDictionary.ContainsKey(cube))
            {
                Debug.LogWarning($@"You add {cube.name} twice");
                return;
            }

            _cubePathDictionary.Add(cube, path);
        }

        public void Dispose()
        {
            UnsubscribeEvents();
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