using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

namespace Smarteye.VRGardening.NPC
{
    public class DialogQuestionCanvas : MonoBehaviour
    {
        [Header("Staging")]
        [SerializeField] private DialogManager.DialogSection.DialogContent m_stagingData;

        [Header("Component Dependencies")]
        public DialogManager dialogManager;
        public Button closeBtn;
        public TextMeshProUGUI textOpeningDialog; // form npc to player

        [Space(4f)]
        public GameObject parentOptionBtn;
        public GameObject prefabOptionBtn;

        [Header("Answer Canvas Config")]
        [Space(4f)]
        public Transform parentGOTransform;
        public Transform instantiatePosition;

        private void Start()
        {
            if (closeBtn == null)
            {
                Debug.LogWarning("Close button is not assigned!");
                return;
            }

            closeBtn.onClick.AddListener(CloseDialogQuestionCanvas);
        }

        public void ShowDilogQuestion(DialogManager.DialogSection.DialogContent argData)
        {
            m_stagingData = argData;

            // Assign opening text
            textOpeningDialog.text = m_stagingData.openingDialogTitle;

            // Clear previous options
            if (parentOptionBtn != null)
            {
                foreach (Transform child in parentOptionBtn.transform)
                {
                    Destroy(child.gameObject);
                }
            }

            // Instantiate new options
            for (int i = 0; i < m_stagingData.QNAContents.Count; i++)
            {
                var itemData = m_stagingData.QNAContents[i].GetItemData();
                GameObject textBtn = Instantiate(prefabOptionBtn, parentOptionBtn.transform);

                if (itemData is DialogManager.DialogSection.DialogContent.QnAContent.CustomContent customContent)
                {
                    object[] contentDatas = new object[1] { m_stagingData.QNAContents[i].contentType };
                    AssignButtonListener(textBtn, () => OpenAnswerCanvas(customContent.formatUI, contentDatas));
                    textBtn.GetComponentInChildren<TextMeshProUGUI>().text = customContent.playerQuestion;
                }
                else if (itemData is DialogManager.DialogSection.DialogContent.QnAContent.AnserWithTextContent anserWithTextContent)
                {
                    object[] contentDatas = new object[2] { m_stagingData.QNAContents[i].contentType, anserWithTextContent.NpcAnswer };
                    AssignButtonListener(textBtn, () => OpenAnswerCanvas(anserWithTextContent.formatUI, contentDatas));
                    textBtn.GetComponentInChildren<TextMeshProUGUI>().text = anserWithTextContent.playerQuestion;
                }
                else if (itemData is DialogManager.DialogSection.DialogContent.QnAContent.AnserWithTextAndPhotoContent anserWithTextAndPhotoContent)
                {
                    object[] contentDatas = new object[4] { m_stagingData.QNAContents[i].contentType, anserWithTextAndPhotoContent.firstParagraph, anserWithTextAndPhotoContent.SecondParagraph, anserWithTextAndPhotoContent.illustarationImage };
                    AssignButtonListener(textBtn, () => OpenAnswerCanvas(anserWithTextAndPhotoContent.formatUI, contentDatas));
                    textBtn.GetComponentInChildren<TextMeshProUGUI>().text = anserWithTextAndPhotoContent.playerQuestion;
                }
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(parentOptionBtn.GetComponent<RectTransform>());
        }

        private void AssignButtonListener(GameObject button, Action listenerAction)
        {
            button.GetComponent<Button>().onClick.AddListener(() => listenerAction.Invoke());
        }

        public void OpenAnswerCanvas(DialogAnswerCanvas prefabCanvas, object[] argData)
        {
            DialogAnswerCanvas ansCom = Instantiate(prefabCanvas, instantiatePosition.position, instantiatePosition.rotation, parentGOTransform);
            ansCom.SetupAnswerCanvas(
                argDataAnswer: argData,
                BackToQuestionFunction: dialogManager.UpdateDialogeState
                );

            dialogManager.UpdateDialogeState(DialogManager.DialogState.AnsweringQuestion);
        }

        public void CloseDialogQuestionCanvas() => dialogManager.UpdateDialogeState(DialogManager.DialogState.DialogIdle);
    }
}
