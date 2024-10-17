using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Smarteye.VRGardening.NPC
{
    public class FeedbackController : MonoBehaviour
    {
        [Header("Staging")]
        [SerializeField] private DirectFeedback.FeedbackContent m_stagingData;

        [Header("Component Dependencies")]
        [Space(4f)]
        [SerializeField] private Transform parentCanvas;
        [SerializeField] private DialogAnswerCanvas prefabFeedbackCanvas;

        [Space(6f)]
        [SerializeField] private InteractionManager interactionManager;

        private void Start()
        {

        }

        public void ShowDirectFeedback(DirectFeedback.FeedbackContent directFeedback)
        {
            m_stagingData = directFeedback;

            if (m_stagingData.feedbackType == DirectFeedback.FeedbackContent.FeedbackType.TextPhotoPopup)
            {
                object[] contentDatas = new object[4] { m_stagingData.feedbackType, m_stagingData.textPhotoPopupContent.firstParagraph,
                                                        m_stagingData.textPhotoPopupContent.secondParagraph, m_stagingData.textPhotoPopupContent.PhotoSprite};
                DialogAnswerCanvas _feedback = Instantiate(prefabFeedbackCanvas, parentCanvas);
                _feedback.SetupFeedbackCanvas(contentDatas, interactionManager.UpdateDialogeState);
            }
        }
    }
}