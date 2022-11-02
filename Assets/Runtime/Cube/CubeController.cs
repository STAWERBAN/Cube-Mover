using System;
using UnityEngine;

namespace Runtime.Cube
{
    public class CubeController : IDisposable, IUpdatable
    {
        private readonly CubeView _cube;
        private readonly Path _path;
        private readonly CubeModel _cubeModel;

        public CubeController(
            CubeView cube,
            Path path,
            CubeModel cubeModel)
        {
            _cube = cube;
            _path = path;
            _cubeModel = cubeModel;
        }

        public void Initialize()
        {
            _cubeModel.RemoveCubePath += OnRemovePath;
            _cubeModel.CubeStopMoving += OnStopMoving;
            _cubeModel.CubeStartMoving += OnStartMoving;
            _cubeModel.MovePathSegment += OnMovePathSegment;
            _cubeModel.AddCubePathSegment += OnAddPathSegment;
            _cubeModel.RemoveCubeLastPathSegment += OnRemoveLastSegment;
            _cubeModel.SegmentChangeCompleted += OnSegmentChangeComplete;
        }

        public void Dispose()
        {
            _cubeModel.RemoveCubePath -= OnRemovePath;
            _cubeModel.CubeStopMoving -= OnStopMoving;
            _cubeModel.CubeStartMoving -= OnStartMoving;
            _cubeModel.MovePathSegment -= OnMovePathSegment;
            _cubeModel.AddCubePathSegment -= OnAddPathSegment;
            _cubeModel.RemoveCubeLastPathSegment -= OnRemoveLastSegment;
            _cubeModel.SegmentChangeCompleted -= OnSegmentChangeComplete;
        }

        public void Update()
        {
            if (!_cubeModel.IsMoving)
                return;

            if (_cubeModel.CubeMovingPath.Count != 0)
            {
                var towardPosition = _cubeModel.CubeMovingPath[0];

                var currentPosition =
                    Vector3.MoveTowards(_cube.Position(), towardPosition, _cube.MoveSpeed * Time.deltaTime);

                _cube.transform.position = currentPosition;

                _cubeModel.OnCubeMoving(currentPosition, _cube);

                if (Vector3.Distance(currentPosition, towardPosition) <= float.Epsilon)
                {
                    _cubeModel.RemovePathSegment();
                }
            }
        }

        private void OnAddPathSegment()
        {
            _path.AddNewSegment();
        }

        private void OnRemoveLastSegment()
        {
            _path.RemoveLastSegment();
        }

        private void OnRemovePath()
        {
            _path.RemoveAll();
        }

        private void OnMovePathSegment(Vector3 offset)
        {
            _path.MoveLastSegment(offset);
        }

        private void OnSegmentChangeComplete()
        {
            if (!_path.CheckLastPositionAvailableDistance())
                _path.RemoveLastSegment();
        }

        private void OnStopMoving()
        {
            _cube.transform.position = _path.PathSegments[0];
        }

        private void OnStartMoving()
        {
            _cubeModel.SetPath(_path.PathSegments);
        }
    }
}