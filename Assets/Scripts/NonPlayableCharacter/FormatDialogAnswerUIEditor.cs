using UnityEditor;
using UnityEngine;

namespace Smarteye.VRGardening.NPC
{
    [CustomEditor(typeof(FormatDialogAnswerUI), true)]
    public class FormatDialogAnswerUIEditor : Editor
    {
        private SerializedProperty contentType;
        private SerializedProperty anserWithTextComponent;
        private SerializedProperty anserWithTextAndPhotoComponent;

        private void OnEnable()
        {
            contentType = serializedObject.FindProperty("contentType");
            anserWithTextComponent = serializedObject.FindProperty("anserWithTextComponent");
            anserWithTextAndPhotoComponent = serializedObject.FindProperty("anserWithTextAndPhotoComponent");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            // Menampilkan pilihan contentType
            EditorGUILayout.PropertyField(contentType, new GUIContent("Content Type"));

            // Menampilkan properti sesuai dengan nilai contentType
            DialogManager.DialogSection.DialogContent.QnAContent.ContentType type = (DialogManager.DialogSection.DialogContent.QnAContent.ContentType)contentType.enumValueIndex;

            switch (type)
            {
                case DialogManager.DialogSection.DialogContent.QnAContent.ContentType.AnserWithText:
                    EditorGUILayout.PropertyField(anserWithTextComponent, new GUIContent("Anser With Text Component"), true);
                    break;
                case DialogManager.DialogSection.DialogContent.QnAContent.ContentType.AnserWithTextAndPhoto:
                    EditorGUILayout.PropertyField(anserWithTextAndPhotoComponent, new GUIContent("Anser With Text And Photo Component"), true);
                    break;
                case DialogManager.DialogSection.DialogContent.QnAContent.ContentType.Custom:
                default:
                    // Jangan tampilkan apa pun jika None
                    break;
            }

            serializedObject.ApplyModifiedProperties();
        }
    }


}
