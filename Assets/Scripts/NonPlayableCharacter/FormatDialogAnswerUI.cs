using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace Smarteye.VRGardening.NPC
{
    public class FormatDialogAnswerUI : MonoBehaviour
    {
        public DialogManager.DialogSection.DialogContent.QnAContent.ContentType contentType;

        public AnserWithTextDependencies anserWithTextComponent;
        public AnserWithTextAndPhotoDependencies anserWithTextAndPhotoComponent;

        [Serializable]
        public struct AnserWithTextDependencies
        {
            public TextMeshProUGUI textParagraph;
        }

        [Serializable]
        public struct AnserWithTextAndPhotoDependencies
        {
            public Image imageItem;
            public TextMeshProUGUI textParagraph1;
            public TextMeshProUGUI textParagraph2;
        }

    }
}