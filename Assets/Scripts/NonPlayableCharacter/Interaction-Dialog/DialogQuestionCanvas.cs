using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Smarteye.VRGardening.Utils;

namespace Smarteye.VRGardening.NPC
{
    public class DialogQuestionCanvas : MonoBehaviour
    {
        [Header("Staging")]
        [SerializeField] private DialogSection.DialogContent m_stagingData;

        [Header("Component Dependencies")]
        [SerializeField] private InteractionManager interactionManager;
        [SerializeField] private Button closeBtn;
        [SerializeField] private TextMeshProUGUI textOpeningDialog; // From NPC to player

        [Space(4f)]
        [SerializeField] private GameObject parentOptionBtn;
        [SerializeField] private GameObject prefabOptionBtn;

        [Header("Answer Canvas Config")]
        [Space(4f)]
        [SerializeField] private Transform instantiatePosition;

        private void Start()
        {
            // Validasi jika close button tidak terpasang
            if (closeBtn == null)
            {
                Debug.LogWarning("Close button is not assigned!");
                return;
            }

            closeBtn.onClick.AddListener(CloseDialogQuestionCanvas);
        }

        public void ShowDialogQuestion(DialogSection.DialogContent argData)
        {
            // Validasi null untuk parentOptionBtn dan prefabOptionBtn
            if (parentOptionBtn == null || prefabOptionBtn == null)
            {
                Debug.LogError("Parent option button or prefab option button is not assigned!");
                return;
            }

            m_stagingData = argData;

            // Set opening dialog text
            textOpeningDialog.text = m_stagingData.openingDialogTitle;

            GameObjectModification.ClearChildern(parentOptionBtn.transform);

            // Iterate over QnAContents
            for (int i = 0; i < m_stagingData.QNAContents.Count; i++)
            {
                var itemData = m_stagingData.QNAContents[i].GetItemData();  // Memanggil GetItemData untuk mendapatkan data

                GameObject textBtn = Instantiate(prefabOptionBtn, parentOptionBtn.transform);

                // Cek tipe data yang dikembalikan oleh GetItemData
                if (itemData is DialogSection.DialogContent.CustomContent customContent)
                {
                    // Contoh penggunaan untuk CustomContent
                    object[] contentDatas = new object[1] { m_stagingData.QNAContents[i].contentType };
                    AssignButtonListener(textBtn, () => OpenAnswerCanvas(customContent.formatUI, contentDatas));
                    textBtn.GetComponentInChildren<TextMeshProUGUI>().text = customContent.playerQuestion;
                }
                else if (itemData is DialogSection.DialogContent.AnswerWithTextContent anserWithTextContent)
                {
                    // Contoh penggunaan untuk AnserWithTextContent
                    object[] contentDatas = new object[2] { m_stagingData.QNAContents[i].contentType, anserWithTextContent.NpcAnswer };
                    AssignButtonListener(textBtn, () => OpenAnswerCanvas(anserWithTextContent.formatUI, contentDatas));
                    textBtn.GetComponentInChildren<TextMeshProUGUI>().text = anserWithTextContent.playerQuestion;
                }
                else if (itemData is DialogSection.DialogContent.AnswerWithTextAndPhotoContent anserWithTextAndPhotoContent)
                {
                    // Contoh penggunaan untuk AnserWithTextAndPhotoContent
                    object[] contentDatas = new object[4] { m_stagingData.QNAContents[i].contentType, anserWithTextAndPhotoContent.firstParagraph, anserWithTextAndPhotoContent.secondParagraph, anserWithTextAndPhotoContent.PhotoSprite };
                    AssignButtonListener(textBtn, () => OpenAnswerCanvas(anserWithTextAndPhotoContent.formatUI, contentDatas));
                    textBtn.GetComponentInChildren<TextMeshProUGUI>().text = anserWithTextAndPhotoContent.playerQuestion;
                }
            }

            // Lakukan rebuild layout setelah instansiasi selesai
            LayoutRebuilder.ForceRebuildLayoutImmediate(parentOptionBtn.GetComponent<RectTransform>());
        }

        private void AssignButtonListener(GameObject button, Action listenerAction)
        {
            if (button == null)
            {
                Debug.LogWarning("Button is null, cannot assign listener.");
                return;
            }
            button.GetComponent<Button>().onClick.AddListener(() => listenerAction.Invoke());
        }

        public void OpenAnswerCanvas(DialogAnswerCanvas prefabCanvas, object[] argData)
        {
            DialogAnswerCanvas ansCom = Instantiate(prefabCanvas, instantiatePosition);
            ansCom.SetupAnswerCanvas(
                argDataAnswer: argData,
                BackToQuestionFunction: interactionManager.ChangeInteractionState
            );

            interactionManager.ChangeInteractionState(InteractionManager.InteractionState.AnsweringQuestion);
        }

        public void CloseDialogQuestionCanvas()
        {
            interactionManager.ChangeInteractionState(InteractionManager.InteractionState.StandByDialog);
        }
    }
}
