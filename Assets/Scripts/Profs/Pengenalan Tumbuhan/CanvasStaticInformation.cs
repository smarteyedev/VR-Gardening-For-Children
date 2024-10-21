using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Smarteye
{
    public class CanvasStaticInformation : MonoBehaviour
    {
        private GameObject _panelCanvas;
        [SerializeField] private GameObject _otherDoublePanel;

        private void Start()
        {
            _panelCanvas = transform.GetChild(0).gameObject;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player")){
                _panelCanvas.SetActive(false);

                if (_otherDoublePanel != null)
                {
                    _otherDoublePanel.SetActive(true);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _panelCanvas.SetActive(true);

                if (_otherDoublePanel != null)
                {
                    _otherDoublePanel.SetActive(false);
                }
            }
        }
    }
}
