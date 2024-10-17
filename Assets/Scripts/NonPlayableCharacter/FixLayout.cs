using UnityEngine;
using UnityEngine.UI;

namespace Smarteye.VRGardening.NPC
{
    public class FixLayout : MonoBehaviour
    {
        public static FixLayout instance;
        public RectTransform parentRectTransform;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Debug.LogWarning("Multiple instances of FixLayout detected!");
                Destroy(gameObject);
            }
        }

        void OnEnable()
        {
            // Paksa layout parent untuk dihitung ulang pada start
            LayoutRebuilder.ForceRebuildLayoutImmediate(parentRectTransform);
        }
    }
}