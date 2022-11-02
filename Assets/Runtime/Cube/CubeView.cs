using UnityEngine;

namespace Runtime.Cube
{
    public class CubeView : MonoBehaviour
    {
        [SerializeField] private Color _backgroundColor;
        [SerializeField] private Color _pathColor;
        [SerializeField] private Color _cubeColor;
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private float _moveSpeed;

        public Color BackgroundColor => _backgroundColor;
        public Color PathColor => _pathColor;
        public Color CubeColor => _cubeColor;
        public LineRenderer LineRenderer => _lineRenderer;
        public float MoveSpeed => _moveSpeed;


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