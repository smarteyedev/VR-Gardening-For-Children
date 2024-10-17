using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

namespace Smarteye.VRGardening.NPC
{
    public enum DialogArea
    {
        None, Welcoming, PlantIntroduction, Gardening
    }

    public class DialogManager : MonoBehaviour
    {
        public enum DialogState
        {
            DialogIdle, OpeningQuestion, AnsweringQuestion
        }

        [Header("Dialog Config")]
        [Space(4f)]
        public DialogState currentDialogState;
        public DialogArea currentDialogArea = DialogArea.None;
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
                    public enum ContentType { Custom, AnserWithText, AnserWithTextAndPhoto }
                    public ContentType contentType;

                    public CustomContent customContent;
                    public AnserWithTextContent anserWithTextContent;
                    public AnserWithTextAndPhotoContent anserWithTextAndPhotoContent;

                    [Serializable]
                    public struct CustomContent
                    {
                        [TextArea] public string playerQuestion;
                        public DialogAnswerCanvas formatUI;
                    }

                    [Serializable]
                    public struct AnserWithTextContent
                    {
                        [TextArea] public string playerQuestion;
                        [TextArea] public string NpcAnswer;
                        public DialogAnswerCanvas formatUI;
                    }

                    [Serializable]
                    public struct AnserWithTextAndPhotoContent
                    {
                        [TextArea] public string playerQuestion;
                        public Sprite illustarationImage;
                        [TextArea] public string firstParagraph;
                        [TextArea] public string SecondParagraph;
                        public DialogAnswerCanvas formatUI;
                    }

                    public object GetItemData()
                    {
                        switch (contentType)
                        {
                            case ContentType.Custom:
                                if (string.IsNullOrEmpty(customContent.playerQuestion) || customContent.formatUI == null)
                                    throw new ArgumentNullException(nameof(customContent.playerQuestion), "CustomContent playerQuestion cannot be null or empty.");
                                return customContent;

                            case ContentType.AnserWithText:
                                if (string.IsNullOrEmpty(anserWithTextContent.playerQuestion) ||
                                    string.IsNullOrEmpty(anserWithTextContent.NpcAnswer) || anserWithTextContent.formatUI == null)
                                    throw new ArgumentNullException(nameof(anserWithTextContent), "AnserWithTextContent fields cannot be null or empty.");
                                return anserWithTextContent;

                            case ContentType.AnserWithTextAndPhoto:
                                if (string.IsNullOrEmpty(anserWithTextAndPhotoContent.playerQuestion) ||
                                    string.IsNullOrEmpty(anserWithTextAndPhotoContent.firstParagraph) ||
                                    string.IsNullOrEmpty(anserWithTextAndPhotoContent.SecondParagraph) ||
                                    anserWithTextAndPhotoContent.illustarationImage == null || anserWithTextAndPhotoContent.formatUI == null)
                                    throw new ArgumentNullException(nameof(anserWithTextAndPhotoContent), "AnserWithTextAndPhotoContent fields cannot be null or empty.");
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
            Button openDialogButton = canvasBtnOpenDialog.GetComponentInChildren<Button>();
            if (openDialogButton == null)
            {
                Debug.LogWarning("OpenDialog button is missing.");
                return;
            }
            openDialogButton.onClick.AddListener(OpenQuestionCanvas);
            UpdateCurrentDialogArea(DialogArea.PlantIntroduction);
        }

        public void UpdateCurrentDialogArea(DialogArea newArea)
        {
            currentDialogArea = newArea;
            UpdateDialogeState(DialogManager.DialogState.DialogIdle);
        }

        private void SetCanvasVisibility(bool questionCanvasActive, bool openDialogButtonActive)
        {
            canvasDialogQuestion.gameObject.SetActive(questionCanvasActive);
            canvasBtnOpenDialog.SetActive(openDialogButtonActive);
        }

        public void UpdateDialogeState(DialogManager.DialogState newState)
        {
            switch (newState)
            {
                case DialogManager.DialogState.DialogIdle:
                    SetCanvasVisibility(false, true);
                    break;
                case DialogManager.DialogState.OpeningQuestion:
                    SetCanvasVisibility(true, false);
                    break;
                case DialogManager.DialogState.AnsweringQuestion:
                    SetCanvasVisibility(false, false);
                    break;
            }
            currentDialogState = newState;
        }

        public DialogSection.DialogContent GetCurrentDialogContent(DialogArea _dialogArea)
        {
            if (ListDialogSection == null || ListDialogSection.Count == 0)
            {
                Debug.LogWarning("DialogSection list is empty.");
                return default(DialogSection.DialogContent);
            }

            var dialogSection = ListDialogSection.FirstOrDefault(x => x.dialogArea == _dialogArea);

            if (dialogSection.Equals(default(DialogSection)))
            {
                Debug.LogWarning($"No dialog content found for dialog area: {_dialogArea}");
                return default(DialogSection.DialogContent);
            }

            return dialogSection.dialogContents;
        }

        public void OpenQuestionCanvas()
        {
            UpdateDialogeState(DialogState.OpeningQuestion);
            canvasDialogQuestion.ShowDilogQuestion(GetCurrentDialogContent(currentDialogArea));
        }
    }
}
