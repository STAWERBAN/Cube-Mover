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
            _path.AddNewSegment(_cube.Position());
            
            _cubeModel.RemoveCubePath += OnRemovePath;
            _cubeModel.CubeStartMoving += OnStartMoving;
            _cubeModel.AddCubePathSegment += OnAddPathSegment;
            _cubeModel.RemoveCubeLastPathSegment += OnRemoveLastSegment;
        }

        public void Dispose()
        {
            _cubeModel.RemoveCubePath -= OnRemovePath;
            _cubeModel.CubeStartMoving -= OnStartMoving;
            _cubeModel.AddCubePathSegment -= OnAddPathSegment;
            _cubeModel.RemoveCubeLastPathSegment -= OnRemoveLastSegment;
        }

        private void OnAddPathSegment(Vector3 segment)
        {
            _path.AddNewSegment(segment);
        }

        private void OnRemoveLastSegment()
        {
            _path.RemoveLastSegment();
        }

        private void OnRemovePath()
        {
            _path.RemoveAll();
        }

        private void OnStartMoving()
        {
            throw new System.NotImplementedException();
        }
    }
}