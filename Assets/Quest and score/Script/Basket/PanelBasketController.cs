using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQuest;

public class PanelBasketController : MonoBehaviour
{
    [SerializeField] private GameObject _oneItemPanel;
    [SerializeField] private GameObject _twoItemPanel;
    [SerializeField] private GameObject _endlessItemPanel;
    [SerializeField] private GameObject _itemPanelGroup;

    public void CheckQuestState() {
        if (QuestController.Instance.isSecondQuestCompleted()) {
            ShowEndlessItemPanel();
        } else {
            ShowTwoItemPanel();
        } 
    }
    public void ShowOneItemPanel() {
        _oneItemPanel.SetActive(true);
        _twoItemPanel.SetActive(false);
        _endlessItemPanel.SetActive(false);
    }

    public void ShowTwoItemPanel() {
        _oneItemPanel.SetActive(false);
        _twoItemPanel.SetActive(true);
        _endlessItemPanel.SetActive(false);
    }

    public void ShowEndlessItemPanel() {
        _itemPanelGroup.SetActive(false);
        _oneItemPanel.SetActive(false);
        _twoItemPanel.SetActive(false);
        _endlessItemPanel.SetActive(true);
    }
}
