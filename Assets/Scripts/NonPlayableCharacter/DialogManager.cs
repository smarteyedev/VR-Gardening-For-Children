using System;
using System.Collections.Generic;
using UnityEngine;

namespace Smarteye.VRGardening.NPC
{
    public class DialogManager : MonoBehaviour
    {

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
                        public FormatDialogAnswerUI formatUI;
                    }

                    [Serializable]
                    public struct AnserWithTextContent
                    {
                        [Header("Anser with Text")]
                        [TextArea]
                        public string playerQuestion;
                        [TextArea]
                        public string NpcAnswer;
                    }

                    [Serializable]
                    public struct AnserWithTextAndPhotoContent
                    {
                        [Header("Anser with Text and Photo")]
                        public Sprite illustarationImage;
                        [TextArea]
                        public string firstParagraph;
                        [TextArea]
                        public string SecondParagraph;
                    }
                }
            }
        }

        [Header("Component Dependencies")]
        [Space(4f)]
        public GameObject mainDialogPanel;
    }

    public enum DialogArea
    {
        // must add this section, if there is new section
        None, Welcoming, PlantIntroduction
    }
}