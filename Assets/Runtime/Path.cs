using System.Collections.Generic;
using UnityEngine;

namespace Runtime
{
    public class Path
    {
        public List<Vector3> PathSegments => _pathSegments;

        private List<Vector3> _pathSegments = new List<Vector3>();
        private int _currentIndex = 0;

        private readonly LineRenderer _lineRenderer;

        private const int StartIndex = 1; //First element equals cube start position

        public Path(LineRenderer lineRenderer)
        {
            _lineRenderer = lineRenderer;
        }

        public void AddNewSegment(Vector3 segment)
        {
            _pathSegments.Add(segment);

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

        private void SetLineRendererParameters()
        {
            _lineRenderer.positionCount = _currentIndex;
            _lineRenderer.SetPositions(_pathSegments.ToArray());
        }
    }
}