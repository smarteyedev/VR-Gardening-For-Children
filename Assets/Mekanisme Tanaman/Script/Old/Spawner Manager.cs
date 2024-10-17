using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FertilizerXRSpawner : MonoBehaviour
{
    // Prefab pupuk yang akan di-spawn
    public GameObject fertilizerPrefab;

    // XR Socket tempat pupuk akan di-respawn
    public XRSocketInteractor socketInteractor;

    // Waktu delay sebelum respawn pupuk (misalnya 2 detik)
    public float respawnDelay = 2f;

    // Variabel untuk menyimpan objek pupuk yang sedang ada
    private GameObject currentFertilizer;

    // Timer untuk menghitung waktu ketika pupuk tidak ada di socket
    private float timer = 0f;

    // Apakah pupuk sedang diambil dari socket
    private bool isFertilizerDestroyed = false;

    // Start is called before the first frame update
    void Start()
    {
        // Spawn pupuk pertama kali ke dalam socket
        SpawnFertilizer();
    }

    // Update is called once per frame
    void Update()
    {
        // Jika pupuk tidak ada di socket dan clone sebelumnya di-destroy, respawn pupuk baru
        if (isFertilizerDestroyed)
        {
            timer += Time.deltaTime;

            // Jika sudah lebih dari respawnDelay, spawn pupuk kembali
            if (timer >= respawnDelay)
            {
                RespawnFertilizer();
                isFertilizerDestroyed = false; // Reset flag
                timer = 0f; // Reset timer
            }
        }
    }

    // Fungsi untuk men-spawn pupuk di socket
    void SpawnFertilizer()
    {
        // Pastikan pupuk tidak sedang ada
        if (currentFertilizer == null)
        {
            // Spawn pupuk baru
            currentFertilizer = Instantiate(fertilizerPrefab);
            
            // Pasangkan ke socket
            socketInteractor.StartManualInteraction(currentFertilizer.GetComponent<IXRSelectInteractable>());
        }
    }

    // Fungsi untuk respawn pupuk ke dalam socket
    void RespawnFertilizer()
    {
        if (currentFertilizer == null)
        {
            currentFertilizer = Instantiate(fertilizerPrefab);
            socketInteractor.StartManualInteraction(currentFertilizer.GetComponent<IXRSelectInteractable>());
        }
    }

    // Fungsi ini bisa dipanggil ketika pupuk ter-destroy
    public void OnFertilizerDestroyed()
    {
        // Tandai bahwa pupuk telah di-destroy
        isFertilizerDestroyed = true;
    }

    // Fungsi ini bisa dipanggil oleh event saat pupuk diambil dari socket (Select Exit Event)
    public void OnFertilizerRemoved()
    {
        if (currentFertilizer == null)
        {
            isFertilizerDestroyed = true;
        }
    }
}
