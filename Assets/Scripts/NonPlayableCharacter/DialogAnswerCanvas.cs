using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace Smarteye.VRGardening.NPC
{
    public class DialogAnswerCanvas : MonoBehaviour
    {
        public DialogSection.DialogContent.QnAContent.ContentType contentType;

        [Header("Component Dependencies")]
        public AnserWithTextDependencies anserWithTextComponent;
        public AnserWithTextAndPhotoDependencies anserWithTextAndPhotoComponent;

        [Serializable]
        public struct AnserWithTextDependencies
        {
            public TextMeshProUGUI textParagraph;
            public Button btnCloseAnswerPanel;
            public Button btnBackToQuestion;
        }

        [Serializable]
        public struct AnserWithTextAndPhotoDependencies
        {
            public TextMeshProUGUI textParagraph1;
            public TextMeshProUGUI textParagraph2;
            public Image imageItem;
            public Button btnCloseAnswerPanel;
            public Button btnBackToQuestion;
        }

        public Button btnCloseCanvas;

        // Helper function to validate if an object is null
        private void ValidateNotNull(object obj, string objName)
        {
            if (obj == null)
                throw new InvalidOperationException($"{objName} cannot be null or empty.");
        }

        // Helper function to set button listeners
        private void SetButtonListeners(Button closeButton, Button backButton, Action<DialogManager.DialogState> backToQuestionFunction)
        {
            closeButton.onClick.AddListener(() =>
            {
                backToQuestionFunction(DialogManager.DialogState.DialogIdle);
                Destroy(this.gameObject);
            });

            backButton.onClick.AddListener(() =>
            {
                backToQuestionFunction(DialogManager.DialogState.OpeningQuestion);
                Destroy(this.gameObject);
            });
        }

        public void SetupAnswerCanvas(object[] argDataAnswer, Action<DialogManager.DialogState> BackToQuestionFunction)
        {
            DialogSection.DialogContent.QnAContent.ContentType myType =
                (DialogSection.DialogContent.QnAContent.ContentType)argDataAnswer[0];

            if (myType == DialogSection.DialogContent.QnAContent.ContentType.AnserWithText)
            {
                string text1 = (string)argDataAnswer[1];

                ValidateNotNull(anserWithTextComponent.textParagraph, "anserWithTextComponent.textParagraph");
                anserWithTextComponent.textParagraph.text = text1;

                SetButtonListeners(
                    anserWithTextComponent.btnCloseAnswerPanel,
                    anserWithTextComponent.btnBackToQuestion,
                    BackToQuestionFunction
                );
            }
            else if (myType == DialogSection.DialogContent.QnAContent.ContentType.AnserWithTextAndPhoto)
            {
                string text1 = (string)argDataAnswer[1];
                string text2 = (string)argDataAnswer[2];
                Sprite spriteImage = (Sprite)argDataAnswer[3];

                ValidateNotNull(anserWithTextAndPhotoComponent.textParagraph1, "anserWithTextAndPhotoComponent.textParagraph1");
                ValidateNotNull(anserWithTextAndPhotoComponent.textParagraph2, "anserWithTextAndPhotoComponent.textParagraph2");
                ValidateNotNull(anserWithTextAndPhotoComponent.imageItem, "anserWithTextAndPhotoComponent.imageItem");

                anserWithTextAndPhotoComponent.textParagraph1.text = text1;
                anserWithTextAndPhotoComponent.textParagraph2.text = text2;
                anserWithTextAndPhotoComponent.imageItem.sprite = spriteImage;

                SetButtonListeners(
                    anserWithTextAndPhotoComponent.btnCloseAnswerPanel,
                    anserWithTextAndPhotoComponent.btnBackToQuestion,
                    BackToQuestionFunction
                );
            }

            btnCloseCanvas.onClick.AddListener(() =>
            {
                BackToQuestionFunction(DialogManager.DialogState.DialogIdle);
                Destroy(this.gameObject);
            });
        }
    }
}
