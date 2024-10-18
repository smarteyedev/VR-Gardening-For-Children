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
        public enum InteractionState
        {
            None, InteractionIdle, StandByDialog, OpeningQuestion, AnsweringQuestion, OverlappingByFeedback, GivingFeedback
        }

        [Header("Interaction Config")]
        [Space(4f)]
        [ReadOnly] public InteractionArea currentInteractionArea = InteractionArea.None;
        [ReadOnly] public InteractionState currentDialogState = InteractionState.None;
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


            //! Example
            UpdateCurrentDialogArea(InteractionArea.PlantIntroduction);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                PlayFeedback(currentInteractionArea, 1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                PlayFeedback(currentInteractionArea, 2);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                PlayFeedback(currentInteractionArea, 3);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                PlayFeedback(currentInteractionArea, 4);
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                PlayFeedback(currentInteractionArea, 5);
            }
        }

        public void UpdateCurrentDialogArea(InteractionArea newArea)
        {
            currentInteractionArea = newArea;

            var currentContent = GetContentDataOnList(ListDialogSection, x => x.interactionArea == currentInteractionArea);
            if (currentContent == null)
            {
                UpdateDialogeState(InteractionState.None);
            }
            else
            {
                UpdateDialogeState(InteractionState.StandByDialog);
            }
        }

        private void SetCanvasVisibility(bool questionCanvasActive, bool openDialogButtonActive)
        {
            canvasDialogQuestion.gameObject.SetActive(questionCanvasActive);
            canvasBtnOpenDialog.SetActive(openDialogButtonActive);
        }

        private void UpdateDialogeState(InteractionState newState)
        {
            switch (newState)
            {
                case InteractionState.None:
                    SetCanvasVisibility(false, false);
                    break;
                case InteractionState.InteractionIdle:
                    SetCanvasVisibility(false, false);
                    break;
                case InteractionState.StandByDialog:
                    SetCanvasVisibility(false, true);
                    break;
                case InteractionState.OpeningQuestion:
                    SetCanvasVisibility(true, false);
                    break;
                case InteractionState.AnsweringQuestion:
                    SetCanvasVisibility(false, false);
                    break;
                case InteractionState.OverlappingByFeedback:
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
            UpdateDialogeState(InteractionState.OpeningQuestion);
            var currentContent = GetContentDataOnList(ListDialogSection, x => x.interactionArea == currentInteractionArea);
            if (currentContent != null)
            {
                canvasDialogQuestion.ShowDialogQuestion(currentContent.dialogContents);
            }
            else
            {
                UpdateDialogeState(InteractionState.None);
            }
        }

        public void PlayFeedback(InteractionArea _dialogArea, int feedbackIdentity)
        {
            var _data = GetContentDataOnList(ListDirectFeedback, x => x.dialogArea == _dialogArea);
            if (_data != null)
            {
                feedbackController.ShowDirectFeedback(_data.feedbackContents.FirstOrDefault((x) => x.identity == feedbackIdentity));

                if (currentDialogState == InteractionState.InteractionIdle)
                {
                    UpdateDialogeState(InteractionState.GivingFeedback);
                }
                else if (currentDialogState == InteractionState.StandByDialog ||
                currentDialogState == InteractionState.OpeningQuestion || currentDialogState == InteractionState.AnsweringQuestion)
                {
                    UpdateDialogeState(InteractionState.OverlappingByFeedback);
                }
            }
            else Debug.LogWarning("can't get data");
        }

        public void ChangeInteractionState(InteractionManager.InteractionState newState)
        {
            if (currentDialogState == InteractionState.OverlappingByFeedback)
            {
                UpdateDialogeState(InteractionState.StandByDialog);
            }
            else UpdateDialogeState(newState);
        }

    }
}
