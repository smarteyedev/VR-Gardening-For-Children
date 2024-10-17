using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

namespace Smarteye.VRGardening.NPC
{
    public class DialogManager : MonoBehaviour
    {
        public enum DialogState
        {
            DialogIdle, OpeningQuestion, AnsweringQuestion

            /* 1. dialog mati -> button nyala	=> DialogIdle
                2. btn mati -> dialog nyala	=> OpeningQuestion
                3. btn mati -> dialog mati	=> AnsweringQuestion */
        }

        [Header("Dialog Config")]
        [Space(4f)]
        public List<DialogSection> ListDialogSection;

        [Serializable]
        public struct DialogSection
        {
            public DialogArea dialogArea;
            public DialogContent dialogContents;

            [Serializable]
            public struct DialogContent
            {
                [TextArea]
                public string openingDialogTitle;
                public List<QnAContent> QNAContents;

                [Serializable]
                public struct QnAContent
                {
                    [Serializable]
                    public enum ContentType
                    {
                        Custom, AnserWithText, AnserWithTextAndPhoto
                    }
                    public ContentType contentType;

                    public CustomContent customContent;
                    public AnserWithTextContent anserWithTextContent;
                    public AnserWithTextAndPhotoContent anserWithTextAndPhotoContent;

                    [Serializable]
                    public struct CustomContent
                    {
                        [Header("Custom Answer")]
                        [TextArea]
                        public string playerQuestion;

                        [Space(4f)]
                        public DialogAnswerCanvas formatUI;
                    }

                    [Serializable]
                    public struct AnserWithTextContent
                    {
                        [Header("Anser with Text")]
                        [TextArea]
                        public string playerQuestion;
                        [TextArea]
                        public string NpcAnswer;

                        [Space(4f)]
                        public DialogAnswerCanvas formatUI;
                    }

                    [Serializable]
                    public struct AnserWithTextAndPhotoContent
                    {
                        [Header("Anser with Text and Photo")]
                        [TextArea]
                        public string playerQuestion;
                        public Sprite illustarationImage;
                        [TextArea]
                        public string firstParagraph;
                        [TextArea]
                        public string SecondParagraph;

                        [Space(4f)]
                        public DialogAnswerCanvas formatUI;
                    }

                    public object GetItemData()
                    {
                        switch (contentType)
                        {
                            case ContentType.Custom:
                                if (string.IsNullOrEmpty(customContent.playerQuestion) || customContent.formatUI == null)
                                    throw new InvalidOperationException("CustomContent playerQuestion cannot be null or empty.");
                                return customContent;

                            case ContentType.AnserWithText:
                                if (string.IsNullOrEmpty(anserWithTextContent.playerQuestion) ||
                                string.IsNullOrEmpty(anserWithTextContent.NpcAnswer) || anserWithTextContent.formatUI == null)
                                    throw new InvalidOperationException("AnserWithTextContent fields cannot be null or empty.");
                                return anserWithTextContent;

                            case ContentType.AnserWithTextAndPhoto:
                                if (string.IsNullOrEmpty(anserWithTextAndPhotoContent.playerQuestion) ||
                                    string.IsNullOrEmpty(anserWithTextAndPhotoContent.firstParagraph) ||
                                    string.IsNullOrEmpty(anserWithTextAndPhotoContent.SecondParagraph) ||
                                    anserWithTextAndPhotoContent.illustarationImage == null || anserWithTextAndPhotoContent.formatUI == null)
                                {
                                    throw new InvalidOperationException("AnserWithTextAndPhotoContent fields cannot be null or empty.");
                                }
                                return anserWithTextAndPhotoContent;

                            default:
                                throw new InvalidOperationException($"Invalid content type: {contentType}");
                        }
                    }
                }
            }
        }

        [Header("Component Dependencies")]
        [Space(4f)]
        public DialogQuestionCanvas canvasDialogQuestion;
        public GameObject canvasBtnOpenDialog;

        private void Start()
        {
            canvasBtnOpenDialog.GetComponentInChildren<Button>().onClick.AddListener(OpenQuestionCanvas);
        }

        public DialogSection.DialogContent GetCurrentDialogContent(DialogArea _dialogArea)
        {
            var dialogSection = ListDialogSection.FirstOrDefault(x => x.dialogArea == _dialogArea);

            if (dialogSection.Equals(default(DialogSection)))
            {
                // Handle case where no matching DialogSection is found
                Debug.LogWarning($"No dialog content found for dialog area: {_dialogArea}");
                return default(DialogSection.DialogContent);
            }

            return dialogSection.dialogContents;
        }

        public void OpenQuestionCanvas()
        {
            canvasDialogQuestion.ShowDilogQuestion(GetCurrentDialogContent(DialogArea.Gardening));

            canvasBtnOpenDialog.SetActive(false);
        }
    }

    public enum DialogArea
    {
        // must add this section, if there is new section
        None, Welcoming, PlantIntroduction, Gardening
    }
}