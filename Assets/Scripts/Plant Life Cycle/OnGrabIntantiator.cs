using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Smarteye
{
    public class OnGrabIntantiator : MonoBehaviour
    {
        public GameObject prefabToInstantiate;
        public Transform instantiatePosition;
        private XRGrabInteractable originalCube;

        void Start()
        {
            originalCube = GetComponent<XRGrabInteractable>();

            // Tambahkan event listener untuk saat cube asli di-grab
            originalCube.selectEntered.AddListener(OnGrab);
        }

        void OnGrab(SelectEnterEventArgs args)
        {
            GameObject instantiatedObject = Instantiate(prefabToInstantiate, instantiatePosition.position, instantiatePosition.rotation);

            XRGrabInteractable newGrabObject = instantiatedObject.GetComponent<XRGrabInteractable>();
            XRBaseInteractor currentInteractor = args.interactorObject as XRBaseInteractor;

            if (newGrabObject != null && currentInteractor != null)
            {
                currentInteractor.interactionManager.SelectExit(currentInteractor, originalCube);

                currentInteractor.interactionManager.SelectEnter(currentInteractor, newGrabObject);
            }
        }

        void OnDestroy()
        {
            // Hapus listener untuk mencegah memory leaks
            if (originalCube != null)
            {
                originalCube.selectEntered.RemoveListener(OnGrab);
            }
        }
    }
}
