using Seville;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

namespace Smarteye
{
    public class PlantController : MonoBehaviour
    {
        public PlantGrowStatus _growthStatus;
        private PlantType _plantType;
        [SerializeField] private Transform _modelTransform;
        private PlantingManager _plantingManager;
        private SESocketInteractor _socketInteractor;
        private GameObject _activePlantModel = null;
        private GameObject _activeSeed = null;
        private CanvasPlantController _canvasPlantController;

        [Header("Progres Properties")]
        [SerializeField] private float _progresWaterMultipiler = 0.2f;
        [SerializeField] private float _fertilizerProgresMultipiler = 1;
        private float _waterProgres = 0;
        private float _fertilizerProgres = 0;

        [Header("Harvest Properties")]
        [SerializeField] private int _harvestAmount;

        private void Start()
        {
            _plantingManager = FindAnyObjectByType<PlantingManager>();
            _socketInteractor = GetComponentInParent<SESocketInteractor>();
            _canvasPlantController = GetComponentInChildren<CanvasPlantController>();
            _canvasPlantController.gameObject.SetActive(false);

            _socketInteractor.selectEntered.AddListener(OnInitializePlant);
        }

        public void OnInitializePlant(SelectEnterEventArgs args)
        {
            _growthStatus = PlantGrowStatus.Bibit;
            _activePlantModel = _socketInteractor.GetOldestInteractableSelected().transform.gameObject;
            _activeSeed = _activePlantModel;

            _canvasPlantController.gameObject.SetActive(true);

            _waterProgres = 0;
            _fertilizerProgres = 0;
            UpdateSliderProgres();

            _canvasPlantController.panelFertilizer = true;
            _canvasPlantController.panelWater = false;
            _canvasPlantController.SetPlantGrowStatus(_growthStatus.ToString());
            _canvasPlantController.ForceRefreshLayout();

            PlantSeed plantScript = _activePlantModel.GetComponent<PlantSeed>();
            _plantType = plantScript.plantType;
            _canvasPlantController.SetPlantName("Tanaman " + _plantType);
        }

        public void ClearEnteredSocket()
        {
            _socketInteractor.interactablesSelected.Clear();
            
            Destroy(_activePlantModel);
        }

        public void OnTriggerEnter(Collider other)
        {
            if (_growthStatus == PlantGrowStatus.None)
                return;

            if (other.CompareTag("Water"))
            {
                if (_growthStatus == PlantGrowStatus.Bibit) {
                    //Action Negatif Disini
                    return;
                }

                _waterProgres += _progresWaterMultipiler;
                OnTriggerProses(other.gameObject);
            }
            else if(other.CompareTag("Fertilizer")){
                if(_growthStatus == PlantGrowStatus.Bibit)
                {
                    _fertilizerProgres += _fertilizerProgresMultipiler;
                    OnTriggerProses(other.gameObject);
                }
                else
                {
                    //Action Negatif Disini
                }
            }
        }

        private void OnTriggerProses(GameObject objTrigger)
        {
            UpdateSliderProgres();
            CheckProgresGrow();

            Destroy(objTrigger);
        }

        private void UpdateSliderProgres()
        {
            _canvasPlantController.sliderWater = _waterProgres;
            _canvasPlantController.sliderFertilizer = _fertilizerProgres;
        }

        private void CheckProgresGrow()
        {
            if(_growthStatus == PlantGrowStatus.Bibit)
            {
                if (_fertilizerProgres >= 1)
                {
                    _growthStatus = PlantGrowStatus.TanamanKecil;
                    UpdatePlantModel();
                    _canvasPlantController.panelWater = true;
                }
            }
            else if (_growthStatus == PlantGrowStatus.TanamanKecil)
            {
                if(_waterProgres >= 1 && _waterProgres < 2)
                {
                    _growthStatus = PlantGrowStatus.Berbunga;
                    UpdatePlantModel();
                }
            }else if(_growthStatus == PlantGrowStatus.Berbunga)
            {
                if(_waterProgres >= 2)
                {
                    _growthStatus = PlantGrowStatus.Berbuah;
                    UpdatePlantModel();
                    ShowPlantResult();
                }
            }
        }
        
        private void UpdatePlantModel()
        {
            GameObject instantiatedPlant = Instantiate(_plantingManager.GetPlantModel(_plantType, _growthStatus), _modelTransform);
            instantiatedPlant.transform.localPosition = Vector3.zero;
            Destroy(_activePlantModel);

            _activePlantModel = instantiatedPlant;

            if(_growthStatus == PlantGrowStatus.TanamanKecil)
            {
                _canvasPlantController.SetPlantGrowStatus("Tanaman Kecil");
            }
            else
            {
                _canvasPlantController.SetPlantGrowStatus(_growthStatus.ToString());
            }

            _canvasPlantController.ForceRefreshLayout();
        }

        private void ShowPlantResult()
        {
            PlantTree plantTree = _activePlantModel.GetComponent<PlantTree>();
            plantTree.Initialize(this, _harvestAmount, _modelTransform);

            ShowNotification();
        }

        public void HarvestPlant()
        {
            _growthStatus = PlantGrowStatus.Layu;
            UpdatePlantModel();
            ShowNotification();
        }

        private void ShowNotification()
        {
            if(_growthStatus == PlantGrowStatus.Berbuah)
            {
                _canvasPlantController.ShowHarvestNotification();
                _canvasPlantController.isNotificationCanInteract = false;
            }
            else if (_growthStatus == PlantGrowStatus.Layu)
            {
                _canvasPlantController.ShowCleanNotification();
                _canvasPlantController.isNotificationCanInteract = true;
            }
        }

        public void OnNotificationPressed()
        {
            if (_growthStatus == PlantGrowStatus.Layu)
            {
                CleanPlanting();
            }
        }

        private void CleanPlanting()
        {
            _canvasPlantController.imageNotification = false;

            _waterProgres = 0;
            _fertilizerProgres = 0;

            Destroy(_activeSeed);
            Destroy(_activePlantModel);
            _growthStatus = PlantGrowStatus.None;

            _canvasPlantController.isNotificationCanInteract = false;
            _canvasPlantController.gameObject.SetActive(false);
        }
    }
}

