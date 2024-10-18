using UnityEngine;
using UnityEngine.UI;

namespace Smarteye.VRGardening.Utils
{
    public static class GameObjectModification
    {
        public static void FixLayout(RectTransform parentRectTransform)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(parentRectTransform);
        }

        public static void ClearChildern(Transform parentObject)
        {
            foreach (Transform child in parentObject.transform)
            {
                Object.Destroy(child.gameObject);
            }
        }
    }
}