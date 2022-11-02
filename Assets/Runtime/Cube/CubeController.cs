using System;
using UnityEngine;

namespace Runtime.Cube
{
    public class CubeController : IDisposable
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
            _cubeModel.CubeStartMoving += OnStartMoving;
            _cubeModel.MovePathSegment += OnMovePathSegment;
            _cubeModel.AddCubePathSegment += OnAddPathSegment;
            _cubeModel.RemoveCubeLastPathSegment += OnRemoveLastSegment;
            _cubeModel.SegmentChangeCompleted += OnSegmentChangeComplete;
        }

        public void Dispose()
        {
            _cubeModel.RemoveCubePath -= OnRemovePath;
            _cubeModel.CubeStartMoving -= OnStartMoving;
            _cubeModel.MovePathSegment -= OnMovePathSegment;
            _cubeModel.AddCubePathSegment -= OnAddPathSegment;
            _cubeModel.RemoveCubeLastPathSegment -= OnRemoveLastSegment;
            _cubeModel.SegmentChangeCompleted -= OnSegmentChangeComplete;
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
            if(!_path.CheckLastPositionAvailableDistance())
                _path.RemoveLastSegment();
        }

        private void OnStartMoving()
        {
            throw new System.NotImplementedException();
        }
    }
}