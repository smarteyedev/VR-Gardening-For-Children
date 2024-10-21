using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Smarteye
{
    public class UIPokeController : MonoBehaviour
    {
        [SerializeField] private ABaseTrackerProses _prosesPengenalanTumbuhan;
        [SerializeField] private PlantType _plantType;
        [SerializeField] private List<XRSimpleInteractable> _listPokeButton = new List<XRSimpleInteractable>();
        [SerializeField] private List<UIPokeInformation> _listInformation;
        private CanvasUIPoke _canvasUIPoke;

        [Header("Button Properties")]
        [SerializeField] private List<MeshRenderer> _buttonMaterial;
        [SerializeField] private Material _defaultMaterial;
        [SerializeField] private Material _hasInteractMaterial;

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
                _listPokeButton[i].firstSelectEntered.AddListener((SelectEnterEventArgs arg0) => OnFirstSelectEntered(arg0, index));

                if (_buttonMaterial[i] != null)
                {
                    _buttonMaterial[i].material = _defaultMaterial;
                }
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


        private void OnFirstSelectEntered(SelectEnterEventArgs arg0, int index)
        {
            if (_buttonMaterial[index] != null)
            {
                _buttonMaterial[index].material = _hasInteractMaterial;
                if(_plantType == PlantType.Tomat)
                {
                    _prosesPengenalanTumbuhan.ClearProses(index + 4);
                }
                else
                {
                    _prosesPengenalanTumbuhan.ClearProses(index);
                }
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
                    } else if (info.imageType == ImageType.ImageOnly)
                    {
                        if (info.spriteImage.Length != 0)
                        {
                            System.Array.Resize(ref info.spriteImage, 0);
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

        public Sprite backgroundImage;
        public ImageType imageType;
        public Sprite[] spriteImage;
    }

    public enum ImageType
    {
        ImageOnly,
        Single,
        Gif
    }
}


