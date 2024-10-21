using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQuest;

public class DummyAddScript : MonoBehaviour
{
    public string description;
    public int questNumber;
    private int numberToTrack = 0;

    public void AddNumber() {
        if(numberToTrack != 3) {
            numberToTrack++;
            QuestController.Instance.EditDescription(questNumber, description + "(" + numberToTrack.ToString() + "/3)");
            if (numberToTrack == 3) {
                QuestController.Instance.FinishQuest(questNumber);
            }
        } else {
            return;
        }
    }
}
