using UnityEngine;

namespace Runtime
{
    public class BackgroundView : MonoBehaviour
    {
        [SerializeField] private Color _defaultColor;
        [SerializeField] private MeshRenderer _meshRenderer;

        public void ChangeColor(Color color)
        {
            _meshRenderer.material.color = color;
        }

        public void SetDefaultColor()
        {
            _meshRenderer.material.color = _defaultColor;
        }
    }
}