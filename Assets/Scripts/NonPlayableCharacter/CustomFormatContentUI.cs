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
        // content view
        public RectTransform parentView;
        public TextMeshProUGUI textTitle;
        public TextMeshProUGUI textDescription;
        public Image imgContent;

        [Space(10f)]
        public Sprite btnSpriteNonActive;
        public Sprite btnSpriteActive;
        public List<Image> btnOption;

        private int m_index = 2;

        private void Start()
        {
            OnChangeContentView(0);

            // ! must create new fuction if is not destroyable gameobject
        }

        private void OnEnable()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(parentView);

            foreach (var item in btnOption)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(item.rectTransform);
            }
        }

        public void UpdateBtnSprite()
        {
            for (int i = 0; i < btnOption.Count; i++)
            {
                var targetSprite = (i == m_index) ? btnSpriteActive : btnSpriteNonActive;
                if (btnOption[i].sprite != targetSprite) // update if sprite is different
                {
                    btnOption[i].sprite = targetSprite;
                }
            }
        }

        public void OnChangeContentView(int newIndex)
        {
            m_index = newIndex;

            textTitle.text = customContents[m_index].title;
            textDescription.text = customContents[m_index].description;
            imgContent.sprite = customContents[m_index].spriteImg;

            UpdateBtnSprite();
        }
    }
}