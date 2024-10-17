using System.Collections.Generic;
using UnityEngine;

namespace Smarteye.VRGardening.NPC
{
    [CreateAssetMenu(fileName = "NewDialogSection", menuName = "Interaction Content/Dialog Section")]
    public class DialogSection : ScriptableObject
    {
        public InteractionArea interactionArea;
        public DialogContent dialogContents;

        [System.Serializable]
        public class DialogContent
        {
            public string openingDialogTitle;
            public List<QnAContent> QNAContents;

            [System.Serializable]
            public class QnAContent
            {
                public enum ContentType { Custom, AnswerWithText, AnswerWithTextAndPhoto }
                public ContentType contentType;

                public CustomContent customContent;
                public AnswerWithTextContent answerWithTextContent;
                public AnswerWithTextAndPhotoContent answerWithTextAndPhotoContent;

                // Fungsi GetItemData diletakkan di sini
                public object GetItemData()
                {
                    switch (contentType)
                    {
                        case ContentType.Custom:
                            ValidateCustomContent();
                            return customContent;

                        case ContentType.AnswerWithText:
                            ValidateAnswerWithTextContent();
                            return answerWithTextContent;

                        case ContentType.AnswerWithTextAndPhoto:
                            ValidateAnswerWithTextAndPhotoContent();
                            return answerWithTextAndPhotoContent;

                        default:
                            throw new System.InvalidOperationException($"Invalid content type: {contentType}");
                    }
                }

                private void ValidateCustomContent()
                {
                    if (string.IsNullOrEmpty(customContent.playerQuestion) || customContent.formatUI == null)
                        throw new System.ArgumentNullException(nameof(customContent.playerQuestion), "CustomContent playerQuestion cannot be null or empty.");
                }

                private void ValidateAnswerWithTextContent()
                {
                    if (string.IsNullOrEmpty(answerWithTextContent.playerQuestion) ||
                        string.IsNullOrEmpty(answerWithTextContent.NpcAnswer) || answerWithTextContent.formatUI == null)
                        throw new System.ArgumentNullException(nameof(answerWithTextContent), "AnswerWithTextContent fields cannot be null or empty.");
                }

                private void ValidateAnswerWithTextAndPhotoContent()
                {
                    if (string.IsNullOrEmpty(answerWithTextAndPhotoContent.playerQuestion) ||
                        string.IsNullOrEmpty(answerWithTextAndPhotoContent.firstParagraph) ||
                        string.IsNullOrEmpty(answerWithTextAndPhotoContent.secondParagraph) ||
                        answerWithTextAndPhotoContent.PhotoSprite == null || answerWithTextAndPhotoContent.formatUI == null)
                        throw new System.ArgumentNullException(nameof(answerWithTextAndPhotoContent), "AnswerWithTextAndPhotoContent fields cannot be null or empty.");
                }
            }

            [System.Serializable]
            public class CustomContent
            {
                public string playerQuestion;
                public DialogAnswerCanvas formatUI;
            }

            [System.Serializable]
            public class AnswerWithTextContent
            {
                public string playerQuestion;
                [TextArea] public string NpcAnswer;
                public DialogAnswerCanvas formatUI;
            }

            [System.Serializable]
            public class AnswerWithTextAndPhotoContent
            {
                public string playerQuestion;
                public Sprite PhotoSprite;
                [TextArea] public string firstParagraph;
                [TextArea] public string secondParagraph;
                public DialogAnswerCanvas formatUI;
            }
        }
    }
}
