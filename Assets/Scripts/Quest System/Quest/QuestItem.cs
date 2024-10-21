using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class QuestItem : MonoBehaviour
{
    public TextMeshProUGUI _numberText;
    public TextMeshProUGUI _descriptionText;
    public Toggle _doneIcon;
    public UnityEvent _onQuestDone;


    public void initializeItem(int number, string description, bool isDone, UnityEvent onQuestDone) {
        _numberText.text = number.ToString();
        _descriptionText.text = description;
        _doneIcon.isOn = isDone;
        _onQuestDone = onQuestDone;
    }

    public void MarkAsDone() {
        _doneIcon.isOn = true;
        _onQuestDone?.Invoke();
    }

    public bool IsDone() {
        return _doneIcon.isOn;
    }

    public void UpdateDesc(string description) {
       _descriptionText.text = description;
    }
}
