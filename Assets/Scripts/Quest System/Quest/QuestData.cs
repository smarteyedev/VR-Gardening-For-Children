using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQuest;

public class QuestData : MonoBehaviour {
    public QuestController _questController;
    public NotificationItem _notificationItem;


    [Header("Quest List One")]
    public List<QuestController.ItemFormat> questEntriesOne = new List<QuestController.ItemFormat>(); 

    [Header("Quest List Two")]
    public List<QuestController.ItemFormat> questEntriesTwo = new List<QuestController.ItemFormat>();

    [Header("Dynamic Notification Content")]
    public List<string> dynamicTitles = new List<string>(); 
    public List<string> dynamicDescriptions = new List<string>(); 
    public List<Sprite> dynamicIcons = new List<Sprite>(); 

    private void Start() {
        if (dynamicTitles.Count > 0 && dynamicDescriptions.Count > 0) {
            UpdateNotification(0);  
        }
    }

    public void UpdateNotification(int index) {
        if (_notificationItem == null) {
            Debug.LogWarning("NotificationItem is not assigned.");
            return;
        }

        if (index < 0 || index >= dynamicDescriptions.Count || index >= dynamicTitles.Count) {
            Debug.LogWarning("Invalid index provided for notification update.");
            return;
        }

        string title = dynamicTitles[index];
        string description = dynamicDescriptions[index];
        Sprite icon = null; 

        if (index < dynamicIcons.Count) {
            icon = dynamicIcons[index];  
        }

        _notificationItem.UpdateNotification(icon, title, description);
    }
    public void AddFirstQuest() {
        _questController.ClearQuests();

        foreach (var questEntry in questEntriesOne) {
            if (string.IsNullOrEmpty(questEntry.description)) {
                Debug.LogError("Quest description cannot be empty.");
                continue; 
            }

            QuestController.ItemFormat newQuest = new QuestController.ItemFormat {
                number = _questController.questList.Count + 1, 
                description = questEntry.description,
                isDone = false, 
                onQuestDone = questEntry.onQuestDone
            };

            _questController.questList.Add(newQuest);
            //Debug.Log("Added new quest from List One: " + questEntry.description);
        }
    }

    public void AddSecondQuest() {
        _questController.ClearQuests();

        foreach (var questEntry in questEntriesTwo) {
            if (string.IsNullOrEmpty(questEntry.description)) {
                Debug.LogError("Quest description cannot be empty.");
                continue; 
            }

            QuestController.ItemFormat newQuest = new QuestController.ItemFormat {
                number = _questController.questList.Count + 1, 
                description = questEntry.description,
                isDone = false,
                onQuestDone = questEntry.onQuestDone
            };

            _questController.questList.Add(newQuest);
            //Debug.Log("Added new quest from List Two: " + questEntry.description);
        }
    }
}
