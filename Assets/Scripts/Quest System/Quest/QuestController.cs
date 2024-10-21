using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SQuest {
    public class QuestController : MonoBehaviour {
        public static QuestController Instance { get; private set; }

        [HideInInspector] public List<ItemFormat> questList = new List<ItemFormat>();  // Data for quests
        private List<QuestItem> generatedQuestItems = new List<QuestItem>(); // Track generated quest items
        public Transform questParent;
        public QuestItem questItemPrefabs;


        private bool firstQuestCompleted = false;
        private bool secondQuestCompleted = false;

        [System.Serializable]
        public struct ItemFormat {
            [HideInInspector] public int number;
            [TextArea]
            public string description;
            public bool isDone;
            public UnityEvent onQuestDone;
        }

        public UnityEvent onFinishAllQuests;


        private void Awake() {
            if (Instance == null) {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            } else {
                Destroy(gameObject);
            }
        }

        public void InstantiateQuests() {
            for (int i = 0; i < questList.Count; i++) {
                var quest = questList[i];
                QuestItem questItem = Instantiate(questItemPrefabs, questParent);
                int displayNumber = i + 1;
                questItem.initializeItem(displayNumber, quest.description, quest.isDone, quest.onQuestDone);
                generatedQuestItems.Add(questItem);
            }
        }

        public void FinishQuest(int questNumber) {
            QuestItem questToFinish = null;

            for (int i = 0; i < generatedQuestItems.Count; i++) {
                if (int.TryParse(generatedQuestItems[i]._numberText.text, out int currentQuestNumber) && currentQuestNumber == questNumber) {
                    questToFinish = generatedQuestItems[i];
                    break;
                }
            }

            if (questToFinish != null) {
                questToFinish.MarkAsDone();

                if (AreAllQuestsDone()) {
                    Debug.Log("All quests are completed!");
                    //ClearQuests();
                    onFinishAllQuests?.Invoke();

                    if (firstQuestCompleted) {
                        secondQuestCompleted = true;
                    } else if (!firstQuestCompleted) {
                        firstQuestCompleted = true;
                    }

                }
            } else {
                Debug.LogError("Quest with number " + questNumber + " not found.");
            }
        }

        private bool AreAllQuestsDone() {
            foreach (var questItem in generatedQuestItems) {
                if (!questItem.IsDone()) {  // Assuming QuestItem has a property to check if it's done
                    return false;  // Return false if any quest item is not done
                }
            }
            return true;  // All quest items are done
        }


        public void EditDescription(int questNumber, string description) {
            QuestItem questToEdit = null;

            for (int i = 0; i < generatedQuestItems.Count; i++) {
                if (int.TryParse(generatedQuestItems[i]._numberText.text, out int currentQuestNumber) && currentQuestNumber == questNumber) {
                    questToEdit = generatedQuestItems[i];
                    break;
                }
            }

            if (questToEdit != null) {
                questToEdit.UpdateDesc(description);
            } else {
                Debug.LogError("Quest with number " + questNumber + " not found.");
            }
        }

        // New method to clear all quests
        public void ClearQuests() {
            for (int i = generatedQuestItems.Count - 1; i >= 0; i--) {
                QuestItem questItem = generatedQuestItems[i];
                Destroy(questItem.gameObject);
            }

            generatedQuestItems.Clear();
            questList.Clear();
            Debug.Log("All quests cleared.");
        }

        public bool isFisrstQuestCompleted() {
            return firstQuestCompleted;
        }
        public bool isSecondQuestCompleted() {
            return secondQuestCompleted;
        }
    }

}
