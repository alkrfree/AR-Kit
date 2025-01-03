using TMPro;
using UnityEngine;

namespace ChessBoardDemo.Scripts
{
    public class MarkerStateUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _stateText;
        [SerializeField] private TMP_Text _clickText;

        public void ChangeStateText(string text) => 
            _stateText.text = text;

        public void ChangeClickText(string text) => 
            _clickText.text = text;
    }
}