using Seville;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Smarteye
{
    public class PlantTree : MonoBehaviour
    {
        private int _harvestAmount;
        [SerializeField] private GameObject _fruitPrefab;
        [SerializeField] private Transform[] _fruitPlace;
        [SerializeField] private PlantType _plantType;
        private PlantController _plantController;
        private Transform _harvestSocket;

        private List<XRGrabInteractableTwoAttach> _listHarvest = new List<XRGrabInteractableTwoAttach>();

        public void Initialize(PlantController plantController, int harvestAmount, Transform harvestSocket)
        {
            _plantController = plantController;
            _harvestAmount = harvestAmount;
            _harvestSocket = harvestSocket;
            InstantiateHarvest();
        }

        public void InstantiateHarvest()
        {
            int placeCount = _fruitPlace.Length;

            for (int i = 0; i < _harvestAmount; i++)
            {
                int placeIndex = i % placeCount;

                GameObject fruit = Instantiate(_fruitPrefab, _fruitPlace[placeIndex].position, Quaternion.identity);
                Rigidbody rb = fruit.GetComponent<Rigidbody>();

                rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;

                fruit.transform.SetParent(_fruitPlace[placeIndex]);

                fruit.transform.localPosition = Vector3.zero;
                fruit.transform.localRotation = Quaternion.identity;
            }

            GetHarvestList();
        }

        private void GetHarvestList()
        {
            XRGrabInteractableTwoAttach[] list = GetComponentsInChildren<XRGrabInteractableTwoAttach>();
            _listHarvest.AddRange(list);

            foreach (var grabInteractable in _listHarvest)
            {
                grabInteractable.selectEntered.AddListener(OnGrabbed);

                PlantSeed seed = grabInteractable.gameObject.AddComponent<PlantSeed>();
                seed.plantType = _plantType;

                grabInteractable.transform.SetParent(_harvestSocket);
            }
        }

        private void OnGrabbed(SelectEnterEventArgs args)
        {
            XRGrabInteractableTwoAttach grabbedObject = args.interactableObject.transform.GetComponent<XRGrabInteractableTwoAttach>();

            if (grabbedObject != null)
            {
                Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.None;

                _listHarvest.Remove(grabbedObject);
                
                grabbedObject.selectEntered.RemoveListener(OnGrabbed);
                CheckHarvestAmount();
            }
        }

        private void CheckHarvestAmount()
        {
            if(_listHarvest.Count <= 0)
            {
                Debug.Log("Sudah Habis");
                _plantController.HarvestPlant();
            }
            else
            {
                Debug.Log("Masih Sisa - " + _listHarvest.Count);
            }
        }
    }
}
