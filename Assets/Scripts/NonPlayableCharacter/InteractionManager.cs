using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

namespace Smarteye.VRGardening.NPC
{
    public enum InteractionArea
    {
        None, Welcoming, PlantIntroduction, ToolsIntroductinon, Gardening
    }

    public class InteractionManager : MonoBehaviour
    {
        public enum DialogState
        {
            None, InteractionIdle, OpeningQuestion, AnsweringQuestion
        }

        [Header("Interaction Config")]
        [Space(4f)]
        [ReadOnly] public InteractionArea currentInteractionArea = InteractionArea.None;
        [ReadOnly] public DialogState currentDialogState = DialogState.None;
        [Space(5f)]
        [SerializeField] private List<DialogSection> ListDialogSection;
        [Space(5f)]
        [SerializeField] private List<DirectFeedback> ListDirectFeedback;

        [Header("Component Dependencies")]
        [Space(4f)]
        [SerializeField] private DialogQuestionCanvas canvasDialogQuestion;
        [SerializeField] private GameObject canvasBtnOpenDialog;

        [Space(6f)]
        [SerializeField] private FeedbackController feedbackController;

        private void Start()
        {
            Button openDialogButton = canvasBtnOpenDialog.GetComponentInChildren<Button>();
            if (openDialogButton == null)
            {
                Debug.LogWarning("OpenDialog button is missing.");
                return;
            }
            openDialogButton.onClick.AddListener(OpenQuestionCanvas);
            UpdateCurrentDialogArea(InteractionArea.ToolsIntroductinon);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                PlayFeedback(currentInteractionArea, 0);
            }
        }

        public void UpdateCurrentDialogArea(InteractionArea newArea)
        {
            currentInteractionArea = newArea;

            var currentContent = GetContentDataOnList(ListDialogSection, x => x.interactionArea == currentInteractionArea);
            if (currentContent == null)
            {
                UpdateDialogeState(DialogState.None);
            }
            else
            {
                UpdateDialogeState(DialogState.InteractionIdle);
            }
        }

        private void SetCanvasVisibility(bool questionCanvasActive, bool openDialogButtonActive)
        {
            canvasDialogQuestion.gameObject.SetActive(questionCanvasActive);
            canvasBtnOpenDialog.SetActive(openDialogButtonActive);
        }

        public void UpdateDialogeState(DialogState newState)
        {
            switch (newState)
            {
                case DialogState.None:
                    SetCanvasVisibility(false, false);
                    break;
                case DialogState.InteractionIdle:
                    SetCanvasVisibility(false, true);
                    break;
                case DialogState.OpeningQuestion:
                    SetCanvasVisibility(true, false);
                    break;
                case DialogState.AnsweringQuestion:
                    SetCanvasVisibility(false, false);
                    break;
            }
            currentDialogState = newState;
        }

        public T GetContentDataOnList<T>(List<T> listTarget, Func<T, bool> predicate)
        {
            // Validasi jika list kosong atau null
            if (listTarget == null || listTarget.Count == 0)
            {
                Debug.LogWarning("List target is empty or null.");
                return default;
            }

            // Mencari item yang sesuai dengan predicate
            var item = listTarget.FirstOrDefault(predicate);

            // Validasi jika item tidak ditemukan
            if (item == null)
            {
                Debug.LogWarning("No matching item found in the list.");
                return default;
            }

            return item;
        }

        public void OpenQuestionCanvas()
        {
            UpdateDialogeState(DialogState.OpeningQuestion);
            var currentContent = GetContentDataOnList(ListDialogSection, x => x.interactionArea == currentInteractionArea);
            if (currentContent != null)
            {
                canvasDialogQuestion.ShowDialogQuestion(currentContent.dialogContents);
            }
            else
            {
                UpdateDialogeState(DialogState.None);
            }
        }

        public void PlayFeedback(InteractionArea _dialogArea, int feedbackIdentity)
        {
            var _data = GetContentDataOnList(ListDirectFeedback, x => x.dialogArea == _dialogArea);
            feedbackController.ShowDirectFeedback(_data.feedbackContents.FirstOrDefault((x) => x.identity == feedbackIdentity));
        }

    }
}
