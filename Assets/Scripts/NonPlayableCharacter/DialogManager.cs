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
        [ReadOnly] public DialogState currentDialogState;
        [ReadOnly] public DialogArea currentDialogArea = DialogArea.None;
        public List<DialogSection> ListDialogSection;

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
            UpdateCurrentDialogArea(DialogArea.Gardening);
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
                return null;
            }

            var dialogSection = ListDialogSection.FirstOrDefault(x => x.dialogArea == _dialogArea);

            if (dialogSection == null)
            {
                Debug.LogWarning($"No dialog content found for dialog area: {_dialogArea}");
                return null;
            }

            return dialogSection.dialogContents;
        }

        public void OpenQuestionCanvas()
        {
            UpdateDialogeState(DialogState.OpeningQuestion);
            var currentContent = GetCurrentDialogContent(currentDialogArea);
            if (currentContent != null)
            {
                canvasDialogQuestion.ShowDialogQuestion(currentContent);
            }
        }
    }
}
