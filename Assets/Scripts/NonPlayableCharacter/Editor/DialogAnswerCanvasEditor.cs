#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Smarteye.VRGardening.NPC
{
    [CustomEditor(typeof(DialogAnswerCanvas), true)]
    public class FormatDialogAnswerCanvasEditor : Editor
    {
        private SerializedProperty contentType;
        private SerializedProperty anserWithTextComponent;
        private SerializedProperty anserWithTextAndPhotoComponent;
        private SerializedProperty btnCloseCanvas;

        private void OnEnable()
        {
            contentType = serializedObject.FindProperty("contentType");
            anserWithTextComponent = serializedObject.FindProperty("anserWithTextComponent");
            anserWithTextAndPhotoComponent = serializedObject.FindProperty("anserWithTextAndPhotoComponent");
            btnCloseCanvas = serializedObject.FindProperty("btnCloseCanvas");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            // Menampilkan pilihan contentType
            EditorGUILayout.PropertyField(contentType, new GUIContent("Content Type"));

            // Menampilkan properti sesuai dengan nilai contentType
            DialogSection.DialogContent.QnAContent.ContentType type = (DialogSection.DialogContent.QnAContent.ContentType)contentType.enumValueIndex;

            switch (type)
            {
                case DialogSection.DialogContent.QnAContent.ContentType.AnswerWithText:
                    EditorGUILayout.PropertyField(anserWithTextComponent, new GUIContent("Anser With Text Component"), true);
                    break;
                case DialogSection.DialogContent.QnAContent.ContentType.AnswerWithTextAndPhoto:
                    EditorGUILayout.PropertyField(anserWithTextAndPhotoComponent, new GUIContent("Anser With Text And Photo Component"), true);
                    break;
                case DialogSection.DialogContent.QnAContent.ContentType.Custom:
                default:
                    // Jangan tampilkan apa pun jika None
                    break;
            }

            EditorGUILayout.Space(5f);
            EditorGUILayout.PropertyField(btnCloseCanvas, new GUIContent("Button Back to Question"), true);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif
