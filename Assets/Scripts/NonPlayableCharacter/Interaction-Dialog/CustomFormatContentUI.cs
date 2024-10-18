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
        [SerializeField] private RectTransform parentView;
        [SerializeField] private TextMeshProUGUI textTitle;
        [SerializeField] private TextMeshProUGUI textDescription;
        [SerializeField] private Image imgContent;

        [Space(10f)]
        [SerializeField] private Sprite btnSpriteNonActive;
        [SerializeField] private Sprite btnSpriteActive;
        [SerializeField] private List<Image> btnOption;

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

        private void UpdateBtnSprite()
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

        // is called on button event
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
