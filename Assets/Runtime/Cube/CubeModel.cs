using System;
using UnityEngine;

namespace Runtime.Cube
{
    public class CubeModel
    {
        public event Action<Vector3> AddCubePathSegment = delegate { };
        public event Action RemoveCubePath = delegate { };
        public event Action RemoveCubeLastPathSegment = delegate { };
        public event Action CubeStartMoving = delegate { };

        public void OnAddCubePathSegment(Vector3 segment)
        {
            AddCubePathSegment.Invoke(segment);
        }

        public void OnRemoveCubePath()
        {
            RemoveCubePath.Invoke();
        }

        public void OnRemoveCubeLastPathSegment()
        {
            RemoveCubeLastPathSegment();
        }

        public void OnCubeStartMoving()
        {
            CubeStartMoving();
        }
    }
}