using System;
using System.Collections.Generic;
using UnityEngine;

namespace Smarteye.VRGardening.NPC
{
    [CreateAssetMenu(fileName = "NewDialogSection", menuName = "Dialog System/Dialog Section")]
    public class DialogSection : ScriptableObject
    {
        public DialogArea dialogArea;
        public DialogContent dialogContents;

        [System.Serializable]
        public class DialogContent
        {
            public string openingDialogTitle;
            public List<QnAContent> QNAContents;

            [System.Serializable]
            public class QnAContent
            {
                public enum ContentType { Custom, AnserWithText, AnserWithTextAndPhoto }
                public ContentType contentType;

                public CustomContent customContent;
                public AnserWithTextContent anserWithTextContent;
                public AnserWithTextAndPhotoContent anserWithTextAndPhotoContent;

                // Fungsi GetItemData diletakkan di sini
                public object GetItemData()
                {
                    switch (contentType)
                    {
                        case ContentType.Custom:
                            ValidateCustomContent();
                            return customContent;

                        case ContentType.AnserWithText:
                            ValidateAnserWithTextContent();
                            return anserWithTextContent;

                        case ContentType.AnserWithTextAndPhoto:
                            ValidateAnserWithTextAndPhotoContent();
                            return anserWithTextAndPhotoContent;

                        default:
                            throw new InvalidOperationException($"Invalid content type: {contentType}");
                    }
                }

                private void ValidateCustomContent()
                {
                    if (string.IsNullOrEmpty(customContent.playerQuestion) || customContent.formatUI == null)
                        throw new ArgumentNullException(nameof(customContent.playerQuestion), "CustomContent playerQuestion cannot be null or empty.");
                }

                private void ValidateAnserWithTextContent()
                {
                    if (string.IsNullOrEmpty(anserWithTextContent.playerQuestion) ||
                        string.IsNullOrEmpty(anserWithTextContent.NpcAnswer) || anserWithTextContent.formatUI == null)
                        throw new ArgumentNullException(nameof(anserWithTextContent), "AnserWithTextContent fields cannot be null or empty.");
                }

                private void ValidateAnserWithTextAndPhotoContent()
                {
                    if (string.IsNullOrEmpty(anserWithTextAndPhotoContent.playerQuestion) ||
                        string.IsNullOrEmpty(anserWithTextAndPhotoContent.firstParagraph) ||
                        string.IsNullOrEmpty(anserWithTextAndPhotoContent.SecondParagraph) ||
                        anserWithTextAndPhotoContent.illustarationImage == null || anserWithTextAndPhotoContent.formatUI == null)
                        throw new ArgumentNullException(nameof(anserWithTextAndPhotoContent), "AnserWithTextAndPhotoContent fields cannot be null or empty.");
                }
            }

            [System.Serializable]
            public class CustomContent
            {
                public string playerQuestion;
                public DialogAnswerCanvas formatUI;
            }

            [System.Serializable]
            public class AnserWithTextContent
            {
                public string playerQuestion;
                [TextArea] public string NpcAnswer;
                public DialogAnswerCanvas formatUI;
            }

            [System.Serializable]
            public class AnserWithTextAndPhotoContent
            {
                public string playerQuestion;
                public Sprite illustarationImage;
                [TextArea] public string firstParagraph;
                [TextArea] public string SecondParagraph;
                public DialogAnswerCanvas formatUI;
            }
        }
    }
}