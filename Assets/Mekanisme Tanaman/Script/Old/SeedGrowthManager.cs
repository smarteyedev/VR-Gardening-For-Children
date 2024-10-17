using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;
using UnityEngine.UI;

public class SeedGrowthManager : MonoBehaviour
{
    public GameObject[] treeStages; // Array untuk semua stage pohon
    public XRSocketInteractor[] seedSockets; // Daftar socket tempat benih ditanam
    public GameObject[] seeds; // Array untuk menyimpan benih (stage 0)
    public GameObject fertilizerPanelPrefab; // Prefab panel yang muncul setelah benih ditanam dan sebelum pupuk diberikan
    public GameObject wateringPanelPrefab; // Prefab panel yang muncul setelah benih diberi pupuk dan sebelum tanaman disiram
    public Transform cameraTransform; // Kamera utama dalam scene

    private bool[] isSocketOccupied; // Status apakah socket sudah terisi
    private int[] currentStage; // Menyimpan tahap pohon saat ini untuk setiap socket
    private bool[] isFertilized; // Menyimpan status apakah tanaman sudah diberi pupuk
    private GameObject[] fertilizerPanels; // Menyimpan panel yang sudah di-spawn
    private GameObject[] wateringPanels; // Menyimpan panel watering yang sudah di-spawn
    private GameObject[] currentSeeds; // Menyimpan referensi ke benih yang ditanam
    private Slider[] wateringSliders; // Menyimpan referensi ke slider pada panel watering
    private GameObject[] activeStages; // Menyimpan referensi ke stage yang aktif
    private int[] pickedFruits; // Menyimpan jumlah buah yang telah dipetik per pohon

    private void Start()
    {
        currentStage = new int[seedSockets.Length];
        isSocketOccupied = new bool[seedSockets.Length];
        isFertilized = new bool[seedSockets.Length];
        fertilizerPanels = new GameObject[seedSockets.Length];
        wateringPanels = new GameObject[seedSockets.Length];
        currentSeeds = new GameObject[seedSockets.Length];
        wateringSliders = new Slider[seedSockets.Length];
        activeStages = new GameObject[seedSockets.Length]; // Inisialisasi array stage aktif
        pickedFruits = new int[seedSockets.Length]; // Inisialisasi array untuk jumlah buah yang dipetik

        InitializeSocketListeners();
    }

    private void InitializeSocketListeners()
    {
        foreach (var socket in seedSockets)
        {
            socket.selectEntered.AddListener(OnSeedPlanted);
        }
    }

    private void OnDestroy()
    {
        foreach (var socket in seedSockets)
        {
            socket.selectEntered.RemoveListener(OnSeedPlanted);
        }
    }

    private void OnSeedPlanted(SelectEnterEventArgs args)
    {
        int socketIndex = System.Array.IndexOf(seedSockets, args.interactorObject);

        if (socketIndex >= 0 && !isSocketOccupied[socketIndex])
        {
            isSocketOccupied[socketIndex] = true;

            currentSeeds[socketIndex] = args.interactableObject.transform.gameObject;

            // Spawn a new fertilizer panel for this seed
            SpawnFertilizerPanel(socketIndex, seedSockets[socketIndex].transform);
        }
    }

    private void SpawnFertilizerPanel(int index, Transform socketTransform)
    {
        if (fertilizerPanels[index] == null)
        {
            Vector3 panelPosition = socketTransform.position + Vector3.up * 0.5f;
            fertilizerPanels[index] = Instantiate(fertilizerPanelPrefab, panelPosition, Quaternion.identity);
            fertilizerPanels[index].transform.LookAt(cameraTransform);
        }
    }

    private void SpawnWateringPanel(int index, Transform socketTransform)
    {
        if (wateringPanels[index] == null)
        {
            Vector3 panelPosition = socketTransform.position + Vector3.up * 0.5f;
            wateringPanels[index] = Instantiate(wateringPanelPrefab, panelPosition, Quaternion.identity);
            wateringPanels[index].transform.LookAt(cameraTransform);

            // Get the slider component from the panel
            wateringSliders[index] = wateringPanels[index].GetComponentInChildren<Slider>();
        }
    }

    public void OnFertilizerApplied(int socketIndex)
    {
        if (socketIndex >= 0 && socketIndex < isFertilized.Length && !isFertilized[socketIndex])
        {
            isFertilized[socketIndex] = true;

            StartCoroutine(ChangeTreeStageForSeed(socketIndex));
        }
    }

    private IEnumerator ChangeTreeStageForSeed(int socketIndex)
    {
        if (currentStage[socketIndex] < treeStages.Length - 1)
        {
            Transform socketTransform = seedSockets[socketIndex].transform;

            // Nonaktifkan benih atau stage sebelumnya
            if (currentStage[socketIndex] == 0 && currentSeeds[socketIndex] != null)
            {
                currentSeeds[socketIndex].SetActive(false);
            }
            else if (activeStages[socketIndex] != null)
            {
                activeStages[socketIndex].SetActive(false); // Nonaktifkan stage sebelumnya
            }

            // Aktifkan stage berikutnya
            GameObject nextStagePrefab = treeStages[currentStage[socketIndex] + 1];
            activeStages[socketIndex] = Instantiate(nextStagePrefab, socketTransform.position, socketTransform.rotation, socketTransform);

            currentStage[socketIndex]++;

            if (currentStage[socketIndex] == 1)
            {
                // Hapus panel fertilizer dan munculkan panel watering saat berubah ke stage 1
                Destroy(fertilizerPanels[socketIndex]);
                SpawnWateringPanel(socketIndex, socketTransform);
            }
            else if (currentStage[socketIndex] == 3)
            {
                // Hapus panel watering saat berubah ke stage 3
                if (wateringPanels[socketIndex] != null)
                {
                    Destroy(wateringPanels[socketIndex]);
                }

                // Munculkan event untuk memetik buah saat berada di stage 3
                RegisterFruitPickEvents(socketIndex);
            }

            yield return new WaitForSeconds(2f); // Tambahkan sedikit penundaan jika diperlukan
        }
    }

    private void RegisterFruitPickEvents(int socketIndex)
    {
        // Cari semua buah yang dapat dipetik (XRGrabInteractable) di dalam stage 3
        XRGrabInteractable[] fruits = activeStages[socketIndex].GetComponentsInChildren<XRGrabInteractable>();

        foreach (XRGrabInteractable fruit in fruits)
        {
            // Tambahkan listener untuk event ketika buah diambil
            fruit.selectExited.AddListener((args) => OnFruitPicked(socketIndex, fruit));
        }
    }

    private void OnFruitPicked(int socketIndex, XRGrabInteractable fruit)
    {
        pickedFruits[socketIndex]++; // Tambahkan jumlah buah yang dipetik

        // Cek jika semua buah telah dipetik (misalnya ada 3 buah)
        if (pickedFruits[socketIndex] >= 3)
        {
            StartCoroutine(ChangeToStage4AfterDelay(socketIndex));
        }
    }

    private IEnumerator ChangeToStage4AfterDelay(int socketIndex)
    {
        yield return new WaitForSeconds(3f); // Delay 3 detik sebelum berubah ke stage 4

        // Nonaktifkan stage 3 dan aktifkan stage 4
        if (activeStages[socketIndex] != null)
        {
            activeStages[socketIndex].SetActive(false);
        }

        Transform socketTransform = seedSockets[socketIndex].transform;
        GameObject stage4Prefab = treeStages[4]; // Asumsi stage 4 adalah tahap pohon berikutnya
        activeStages[socketIndex] = Instantiate(stage4Prefab, socketTransform.position, socketTransform.rotation, socketTransform);

        currentStage[socketIndex] = 4; // Update stage saat ini menjadi stage 4
    }

    public void UpdateWateringSlider(int socketIndex, float amount)
    {
        if (socketIndex >= 0 && socketIndex < wateringSliders.Length)
        {
            // Tambahkan nilai ke slider
            wateringSliders[socketIndex].value += amount;

            // Pastikan nilai slider tidak melebihi 1
            if (wateringSliders[socketIndex].value > 1)
            {
                wateringSliders[socketIndex].value = 1;
            }

            // Cek jika slider mencapai 0.5 dan stage saat ini adalah stage 1
            if (wateringSliders[socketIndex].value >= 0.5f && currentStage[socketIndex] == 1)
            {
                // Ubah ke stage 2
                StartCoroutine(ChangeTreeStageForSeed(socketIndex));
            }

            // Cek jika slider mencapai 1 dan stage saat ini adalah stage 2
            if (wateringSliders[socketIndex].value >= 1f && currentStage[socketIndex] == 2)
            {
                // Ubah ke stage 3
                StartCoroutine(ChangeTreeStageForSeed(socketIndex));
            }
        }
    }

    private void Update()
    {
        for (int i = 0; i < fertilizerPanels.Length; i++)
        {
            if (fertilizerPanels[i] != null)
            {
                fertilizerPanels[i].transform.position = seedSockets[i].transform.position + Vector3.up * 0.5f;
                fertilizerPanels[i].transform.LookAt(cameraTransform);
            }
        }

        for (int i = 0; i < wateringPanels.Length; i++)
        {
            if (wateringPanels[i] != null)
            {
                wateringPanels[i].transform.position = seedSockets[i].transform.position + Vector3.up * 0.5f;
                wateringPanels[i].transform.LookAt(cameraTransform);
            }
        }
    }
}
