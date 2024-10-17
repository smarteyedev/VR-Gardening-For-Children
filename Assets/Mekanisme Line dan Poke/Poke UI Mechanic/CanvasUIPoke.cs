using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Smarteye
{
    public class CanvasUIPoke : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textTitle;
        [SerializeField] private TextMeshProUGUI _textDescription;
        [SerializeField] private Image _imageInformation;

        private UIPokeInformation _information;
        private bool isGifImage = false;
        private float _frameRate = 1f / 30f;

        public bool showUIInformation
        {
            set
            {
                gameObject.SetActive(value);
            }
        }

        public void SetUIInformation(UIPokeInformation information)
        {
            _information = information;

            if (_information.imageType == ImageType.Single)
            {
                isGifImage = false;
                _imageInformation.sprite = information.spriteImage[0];
            }
            else if(_information.imageType == ImageType.Gif)
            {
                isGifImage = true;
                StartCoroutine(PlayGIF());
            }
        }

        private void SetTextInformation()
        {
            _textTitle.text = _information.titleInformation;
            _textDescription.text = _information.description;
        }

        private IEnumerator PlayGIF()
        {
            Sprite[] sprites;
            sprites = _information.spriteImage;
            int index = 0;
            while (isGifImage)
            {
                _imageInformation.sprite = sprites[index];
                index = (index + 1) % sprites.Length;

                yield return new WaitForSeconds(_frameRate);
            }
        }
    }
}
