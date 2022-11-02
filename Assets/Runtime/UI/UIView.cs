using UnityEngine;
using UnityEngine.UI;

namespace Runtime.UI
{
    public class UIView : MonoBehaviour
    {
        [SerializeField] private Button _removeSegmentButton;
        [SerializeField] private Button _removePathButton;
        [SerializeField] private Button _changeBlockButton;
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _stopButton;


        public Button RemoveSegmentButton => _removeSegmentButton;
        public Button RemovePathButton => _removePathButton;
        public Button ChangeBlockButton => _changeBlockButton;
        public Button PlayButton => _playButton;
        public Button StopButton => _stopButton;
    }
}