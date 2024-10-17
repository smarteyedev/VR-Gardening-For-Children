#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Smarteye.VRGardening.NPC
{
    [CustomPropertyDrawer(typeof(DirectFeedback.FeedbackContent))]
    public class FeedbackContentDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            // Mendapatkan properti FeedbackType dan Identity
            var feedbackTypeProp = property.FindPropertyRelative("feedbackType");
            var identityProp = property.FindPropertyRelative("identity");

            // Mendapatkan properti lain berdasarkan FeedbackType
            var textPopupContentProp = property.FindPropertyRelative("textPopupContent");
            var textPhotoPopupContentProp = property.FindPropertyRelative("textPhotoPopupContent");
            var animationContentProp = property.FindPropertyRelative("animationContent");

            // Menampilkan identity
            position.height = EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(position, identityProp, new GUIContent("Identity"));

            // Spasi antara field
            position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            // Menampilkan FeedbackType Enum
            EditorGUI.PropertyField(position, feedbackTypeProp);

            // Spasi antara field
            position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            // Menampilkan properti berdasarkan feedbackType
            switch ((DirectFeedback.FeedbackContent.FeedbackType)feedbackTypeProp.enumValueIndex)
            {
                case DirectFeedback.FeedbackContent.FeedbackType.TextPopup:
                    if (textPopupContentProp != null)
                    {
                        EditorGUI.PropertyField(position, textPopupContentProp, true);
                    }
                    break;

                case DirectFeedback.FeedbackContent.FeedbackType.TextPhotoPopup:
                    if (textPhotoPopupContentProp != null)
                    {
                        EditorGUI.PropertyField(position, textPhotoPopupContentProp, true);
                    }
                    break;

                case DirectFeedback.FeedbackContent.FeedbackType.Animation:
                    if (animationContentProp != null)
                    {
                        EditorGUI.PropertyField(position, animationContentProp, true);
                    }
                    break;
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var feedbackTypeProp = property.FindPropertyRelative("feedbackType");
            var textPopupContentProp = property.FindPropertyRelative("textPopupContent");
            var textPhotoPopupContentProp = property.FindPropertyRelative("textPhotoPopupContent");
            var animationContentProp = property.FindPropertyRelative("animationContent");

            // Hitung tinggi berdasarkan FeedbackType
            float height = EditorGUIUtility.singleLineHeight * 2 + EditorGUIUtility.standardVerticalSpacing * 2; // Untuk identity dan feedbackType

            // Pengecekan null untuk menghindari NullReferenceException
            switch ((DirectFeedback.FeedbackContent.FeedbackType)feedbackTypeProp.enumValueIndex)
            {
                case DirectFeedback.FeedbackContent.FeedbackType.TextPopup:
                    if (textPopupContentProp != null)
                    {
                        height += EditorGUI.GetPropertyHeight(textPopupContentProp, true);
                    }
                    break;

                case DirectFeedback.FeedbackContent.FeedbackType.TextPhotoPopup:
                    if (textPhotoPopupContentProp != null)
                    {
                        height += EditorGUI.GetPropertyHeight(textPhotoPopupContentProp, true);
                    }
                    break;

                case DirectFeedback.FeedbackContent.FeedbackType.Animation:
                    if (animationContentProp != null)
                    {
                        height += EditorGUI.GetPropertyHeight(animationContentProp, true);
                    }
                    break;
            }

            return height;
        }
    }

    [CustomPropertyDrawer(typeof(DirectFeedback.FeedbackContent.TextPopupContent))]
    public class TextPopupContentDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            // Mendapatkan properti
            var scriptTextProp = property.FindPropertyRelative("scriptText");
            var isUsingAudioProp = property.FindPropertyRelative("isUsingAudio");
            var speakAudioProp = property.FindPropertyRelative("speakAudio");

            // Menampilkan scriptText
            position.height = EditorGUIUtility.singleLineHeight * 3; // Menampilkan teks area
            EditorGUI.PropertyField(position, scriptTextProp);

            position.y += EditorGUIUtility.singleLineHeight * 3 + EditorGUIUtility.standardVerticalSpacing;

            // Menampilkan isUsingAudio
            position.height = EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(position, isUsingAudioProp);

            position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            // Jika isUsingAudio diaktifkan, tampilkan speakAudio
            if (isUsingAudioProp.boolValue)
            {
                EditorGUI.PropertyField(position, speakAudioProp);
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var isUsingAudioProp = property.FindPropertyRelative("isUsingAudio");

            // Tinggi dasar untuk scriptText dan isUsingAudio
            float height = EditorGUIUtility.singleLineHeight * 3 + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing * 2;

            // Tambah tinggi jika isUsingAudio dicentang
            if (isUsingAudioProp.boolValue)
            {
                height += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            }

            return height;
        }
    }

    [CustomPropertyDrawer(typeof(DirectFeedback.FeedbackContent.TextPhotoPopupContent))]
    public class TextPhotoPopupContentDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            // Mendapatkan properti
            var firstParagraphProp = property.FindPropertyRelative("firstParagraph");
            var secondParagraphProp = property.FindPropertyRelative("secondParagraph");
            var photoSpriteProp = property.FindPropertyRelative("PhotoSprite");
            var isUsingAudioProp = property.FindPropertyRelative("isUsingAudio");
            var speakAudioProp = property.FindPropertyRelative("speakAudio");

            // Menampilkan firstParagraph
            position.height = EditorGUIUtility.singleLineHeight * 3;
            EditorGUI.PropertyField(position, firstParagraphProp);

            position.y += EditorGUIUtility.singleLineHeight * 3 + EditorGUIUtility.standardVerticalSpacing;

            // Menampilkan secondParagraph
            position.height = EditorGUIUtility.singleLineHeight * 3;
            EditorGUI.PropertyField(position, secondParagraphProp);

            position.y += EditorGUIUtility.singleLineHeight * 3 + EditorGUIUtility.standardVerticalSpacing;

            // Menampilkan photoSprite
            position.height = EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(position, photoSpriteProp);

            position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            // Menampilkan isUsingAudio
            position.height = EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(position, isUsingAudioProp);

            position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            // Jika isUsingAudio diaktifkan, tampilkan speakAudio
            if (isUsingAudioProp.boolValue)
            {
                EditorGUI.PropertyField(position, speakAudioProp);
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var isUsingAudioProp = property.FindPropertyRelative("isUsingAudio");

            // Tinggi dasar untuk firstParagraph, secondParagraph, photoSprite, dan isUsingAudio
            float height = EditorGUIUtility.singleLineHeight * 8 + EditorGUIUtility.standardVerticalSpacing * 3;

            // Tambah tinggi jika isUsingAudio dicentang
            if (isUsingAudioProp.boolValue)
            {
                height += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            }

            return height;
        }
    }

    [CustomPropertyDrawer(typeof(DirectFeedback.FeedbackContent.AnimationContent))]
    public class AnimationContentDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            // Mendapatkan properti
            var animationNameProp = property.FindPropertyRelative("animationName");
            var isUsingAudioProp = property.FindPropertyRelative("isUsingAudio");
            var speakAudioProp = property.FindPropertyRelative("speakAudio");

            // Menampilkan animationName
            position.height = EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(position, animationNameProp);

            position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            // Menampilkan isUsingAudio
            position.height = EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(position, isUsingAudioProp);

            position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            // Jika isUsingAudio diaktifkan, tampilkan speakAudio
            if (isUsingAudioProp.boolValue)
            {
                EditorGUI.PropertyField(position, speakAudioProp);
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var isUsingAudioProp = property.FindPropertyRelative("isUsingAudio");

            // Tinggi dasar untuk animationName dan isUsingAudio
            float height = EditorGUIUtility.singleLineHeight * 2 + EditorGUIUtility.standardVerticalSpacing * 2;

            // Tambah tinggi jika isUsingAudio dicentang
            if (isUsingAudioProp.boolValue)
            {
                height += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            }

            return height;
        }
    }
}
#endif
