using UnityEngine;

public class Soil : MonoBehaviour
{
    private bool isFertilized = false;
    private SeedGrowthManager seedGrowthManager;
    public int socketIndex;

    private void Start()
    {
        seedGrowthManager = FindObjectOfType<SeedGrowthManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fertilizer") && !isFertilized)
        {
            isFertilized = true;

            if (seedGrowthManager != null)
            {
                seedGrowthManager.OnFertilizerApplied(socketIndex);
            }

            Destroy(other.gameObject);
        }
        else if (other.CompareTag("WaterDroplet"))
        {
            if (seedGrowthManager != null)
            {
                // Tambahkan nilai slider pada Soil
                AddWateringSliderValue(0.05f);
            }
        }
    }

    // Tambahkan metode ini
    public void AddWateringSliderValue(float amount)
    {
        if (seedGrowthManager != null)
        {
            seedGrowthManager.UpdateWateringSlider(socketIndex, amount);
        }
    }
}
