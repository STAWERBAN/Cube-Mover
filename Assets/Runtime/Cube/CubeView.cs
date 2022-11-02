using System;
using UnityEngine;

namespace Runtime.Cube
{
    public class CubeView : MonoBehaviour
    {
        [SerializeField] private Color _backgroundColor;
        [SerializeField] private Color _pathColor;
        [SerializeField] private Color _cubeColor;
        [SerializeField] private MeshRenderer _meshRenderer;

        public Color BackgroundColor => _backgroundColor;
        public Color PathColor => _pathColor;

        public Vector3 Position()
        {
            return transform.position;
        }

        private void OnValidate()
        {
            _meshRenderer.material.color = _cubeColor;
        }
    }
}