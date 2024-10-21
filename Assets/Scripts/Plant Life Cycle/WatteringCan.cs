using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WateringCanSpray : MonoBehaviour
{
    public GameObject waterDropletPrefab; // Prefab untuk tetesan air
    public Transform sprayPoint; // Titik keluarnya tetesan air
    public float dropletSpeed = 0f; // Kecepatan tetesan air
    public float fireRate = 0.1f; // Waktu antar tetesan air

    private float nextFire = 0f;
    private XRGrabInteractable grabInteractable; // Komponen XR Grab Interactable
    private bool isGrabbed = false; // Status apakah objek sedang di-grab

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrab);
            grabInteractable.selectExited.AddListener(OnRelease);
        }
        else
        {
            Debug.LogError("XR Grab Interactable tidak ditemukan pada objek!");
        }
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        isGrabbed = true;
    }

    void OnRelease(SelectExitEventArgs args)
    {
        isGrabbed = false;
    }

    void Update()
    {
        if (isGrabbed && Input.GetMouseButton(0) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Spray();
        }
    }

    void Spray()
    {
        if (waterDropletPrefab == null)
        {
            Debug.LogError("Prefab tetesan air tidak terpasang di inspector!");
            return;
        }

        // Membuat tetesan air dari prefab jika prefab valid
        GameObject droplet = Instantiate(waterDropletPrefab, sprayPoint.position, sprayPoint.rotation);
        if (droplet != null)
        {
            Rigidbody rb = droplet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = sprayPoint.forward * dropletSpeed;
            }
            else
            {
                Debug.LogError("Prefab tetesan air tidak memiliki komponen Rigidbody!");
            }
        }
        else
        {
            Debug.LogError("Gagal membuat tetesan air!");
        }
    }
}
