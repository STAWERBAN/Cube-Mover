using System.Collections.Generic;
using UnityEngine;

namespace Runtime
{
    public class Path
    {
        public List<Vector3> PathSegments => _pathSegments;

        private List<Vector3> _pathSegments = new List<Vector3>();
        
        private const int StartIndex = 1; //First element equals cube start position

        public void AddNewSegment(Vector3 segment)
        {
            _pathSegments.Add(segment);
        }

        public void RemoveAll()
        {
            _pathSegments = new List<Vector3>();
        }

        public void RemoveLastSegment()
        {
            var segmentsCount = _pathSegments.Count;
            
            if (segmentsCount <= StartIndex)
                return;

            _pathSegments.RemoveAt(segmentsCount - 1);
        }
    }
}