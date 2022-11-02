using UnityEngine;

namespace Runtime
{
    public class FinishView : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private Color _defaultColor;
        [SerializeField] private float _xOffset;

        public Vector3 FinishPosition()
        {
            return transform.position + Vector3.right * _xOffset;
        }
        
        public void SetDefaultColor()
        {
            _meshRenderer.material.color = _defaultColor;
        }

        public void SetColor(Color color)
        {
            _meshRenderer.material.color = color;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawCube(FinishPosition(), new Vector3(0.05f, 10));
        }
    }
}