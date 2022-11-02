using System.Collections.Generic;
using UnityEngine;

namespace Runtime
{
    public class Path
    {
        public List<Vector3> PathSegments => _pathSegments;

        private List<Vector3> _pathSegments = new List<Vector3>();
        private Vector3 _lastSegmentPosition;
        private int _currentIndex = 0;

        private readonly LineRenderer _lineRenderer;

        private const int StartIndex = 1; //First element equals cube start position
        private const float MinDistanceToAddSegment = 1;

        public Path(LineRenderer lineRenderer, Color lineColor, Vector3 firstPosition)
        {
            _lineRenderer = lineRenderer;
            _lineRenderer.material.color = lineColor;

            AddFirstSegment(firstPosition);
        }

        public void AddNewSegment()
        {
            _lastSegmentPosition = _pathSegments[_currentIndex - 1];
            _pathSegments.Add(_lastSegmentPosition);

            _currentIndex++;

            SetLineRendererParameters();
        }

        public void RemoveAll()
        {
            var firstElement = _pathSegments[0];

            _currentIndex = StartIndex;

            _pathSegments = new List<Vector3> { firstElement };

            SetLineRendererParameters();
        }

        public void RemoveLastSegment()
        {
            if (_currentIndex <= StartIndex)
                return;

            _pathSegments.RemoveAt(_currentIndex - 1);

            _currentIndex--;

            SetLineRendererParameters();
        }

        public void MoveLastSegment(Vector3 offset)
        {
            var currentPosition = _lastSegmentPosition + offset;

            _pathSegments[_currentIndex - 1] = currentPosition;
            _lineRenderer.SetPosition(_currentIndex - 1, currentPosition);
        }

        public bool CheckLastPositionAvailableDistance()
        {
            if (_currentIndex <= StartIndex)
                return true;
            
            var lastElement = _pathSegments[_currentIndex - 1];
            var previousElement = _pathSegments[_currentIndex - 2];

            return Vector3.Distance(lastElement, previousElement) > MinDistanceToAddSegment;
        }

        private void AddFirstSegment(Vector3 segmentPosition)
        {
            _pathSegments.Add(segmentPosition);

            _currentIndex++;

            SetLineRendererParameters();
        }

        private void SetLineRendererParameters()
        {
            _lineRenderer.positionCount = _currentIndex;
            _lineRenderer.SetPositions(_pathSegments.ToArray());
        }
    }
}