using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using SQuest;

public class QuestCanvasController : MonoBehaviour {
    [Header("Camera")]
    [SerializeField] Camera _playerCamera;

    [Header("Notification Panel")]
    [SerializeField] QuestData _questData;

    [Header("UI Panels")]
    [SerializeField] GameObject _panelInformation;
    [SerializeField] GameObject _panelScore;
    [SerializeField] GameObject _panelQuest;
    [SerializeField] GameObject _panelNotification;

    [Header("UI Text")]
    [SerializeField] TextMeshProUGUI _scoreText;

    private void Update() {
        _scoreText.text = GameManager.Instance.GetScore().ToString();
        //_tomatoCountText.text = GameManager.Instance.GetTomatoCount().ToString();
        //_orangeCountText.text = GameManager.Instance.GetOrangeCount().ToString();
    }
    public void ShowInformationPanel() {
        _panelInformation.SetActive(true);
        _panelScore.SetActive(false);
        _panelQuest.SetActive(false);
        _panelNotification.SetActive(false);

    }

    public void ShowScorePanel() {
        _panelInformation.SetActive(false);
        _panelScore.SetActive(true);
        _panelQuest.SetActive(false);
        _panelNotification.SetActive(false);

    }
    public void ShowQuestPanel() {
        _panelInformation.SetActive(false);
        _panelScore.SetActive(false);
        _panelQuest.SetActive(true);
        _panelNotification.SetActive(false);

    }

    public void ShowNotificationPanel() {
        StartCoroutine(DelayNotificationPanel());
    }

    IEnumerator DelayNotificationPanel() {
        yield return new WaitForSeconds(1f);
        //PositionPanelInFrontOfPlayer(_panelNotification.transform.parent.gameObject);
        if (QuestController.Instance.isSecondQuestCompleted()) {
            _questData.UpdateNotification(1);
        }
        _panelNotification.SetActive(true);
        _panelInformation.SetActive(false);
        _panelScore.SetActive(false);
    }

    public void ShowNextQuestPanel() {
        if (QuestController.Instance.isSecondQuestCompleted()) {
            ShowScorePanel();
        } else {
            ShowQuestPanel();
        }
    }

    private void PositionPanelInFrontOfPlayer(GameObject panel) {
        Vector3 cameraForward = _playerCamera.transform.forward;
        Vector3 newPosition = _playerCamera.transform.position + cameraForward * 0.5f;
        cameraForward.y = 0;
        cameraForward.Normalize();
        panel.transform.position = newPosition;
        panel.transform.rotation = Quaternion.LookRotation(cameraForward);
    }
}