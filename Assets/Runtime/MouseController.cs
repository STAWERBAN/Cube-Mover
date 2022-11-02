using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime
{
    public class MouseController : IUpdatable
    {
        public event Action MousePressed = delegate { };
        public event Action MouseUpped = delegate { }; 
        public event Action<Vector3> MouseDragging = delegate { };

        private readonly Camera _camera;
        private readonly EventSystem _eventSystem;
        
        private Vector3 _startPosition;
        private Vector3 _endPosition;
        private bool _dragging;

        public MouseController(Camera camera, EventSystem eventSystem)
        {
            _camera = camera;
            _eventSystem = eventSystem;
        }
        
        public void Update()
        {
            if(_eventSystem.currentSelectedGameObject && !_dragging)
                return;
            
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