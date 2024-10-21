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
        [SerializeField] private Image _backgroundImage;

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

            if (_information.imageType == ImageType.ImageOnly)
            {
                isGifImage = false;
                SetTextInformation();
                VisibleOtherUI(false);
            }
            else if (_information.imageType == ImageType.Single)
            {
                isGifImage = false;
                VisibleOtherUI(true);
                SetTextInformation();
            }
            else if(_information.imageType == ImageType.Gif)
            {
                isGifImage = true;
                VisibleOtherUI(false);
                StartCoroutine(PlayGIF());
            }
        }

        private void SetTextInformation()
        {
            _textTitle.text = _information.titleInformation;
            _textDescription.text = _information.description;
            _backgroundImage.gameObject.SetActive(false);
        }

        private void VisibleOtherUI(bool isVisible)
        {
            _textTitle.gameObject.SetActive(isVisible);
            _textDescription.gameObject.SetActive(isVisible);

            if (_information.backgroundImage != null)
            {
                _backgroundImage.gameObject.SetActive(true);
                _backgroundImage.sprite = _information.backgroundImage;
            }
            else
            {
                _backgroundImage.gameObject.SetActive(false);
            }

            if (_information.spriteImage.Length > 0)
            {
                _imageInformation.gameObject.SetActive(true);
                _imageInformation.sprite = _information.spriteImage[0];
            }
            else
            {
                _imageInformation.gameObject.SetActive(false);
            }
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
