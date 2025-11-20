using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AlmanaqueData))]
public class AlmanaqueDataEditor : Editor
{
    private SerializedProperty todosOsItens;

    private void OnEnable()
    {
        todosOsItens = serializedObject.FindProperty("todosOsItens");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.LabelField("Itens do Almanaque", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        if (todosOsItens != null)
        {
            for (int i = 0; i < todosOsItens.arraySize; i++)
            {
                SerializedProperty item = todosOsItens.GetArrayElementAtIndex(i);
                SerializedProperty nome = item.FindPropertyRelative("nome");
                SerializedProperty categoria = item.FindPropertyRelative("categoria");
                SerializedProperty descricao = item.FindPropertyRelative("descricao");

                // NOVOS CAMPOS
                SerializedProperty spriteIcon = item.FindPropertyRelative("spriteIcon");
                SerializedProperty spriteGrande = item.FindPropertyRelative("spriteGrande");

                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.LabelField($"Item {i + 1}", EditorStyles.boldLabel);

                EditorGUILayout.PropertyField(nome);
                EditorGUILayout.PropertyField(categoria);

                // EXIBE ÍCONE E IMAGEM GRANDE
                EditorGUILayout.PropertyField(spriteIcon, new GUIContent("Ícone"));
                EditorGUILayout.PropertyField(spriteGrande, new GUIContent("Imagem Grande"));

                EditorGUILayout.LabelField("Descrição:");
                descricao.stringValue = EditorGUILayout.TextArea(descricao.stringValue, GUILayout.MinHeight(60));

                if (GUILayout.Button("Remover Item"))
                {
                    todosOsItens.DeleteArrayElementAtIndex(i);
                }

                EditorGUILayout.EndVertical();
                EditorGUILayout.Space();
            }

            if (GUILayout.Button("➕ Adicionar Novo Item"))
            {
                todosOsItens.InsertArrayElementAtIndex(todosOsItens.arraySize);
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}
