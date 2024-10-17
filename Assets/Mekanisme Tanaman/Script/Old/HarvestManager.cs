using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro; // Untuk menggunakan TextMeshPro

public class HarvestManager : MonoBehaviour
{
    public XRSocketInteractor[] sockets; // Array dari semua socket
    public TextMeshProUGUI scoreText; // Text UI untuk menampilkan score
    private int currentScore = 0; // Nilai awal score
    private int maxScore = 18; // Nilai maksimal score

    private void Start()
    {
        // Inisialisasi listener untuk setiap socket
        foreach (var socket in sockets)
        {
            socket.selectEntered.AddListener(OnObjectPlacedInSocket);
            socket.selectExited.AddListener(OnObjectRemovedFromSocket);
        }

        // Update UI pertama kali
        UpdateScoreUI();
    }

    private void OnDestroy()
    {
        // Hapus listener saat objek dihancurkan untuk mencegah memory leak
        foreach (var socket in sockets)
        {
            socket.selectEntered.RemoveListener(OnObjectPlacedInSocket);
            socket.selectExited.RemoveListener(OnObjectRemovedFromSocket);
        }
    }

    // Fungsi ini dipanggil ketika objek ditempatkan di socket
    private void OnObjectPlacedInSocket(SelectEnterEventArgs args)
    {
        // Tambahkan score saat socket diisi
        if (currentScore < maxScore)
        {
            currentScore++;
        }

        // Update tampilan UI
        UpdateScoreUI();
    }

    // Fungsi ini dipanggil ketika objek dikeluarkan dari socket
    private void OnObjectRemovedFromSocket(SelectExitEventArgs args)
    {
        // Kurangi score saat objek dikeluarkan dari socket
        if (currentScore > 0)
        {
            currentScore--;
        }

        // Update tampilan UI
        UpdateScoreUI();
    }

    // Fungsi untuk memperbarui tampilan UI score
    private void UpdateScoreUI()
    {
        // Menampilkan score sebagai "currentScore / maxScore"
        scoreText.text = $"{currentScore}/{maxScore}";
    }
}
