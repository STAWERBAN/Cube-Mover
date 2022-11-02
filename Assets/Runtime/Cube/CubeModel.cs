using System;
using UnityEngine;

namespace Runtime.Cube
{
    public class CubeModel
    {
        public event Action<Vector3> MovePathSegment = delegate { };
        public event Action SegmentChangeCompleted = delegate {  };
        public event Action AddCubePathSegment = delegate { };
        public event Action RemoveCubePath = delegate { };
        public event Action RemoveCubeLastPathSegment = delegate { };
        public event Action CubeStartMoving = delegate { };

        public void OnMoveLastSegment(Vector3 segment)
        {
            MovePathSegment.Invoke(segment);
        }

        public void OnAddNewSegment()
        {
            AddCubePathSegment.Invoke();
        }

        public void OnSegmentChangeComplete()
        {
            SegmentChangeCompleted.Invoke();
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