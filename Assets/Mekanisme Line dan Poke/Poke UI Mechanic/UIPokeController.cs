using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Smarteye
{
    public class UIPokeController : MonoBehaviour
    {
        [SerializeField] private List<XRSimpleInteractable> _listPokeButton = new List<XRSimpleInteractable>();
        [SerializeField] private List<UIPokeInformation> _listInformation;
        private CanvasUIPoke _canvasUIPoke;

        private int _activeIndex = 100;
        private bool _hasActive = false;

        private void OnEnable()
        {
            _listPokeButton.Clear();
            XRSimpleInteractable[] listPoke = GetComponentsInChildren<XRSimpleInteractable>();
            _listPokeButton.AddRange(listPoke);
            _canvasUIPoke = GetComponentInChildren<CanvasUIPoke>();
            _canvasUIPoke.showUIInformation = false;
            _activeIndex = 100;

            for (int i = 0; i < listPoke.Length; i++)
            {
                int index = i;
                _listPokeButton[i].selectEntered.AddListener((SelectEnterEventArgs arg0) => OnXRShowInformation(arg0, index));
            }
        }

        private void OnXRShowInformation(SelectEnterEventArgs arg0, int index)
        {
            if(_activeIndex != index || !_hasActive)
            {
                _activeIndex = index;
                _canvasUIPoke.showUIInformation = true;
                _hasActive = true;
                _canvasUIPoke.SetUIInformation(_listInformation[index]);
            }
            else
            {
                _hasActive = false;
                _canvasUIPoke.showUIInformation = false;
            }
        }

        private void OnDisable()
        {
            
        }

        private void OnValidate()
        {
            if (_listInformation != null)
            {
                foreach (var info in _listInformation)
                {
                    if (info.imageType == ImageType.Single)
                    {
                        if (info.spriteImage.Length != 1)
                        {
                            System.Array.Resize(ref info.spriteImage, 1);
                        }
                    }
                }
            }
        }
    }

    [System.Serializable]
    public class UIPokeInformation
    {
        public string titleInformation;
        [TextArea(3, 10)]
        public string description;

        public ImageType imageType;
        public Sprite[] spriteImage;
    }

    public enum ImageType
    {
        Single,
        Gif
    }
}


