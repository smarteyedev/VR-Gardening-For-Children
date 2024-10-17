using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Smarteye.VRGardening.NPC
{
    [CreateAssetMenu(fileName = "NewDirectFeedback", menuName = "Interaction Content/Direct Feedback")]
    public class DirectFeedback : ScriptableObject
    {
        public InteractionArea dialogArea;

        [System.Serializable]
        public class FeedbackContent
        {
            public enum FeedbackType { Custom, TextPopup, TextPhotoPopup, Animation }
            public FeedbackType feedbackType;
            public int identity;

            public TextPopupContent textPopupContent;
            public TextPhotoPopupContent textPhotoPopupContent;
            public AnimationContent animationContent;

            [System.Serializable]
            public class TextPopupContent
            {
                [TextArea] public string scriptText;
                public bool isUsingAudio;
                public AudioClip speakAudio;
            }

            [System.Serializable]
            public class TextPhotoPopupContent
            {
                [TextArea] public string firstParagraph;
                [TextArea] public string secondParagraph;
                public Sprite PhotoSprite;
                public bool isUsingAudio;
                public AudioClip speakAudio;
            }

            [System.Serializable]
            public class AnimationContent
            {
                public string animationName;
                public bool isUsingAudio;
                public AudioClip speakAudio;
            }
        }

        public List<FeedbackContent> feedbackContents;
    }
}
