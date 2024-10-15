using UnityEngine;
using UnityEngine.UI;

namespace Smarteye.NPC
{

    public class FixLayout : MonoBehaviour
    {
        public RectTransform parentRectTransform;

        void OnEnable()
        {
            // Paksa layout parent untuk dihitung ulang pada start
            LayoutRebuilder.ForceRebuildLayoutImmediate(parentRectTransform);
        }
    }
}