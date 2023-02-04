using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(Loot2))]
public class LootEditor : Editor
{
    private SerializedProperty quantity;
    private SerializedProperty lootName;
    private SerializedProperty lootType;
    private SerializedProperty Sprite;

    private void OnEnable()
    {
        quantity = serializedObject.FindProperty("quantity");
        lootName = serializedObject.FindProperty("name");
        lootType = serializedObject.FindProperty("lootType");
        Sprite = serializedObject.FindProperty("Sprite");
    }

    public override void OnInspectorGUI()
    {

        serializedObject.UpdateIfRequiredOrScript();

        Loot2 data = (Loot2)target;
        
        EditorGUILayout.LabelField(data.name.ToUpper(), EditorStyles.boldLabel);
        EditorGUILayout.Space(10);
        //preGUI render
        base.OnInspectorGUI();
        //postGUI render

        GUIDrawSprite(data.Sprite.textureRect, data.Sprite);

        float capacity = (float)data.Quantity / 99;
        ProgressBar(capacity, "Capacity");


        if (data.Sprite == null)
            EditorGUILayout.HelpBox(
                "Caution! You must specify a sprite in order for it to be displayed in the shop.", MessageType.Warning);
        if (data.Type == MonsterLoot.None)
            EditorGUILayout.HelpBox(
                "Caution! Loot must have a Type specified to be stored in the Inventory.", MessageType.Warning);

        if (GUILayout.Button("CHEAT!!!"))
        {
            Cheat();
        }

        serializedObject.ApplyModifiedProperties();
    }
    public static void GUIDrawSprite(Rect rect, Sprite sprite)
    {
        Rect spriteRect = sprite.rect;
        Texture2D tex = sprite.texture;
        GUI.DrawTextureWithTexCoords(rect, tex, new Rect
            (spriteRect.x / tex.width, spriteRect.y / tex.height, spriteRect.width / tex.width, spriteRect.height / tex.height));
    }
    private static void ProgressBar(float value, string label)
    {
        Rect rect = GUILayoutUtility.GetRect(18, 18, "TextField");
        EditorGUI.ProgressBar(rect, value, label);
        EditorGUILayout.Space(10);
    }

    /// <summary>
    /// Caps out the Capacity for a loot item for TESTING
    /// </summary>
    private void Cheat()
    {
        quantity.intValue = 99;
    }
}
