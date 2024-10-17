using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Fertilizer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Pastikan benih memiliki tag "Seed"
        if (other.CompareTag("Seed"))
        {
            // Cari SeedGrowthManager di scene
            SeedGrowthManager seedGrowthManager = FindObjectOfType<SeedGrowthManager>();
            FertilizerPanelManager fertilizerPanelManager = FindObjectOfType<FertilizerPanelManager>();

            if (seedGrowthManager == null)
            {
                Debug.LogError("SeedGrowthManager tidak ditemukan di scene.");
                return;
            }

            if (fertilizerPanelManager == null)
            {
                Debug.LogError("FertilizerPanelManager tidak ditemukan di scene.");
                return;
            }

            // Cari socket index berdasarkan parent dari benih
            XRSocketInteractor socket = other.transform.parent?.GetComponent<XRSocketInteractor>();

            if (socket == null)
            {
                return;
            }

            int socketIndex = System.Array.IndexOf(seedGrowthManager.seedSockets, socket);

            if (socketIndex >= 0)
            {
                // Beri tahu SeedGrowthManager bahwa pupuk telah diterapkan
                seedGrowthManager.OnFertilizerApplied(socketIndex);

                // Hapus atau nonaktifkan pupuk setelah digunakan
                gameObject.SetActive(false);
            }
            else
            {
                Debug.LogError("Socket tidak ditemukan dalam array seedSockets.");
            }
        }
    }
}
