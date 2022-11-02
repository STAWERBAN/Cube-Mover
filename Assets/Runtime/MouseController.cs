using System;
using UnityEngine;

namespace Runtime
{
    public class MouseController
    {
        public event Action MousePressed = delegate { };
        public event Action MouseUpped = delegate { }; 
        public event Action<Vector3> MouseDragging = delegate { };

        private readonly Camera _camera;
        
        private Vector3 _startPosition;
        private Vector3 _endPosition;
        private bool _dragging;

        public MouseController(Camera camera)
        {
            _camera = camera;
        }
        
        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _dragging = true;
                _startPosition = GetInputWorldCoords();

                MousePressed();
            }

            if (Input.GetMouseButtonUp(0))
            {
                _dragging = false;
                MouseUpped();
            }

            if (_dragging)
            {
                _endPosition = GetInputWorldCoords();
                MouseDragging(_endPosition - _startPosition);
            }
        }
        
        private Vector3 GetInputWorldCoords()
        {
            var inputPosition = Input.mousePosition;
            inputPosition.z = _camera.transform.position.z * -1;

            return _camera.ScreenToWorldPoint(inputPosition);
        }
    }
}