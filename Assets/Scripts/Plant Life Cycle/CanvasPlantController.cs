using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Smarteye
{
    public class CanvasPlantController : MonoBehaviour
    {
        private PlantController _plantController;

        [SerializeField] private TextMeshProUGUI _textPlantName;
        [SerializeField] private TextMeshProUGUI _plantGrowStatus;

        [SerializeField] private Slider _sliderWater;
        [SerializeField] private Slider _sliderFertilizer;
        [SerializeField] private RectTransform _headerRectTransform;

        private GameObject _panelWater;
        private GameObject _panelFertilizer;

        [Header("Notification Properties")]
        [SerializeField] private Image _imageNotification;
        private Button _buttonNotification;
        [SerializeField] private Sprite _harveshSprite;
        [SerializeField] private Sprite _cleanSprite;



        public float sliderWater { 
            set
            {
                _sliderWater.value = value;
            }
            get
            {
                return _sliderWater.value;
            }
        }

        public float sliderFertilizer
        {
            set {
                _sliderFertilizer.value = value;
            }
            get
            {
                return _sliderFertilizer.value;
            }
        }

        public bool panelWater
        {
            set
            {
                _panelWater.SetActive(value);
            }
        }

        public bool panelFertilizer
        {
            set
            {
                _panelFertilizer.SetActive(value);
            }
        }

        public bool imageNotification
        {
            set
            {
                _imageNotification.gameObject.SetActive(value);
            }
        }

        public bool isNotificationCanInteract
        {
            set
            {
                _imageNotification.raycastTarget = value;
            }
        }

        private void Start()
        {
            _panelWater = _sliderWater.transform.parent.gameObject;
            _panelFertilizer = _sliderFertilizer.transform.parent.gameObject;
            _buttonNotification = _imageNotification.GetComponent<Button>();
            _plantController = GetComponentInParent<PlantController>();

            _buttonNotification.onClick.AddListener(OnClickedNotification);
            _imageNotification.gameObject.SetActive(false);
        }

        public void ShowHarvestNotification()
        {
            _imageNotification.sprite = _harveshSprite;
            _imageNotification.gameObject.SetActive(true);
        }

        public void ShowCleanNotification()
        {
            _imageNotification.sprite = _cleanSprite;
            _imageNotification.gameObject.SetActive(true);
        }

        public void SetPlantName(string name)
        {
            _textPlantName.text = name;
        }

        public void SetPlantGrowStatus(string growStatus)
        {
            _plantGrowStatus.text = growStatus;
        }

        private void OnClickedNotification()
        {
            _plantController.OnNotificationPressed();
        }

        public void ForceRefreshLayout()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(_headerRectTransform);
        }

    }
}
