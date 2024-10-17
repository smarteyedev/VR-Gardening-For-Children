using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

namespace Smarteye.VRGardening.NPC
{
    public class CustomFormatContentUI : MonoBehaviour
    {
        [Header("Contents")]
        public List<CustomContent> customContents;

        [Serializable]
        public struct CustomContent
        {
            public string title;
            public string description;
            public Sprite spriteImg;
        }

        [Header("UI Dependencies")]
        public RectTransform parentView;
        public TextMeshProUGUI textTitle;
        public TextMeshProUGUI textDescription;
        public Image imgContent;

        [Space(10f)]
        public Sprite btnSpriteNonActive;
        public Sprite btnSpriteActive;
        public List<Image> btnOption;

        private int m_index = 2; // Default index

        private void Start()
        {
            ValidateUIComponents();
            OnChangeContentView(0);
        }

        private void ValidateUIComponents()
        {
            if (textTitle == null) Debug.LogError("Text Title is not assigned.");
            if (textDescription == null) Debug.LogError("Text Description is not assigned.");
            if (imgContent == null) Debug.LogError("Image Content is not assigned.");
        }

        private void OnEnable()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(parentView);

            foreach (var item in btnOption)
            {
                if (item != null) // tambahkan null check
                {
                    LayoutRebuilder.ForceRebuildLayoutImmediate(item.rectTransform);
                }
            }
        }

        public void UpdateBtnSprite()
        {
            for (int i = 0; i < btnOption.Count; i++)
            {
                var targetSprite = (i == m_index) ? btnSpriteActive : btnSpriteNonActive;

                if (btnOption[i] != null && btnOption[i].sprite != targetSprite)
                {
                    btnOption[i].sprite = targetSprite;
                }
            }
        }

        public void OnChangeContentView(int newIndex)
        {
            if (newIndex < 0 || newIndex >= customContents.Count)
            {
                Debug.LogError("Invalid index for content view.");
                return;
            }

            m_index = newIndex;

            textTitle.text = customContents[m_index].title;
            textDescription.text = customContents[m_index].description;
            imgContent.sprite = customContents[m_index].spriteImg;

            UpdateBtnSprite();
        }
    }
}
