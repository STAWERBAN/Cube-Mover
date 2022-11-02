using System;
using System.Collections.Generic;

using UnityEngine;

namespace Runtime.Cube
{
    public class CubeModel
    {
        public event Action<Vector3> MovePathSegment = delegate { };
        public event Action<Vector3, CubeView> CubeMovingPosition = delegate {  };
        public event Action SegmentChangeCompleted = delegate { };
        public event Action AddCubePathSegment = delegate { };
        public event Action RemoveCubePath = delegate { };
        public event Action RemoveCubeLastPathSegment = delegate { };
        public event Action CubeStartMoving = delegate { };
        public event Action CubeStopMoving = delegate { };

        public bool IsMoving { get; private set; }
        public List<Vector3> CubeMovingPath = new List<Vector3>();

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
            RemoveCubeLastPathSegment.Invoke();
        }

        public void OnCubeStartMoving()
        {
            IsMoving = true;

            CubeStartMoving.Invoke();
        }

        public void OnCubeStopMoving()
        {
            IsMoving = false;

            CubeStopMoving.Invoke();
        }

        public void SetPath(List<Vector3> path)
        {
            CubeMovingPath = new List<Vector3>(path);
        }

        public void RemovePathSegment()
        {
            CubeMovingPath.RemoveAt(0);
        }

        public void OnCubeMoving(Vector3 position, CubeView view)
        {
            CubeMovingPosition.Invoke(position, view);
        }
    }
}