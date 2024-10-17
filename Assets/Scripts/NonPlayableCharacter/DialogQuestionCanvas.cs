using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Smarteye.VRGardening.NPC
{
    public class DialogQuestionCanvas : MonoBehaviour
    {
        [Header("Staging")]
        [SerializeField] private DialogManager.DialogSection.DialogContent m_stagingData;

        [Header("Component Dependencies")]
        public TextMeshProUGUI textOpeningDialog; // form npc to plyer

        [Space(4f)]
        public GameObject parentOptionBtn;
        public GameObject prefabOptionBtn;

        [Header("Answer Canvas Config")]
        [Space(4f)]
        public Transform parentGOTransform;
        public Transform instantiatePosition;

        public void ShowDilogQuestion(DialogManager.DialogSection.DialogContent argData)
        {
            m_stagingData = argData;

            DialogQuestionVisibility(DialogManager.DialogState.OpeningQuestion);

            //? asign opening text
            textOpeningDialog.text = m_stagingData.openingDialogTitle;

            if (parentOptionBtn != null)
            {
                // Hapus semua child dari parent
                foreach (Transform child in parentOptionBtn.transform)
                {
                    Destroy(child.gameObject);
                }
            }

            for (int i = 0; i < m_stagingData.QNAContents.Count; i++)
            {
                var itemData = m_stagingData.QNAContents[i].GetItemData();

                GameObject textBtn = Instantiate(prefabOptionBtn, parentOptionBtn.transform);

                if (itemData is DialogManager.DialogSection.DialogContent.QnAContent.CustomContent customContent)
                {
                    object[] contentDatas = new object[1] { m_stagingData.QNAContents[i].contentType };

                    //? asign button behaviour
                    textBtn.GetComponent<Button>().onClick.AddListener(() => OpenAnswerCanvas(customContent.formatUI, contentDatas));
                    textBtn.GetComponentInChildren<TextMeshProUGUI>().text = customContent.playerQuestion;
                }
                else if (itemData is DialogManager.DialogSection.DialogContent.QnAContent.AnserWithTextContent anserWithTextContent)
                {
                    object[] contentDatas = new object[2] { m_stagingData.QNAContents[i].contentType, anserWithTextContent.NpcAnswer };

                    //? asign button behaviour
                    textBtn.GetComponent<Button>().onClick.AddListener(() => OpenAnswerCanvas(anserWithTextContent.formatUI, contentDatas));
                    textBtn.GetComponentInChildren<TextMeshProUGUI>().text = anserWithTextContent.playerQuestion;
                }
                else if (itemData is DialogManager.DialogSection.DialogContent.QnAContent.AnserWithTextAndPhotoContent anserWithTextAndPhotoContent)
                {
                    object[] contentDatas = new object[4] { m_stagingData.QNAContents[i].contentType, anserWithTextAndPhotoContent.firstParagraph, anserWithTextAndPhotoContent.SecondParagraph, anserWithTextAndPhotoContent.illustarationImage };

                    //? asign button behaviour
                    textBtn.GetComponent<Button>().onClick.AddListener(() => OpenAnswerCanvas(anserWithTextAndPhotoContent.formatUI, contentDatas));
                    textBtn.GetComponentInChildren<TextMeshProUGUI>().text = anserWithTextAndPhotoContent.playerQuestion;
                }
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(parentOptionBtn.GetComponent<RectTransform>());
        }

        public void OpenAnswerCanvas(DialogAnswerCanvas prefabCanvas, object[] argData)
        {
            DialogAnswerCanvas ansCom = Instantiate(prefabCanvas, instantiatePosition.position, instantiatePosition.rotation, parentGOTransform);
            ansCom.SetupAnswerCanvas(
                argDataAnswer: argData,
                BackToQuestionFunction: DialogQuestionVisibility
                );

            DialogQuestionVisibility(DialogManager.DialogState.AnsweringQuestion);
        }

        public void DialogQuestionVisibility(DialogManager.DialogState feedback)
        {
            switch (feedback)
            {
                case DialogManager.DialogState.DialogIdle:
                    this.gameObject.SetActive(false);
                    break;
                case DialogManager.DialogState.OpeningQuestion:
                    this.gameObject.SetActive(true);
                    break;
                case DialogManager.DialogState.AnsweringQuestion:
                    this.gameObject.SetActive(false);
                    break;
            }
        }
    }
}