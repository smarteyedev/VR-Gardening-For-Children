using UnityEngine;
using UnityEngine.UI;

public class WateringPanelManager : MonoBehaviour
{
    public Slider wateringSlider;

    public void UpdateSlider(float amount)
    {
        wateringSlider.value += amount;
        wateringSlider.value = Mathf.Clamp01(wateringSlider.value); // Pastikan nilai slider antara 0 dan 1
    }

    public float GetSliderValue()
    {
        return wateringSlider.value;
    }
}
