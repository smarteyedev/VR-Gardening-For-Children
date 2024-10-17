#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Smarteye.VRGardening.NPC
{
    [CustomPropertyDrawer(typeof(DialogSection.DialogContent.QnAContent))]
    public class QnAContentDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            var contentTypeProp = property.FindPropertyRelative("contentType");
            var customContentProp = property.FindPropertyRelative("customContent");
            var answerWithTextContentProp = property.FindPropertyRelative("answerWithTextContent");
            var answerWithTextAndPhotoContentProp = property.FindPropertyRelative("answerWithTextAndPhotoContent");

            // Menampilkan Content Type Enum
            position.height = EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(position, contentTypeProp);

            // Spasi antara field
            position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            // Menampilkan properti berdasarkan contentType
            switch ((DialogSection.DialogContent.QnAContent.ContentType)contentTypeProp.enumValueIndex)
            {
                case DialogSection.DialogContent.QnAContent.ContentType.Custom:
                    if (customContentProp != null) // Tambahkan pengecekan null
                    {
                        EditorGUI.PropertyField(position, customContentProp, true);
                    }
                    break;

                case DialogSection.DialogContent.QnAContent.ContentType.AnswerWithText:
                    if (answerWithTextContentProp != null) // Tambahkan pengecekan null
                    {
                        EditorGUI.PropertyField(position, answerWithTextContentProp, true);
                    }
                    break;

                case DialogSection.DialogContent.QnAContent.ContentType.AnswerWithTextAndPhoto:
                    if (answerWithTextAndPhotoContentProp != null) // Tambahkan pengecekan null
                    {
                        EditorGUI.PropertyField(position, answerWithTextAndPhotoContentProp, true);
                    }
                    break;
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var contentTypeProp = property.FindPropertyRelative("contentType");
            var customContentProp = property.FindPropertyRelative("customContent");
            var answerWithTextContentProp = property.FindPropertyRelative("answerWithTextContent");
            var answerWithTextAndPhotoContentProp = property.FindPropertyRelative("answerWithTextAndPhotoContent");

            // Hitung tinggi berdasarkan contentType
            float height = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            // Pengecekan null untuk menghindari NullReferenceException
            switch ((DialogSection.DialogContent.QnAContent.ContentType)contentTypeProp.enumValueIndex)
            {
                case DialogSection.DialogContent.QnAContent.ContentType.Custom:
                    if (customContentProp != null) // Pengecekan null
                    {
                        height += EditorGUI.GetPropertyHeight(customContentProp, true);
                    }
                    break;

                case DialogSection.DialogContent.QnAContent.ContentType.AnswerWithText:
                    if (answerWithTextContentProp != null) // Pengecekan null
                    {
                        height += EditorGUI.GetPropertyHeight(answerWithTextContentProp, true);
                    }
                    break;

                case DialogSection.DialogContent.QnAContent.ContentType.AnswerWithTextAndPhoto:
                    if (answerWithTextAndPhotoContentProp != null) // Pengecekan null
                    {
                        height += EditorGUI.GetPropertyHeight(answerWithTextAndPhotoContentProp, true);
                    }
                    break;
            }

            return height;
        }
    }
}
#endif
