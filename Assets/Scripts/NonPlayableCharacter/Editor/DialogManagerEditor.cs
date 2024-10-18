#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Smarteye.VRGardening.NPC
{
    // [CustomEditor(typeof(DialogManager))]
    public class DialogManagerEditor : Editor
    {
        /* private SerializedProperty listDialogSection;
        private SerializedProperty dialogQuestion;
        private SerializedProperty canvasOpen;

        // Variabel pagination
        private int currentPage = 0;
        private const int itemsPerPage = 3; // Maksimal 3 item per halaman

        private void OnEnable()
        {
            // Mendapatkan referensi dari serialized properties
            listDialogSection = serializedObject.FindProperty("ListDialogSection");
            dialogQuestion = serializedObject.FindProperty("canvasDialogQuestion");
            canvasOpen = serializedObject.FindProperty("canvasBtnOpenDialog");
        }

        public override void OnInspectorGUI()
        {
            // Update serialized object
            serializedObject.Update();

            // Pagination untuk ListDialogSection
            int totalItems = listDialogSection.arraySize;
            int totalPages = Mathf.CeilToInt((float)totalItems / itemsPerPage); // Menghitung total halaman

            // Menampilkan custom list dengan pagination
            EditorGUILayout.LabelField("List Dialog Section", EditorStyles.boldLabel);

            int startItem = currentPage * itemsPerPage; // Elemen pertama dari halaman
            int endItem = Mathf.Min(startItem + itemsPerPage, totalItems); // Elemen terakhir dari halaman

            for (int i = startItem; i < endItem; i++)
            {
                SerializedProperty dialogSection = listDialogSection.GetArrayElementAtIndex(i);
                SerializedProperty dialogArea = dialogSection.FindPropertyRelative("dialogArea");

                EditorGUILayout.BeginVertical(GUI.skin.box); // Membuat area terkotak untuk tiap elemen list

                // Menampilkan nilai enum DialogArea sebagai label
                EditorGUILayout.LabelField($"==> Dialog Section: {dialogArea.enumDisplayNames[dialogArea.enumValueIndex]} <==", EditorStyles.boldLabel);

                // Menampilkan properti dialogArea dan dialogContents dalam ListDialogSection
                EditorGUILayout.PropertyField(dialogArea, new GUIContent("Dialog Area"));
                EditorGUILayout.PropertyField(dialogSection.FindPropertyRelative("dialogContents"), new GUIContent("Dialog Contents"));

                // Tombol hapus elemen
                if (GUILayout.Button("Remove Section"))
                {
                    listDialogSection.DeleteArrayElementAtIndex(i);
                }

                EditorGUILayout.EndVertical();
            }

            // Tombol untuk menambahkan elemen baru ke ListDialogSection
            if (GUILayout.Button("Add New Dialog Section"))
            {
                listDialogSection.InsertArrayElementAtIndex(listDialogSection.arraySize);
            }

            // Kontrol pagination
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Previous Page") && currentPage > 0)
            {
                currentPage--;
            }

            EditorGUILayout.LabelField($"Page {currentPage + 1} of {totalPages}", EditorStyles.centeredGreyMiniLabel, GUILayout.Width(100));

            if (GUILayout.Button("Next Page") && currentPage < totalPages - 1)
            {
                currentPage++;
            }

            EditorGUILayout.EndHorizontal();

            // Menampilkan properti lainnya di bagian akhir
            EditorGUILayout.PropertyField(dialogQuestion, new GUIContent("Canvas Dialog Question"));
            EditorGUILayout.PropertyField(canvasOpen, new GUIContent("Canvas Button Open Dialog"));

            // Apply changes to serialized object
            serializedObject.ApplyModifiedProperties();
        } */
    }
}
#endif