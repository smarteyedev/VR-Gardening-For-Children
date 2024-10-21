using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Smarteye
{
    public class RotateTowardPlayer : MonoBehaviour
    {
        [SerializeField] private float rotationYOffset = 180f; // Offset rotasi di sumbu Y
        private Transform cameraTransform;  // Transform dari Main Camera

        void Start()
        {
            // Cari Main Camera di scene
            if (Camera.main != null)
            {
                cameraTransform = Camera.main.transform;
            }
            else
            {
                Debug.LogError("Main Camera not found. Please ensure there is a camera tagged as 'MainCamera'.");
            }
        }

        void Update()
        {
            if (cameraTransform != null)
            {
                // Buat object selalu menghadap ke camera, tapi hanya di sumbu Y
                Vector3 targetPosition = new Vector3(cameraTransform.position.x, transform.position.y, cameraTransform.position.z);

                // Rotasikan object untuk menghadap target posisi
                transform.LookAt(targetPosition);

                // Tambahkan offset rotasi di sumbu Y
                transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y + rotationYOffset, 0);
            }
        }
    }
}
