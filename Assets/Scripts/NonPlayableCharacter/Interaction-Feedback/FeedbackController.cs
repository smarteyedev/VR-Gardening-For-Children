using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Smarteye.VRGardening.Utils;

namespace Smarteye.VRGardening.NPC
{
    public class FeedbackController : MonoBehaviour
    {
        [Header("Staging")]
        private DirectFeedback.FeedbackContent m_stagingData;

        [Header("Component Dependencies")]
        [Space(4f)]
        [SerializeField] private Transform parentCanvas;
        [SerializeField] private DialogAnswerCanvas prefabFeedbackTextCanvas;
        [SerializeField] private DialogAnswerCanvas prefabFeedbackTextPhotoCanvas;

        [Space(6f)]
        [SerializeField] private InteractionManager interactionManager;

        public void ShowDirectFeedback(DirectFeedback.FeedbackContent directFeedback)
        {
            m_stagingData = directFeedback;

            if (m_stagingData.feedbackType == DirectFeedback.FeedbackContent.FeedbackType.TextPopup)
            {
                GameObjectModification.ClearChildern(parentCanvas);

                AudioClip assetAudio = m_stagingData.textPopupContent.isUsingAudio ? m_stagingData.textPopupContent.speakAudio : null;
                object[] contentDatas = new object[3] { m_stagingData.feedbackType, m_stagingData.textPopupContent.scriptText, assetAudio };

                DialogAnswerCanvas _feedback = Instantiate(prefabFeedbackTextCanvas, parentCanvas);
                _feedback.SetupFeedbackCanvas(contentDatas, interactionManager.ChangeInteractionState);
            }
            else if (m_stagingData.feedbackType == DirectFeedback.FeedbackContent.FeedbackType.TextPhotoPopup)
            {
                GameObjectModification.ClearChildern(parentCanvas);

                AudioClip assetAudio = m_stagingData.textPopupContent.isUsingAudio ? m_stagingData.textPopupContent.speakAudio : null;
                object[] contentDatas = new object[5] { m_stagingData.feedbackType, m_stagingData.textPhotoPopupContent.firstParagraph,
                                                        m_stagingData.textPhotoPopupContent.secondParagraph, m_stagingData.textPhotoPopupContent.PhotoSprite, assetAudio};

                DialogAnswerCanvas _feedback = Instantiate(prefabFeedbackTextPhotoCanvas, parentCanvas);
                _feedback.SetupFeedbackCanvas(contentDatas, interactionManager.ChangeInteractionState);
            }
        }
    }
}