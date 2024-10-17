using UnityEngine;

public class FertilizerPanelManager : MonoBehaviour
{
    public GameObject fertilizerPanelPrefab; // Prefab panel pupuk
    private GameObject activePanel; // Panel yang sedang aktif
    private int currentSocketIndex = -1; // Indeks socket saat ini

    // Referensi ke SeedGrowthManager untuk memberi tahu ketika pupuk diterapkan
    public SeedGrowthManager seedGrowthManager;

    public void ShowPanel(Transform targetTransform, int socketIndex)
    {
        // Jika panel belum ada, instansiasi dari prefab
        if (activePanel == null)
        {
            activePanel = Instantiate(fertilizerPanelPrefab);
        }

        // Aktifkan panel dan tempatkan di posisi benih
        activePanel.SetActive(true);
        activePanel.transform.position = targetTransform.position + Vector3.up * 0.5f; // Offset agar terlihat di atas benih

        currentSocketIndex = socketIndex;
    }

    public void HidePanel()
    {
        if (activePanel != null)
        {
            activePanel.SetActive(false);
            currentSocketIndex = -1;
        }
    }

    // Method yang dipanggil oleh panel ketika pupuk diterapkan
    public void ApplyFertilizer()
    {
        if (currentSocketIndex >= 0)
        {
            seedGrowthManager.OnFertilizerApplied(currentSocketIndex);
            HidePanel();
        }
    }
}
