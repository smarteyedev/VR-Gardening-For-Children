using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FertilizerDestroyHandler : MonoBehaviour
{
    private FertilizerXRSpawner spawner;

    private void Start()
    {
        // Mencari spawner di scene
        spawner = FindObjectOfType<FertilizerXRSpawner>();

        if (spawner == null)
        {
            Debug.LogError("FertilizerXRSpawner tidak ditemukan di scene!");
        }
    }

    private void OnDestroy()
    {
        // Panggil event ketika pupuk ter-destroy
        if (spawner != null)
        {
            spawner.OnFertilizerDestroyed();
        }
    }
}


