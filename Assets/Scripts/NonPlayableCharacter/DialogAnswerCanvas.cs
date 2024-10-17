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
        public DialogManager.DialogSection.DialogContent.QnAContent.ContentType contentType;

        public AnserWithTextDependencies anserWithTextComponent;
        public AnserWithTextAndPhotoDependencies anserWithTextAndPhotoComponent;

        [Serializable]
        public struct AnserWithTextDependencies
        {
            public TextMeshProUGUI textParagraph;
        }

        [Serializable]
        public struct AnserWithTextAndPhotoDependencies
        {
            public TextMeshProUGUI textParagraph1;
            public TextMeshProUGUI textParagraph2;
            public Image imageItem;
        }

        [Header("Component Dependencies")]
        public Button btnBackToQuestion;

        public void SetupAnswerCanvas(object[] argDataAnswer, Action<DialogManager.DialogState> BackToQuestionFunction)
        {
            DialogManager.DialogSection.DialogContent.QnAContent.ContentType myType = (DialogManager.DialogSection.DialogContent.QnAContent.ContentType)argDataAnswer[0];

            if (myType == DialogManager.DialogSection.DialogContent.QnAContent.ContentType.AnserWithText)
            {
                string text1 = (string)argDataAnswer[1];

                if (anserWithTextComponent.textParagraph)
                    anserWithTextComponent.textParagraph.text = text1;
                else throw new InvalidOperationException("anserWithTextComponent.textParagraph cannot be null or empty.");

                btnBackToQuestion.GetComponent<Button>().onClick.AddListener(() =>
                {
                    BackToQuestionFunction(DialogManager.DialogState.OpeningQuestion);
                    OnCloseOrDestroyCanvas();
                });
            }
            else if (myType == DialogManager.DialogSection.DialogContent.QnAContent.ContentType.AnserWithTextAndPhoto)
            {
                string text1 = (string)argDataAnswer[1];
                string text2 = (string)argDataAnswer[2];
                Sprite spriteImage = (Sprite)argDataAnswer[3];

                if (anserWithTextAndPhotoComponent.textParagraph1)
                    anserWithTextAndPhotoComponent.textParagraph1.text = text1;
                else throw new InvalidOperationException("anserWithTextAndPhotoComponent.textParagraph1 cannot be null or empty.");
                if (anserWithTextAndPhotoComponent.textParagraph2)
                    anserWithTextAndPhotoComponent.textParagraph2.text = text2;
                else throw new InvalidOperationException("anserWithTextAndPhotoComponent.textParagraph2 cannot be null or empty.");
                if (anserWithTextAndPhotoComponent.imageItem)
                    anserWithTextAndPhotoComponent.imageItem.sprite = spriteImage;
                else throw new InvalidOperationException("anserWithTextAndPhotoComponent.imageItem cannot be null or empty.");

                btnBackToQuestion.GetComponent<Button>().onClick.AddListener(() =>
                {
                    BackToQuestionFunction(DialogManager.DialogState.OpeningQuestion);
                    OnCloseOrDestroyCanvas();
                });
            }
        }

        public void OnCloseOrDestroyCanvas()
        {
            Destroy(this.gameObject);
        }
    }
}