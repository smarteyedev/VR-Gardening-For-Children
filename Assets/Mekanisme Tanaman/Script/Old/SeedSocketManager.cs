using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SeedSocketManager : MonoBehaviour
{
    public XRSocketInteractor[] seedSockets;
    public GameObject seedPrefab;
    private Vector3 originalScale;

    void Start()
    {
        originalScale = seedPrefab.transform.localScale;
        foreach (var socket in seedSockets)
        {
            socket.selectEntered.AddListener(OnSeedPlaced);
        }
    }

    private void OnSeedPlaced(SelectEnterEventArgs args)
    {
        // Mengatur kembali skala benih ke skala asli setelah dipasang pada socket
        args.interactableObject.transform.localScale = originalScale;
    }
}
