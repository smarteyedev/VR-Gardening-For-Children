using UnityEngine;
using UnityEngine.UI; // Untuk Slider
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // TextMeshPro untuk menampilkan skor
    public XRSocketInteractor[] seedSockets; // Daftar socket untuk menanam benih
    public Slider plantingSlider; // Slider pada panel planting

    private int totalSockets;
    private int filledSockets;

    private void Start()
    {
        totalSockets = seedSockets.Length;
        filledSockets = 0;
        UpdateScoreText();
        InitializeSocketListeners();
        UpdateSliderValue();
    }

    private void InitializeSocketListeners()
    {
        foreach (var socket in seedSockets)
        {
            socket.selectEntered.AddListener(OnSeedPlaced);
        }
    }

    private void OnDestroy()
    {
        foreach (var socket in seedSockets)
        {
            socket.selectEntered.RemoveListener(OnSeedPlaced);
        }
    }

    private void OnSeedPlaced(SelectEnterEventArgs args)
    {
        if (filledSockets < totalSockets)
        {
            filledSockets++;
            UpdateScoreText();
            UpdateSliderValue();
        }
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = $"{filledSockets}/{totalSockets}";
        }
    }

    private void UpdateSliderValue()
    {
        if (plantingSlider != null)
        {
            float value = 0f;
            switch (filledSockets)
            {
                case 1:
                    value = 0.160f;
                    break;
                case 2:
                    value = 0.320f;
                    break;
                case 3:
                    value = 0.510f;
                    break;
                case 4:
                    value = 0.670f;
                    break;
                case 5:
                    value = 0.850f;
                    break;
                case 6:
                    value = 1f;
                    break;
            }
            plantingSlider.value = value;
        }
    }
}
