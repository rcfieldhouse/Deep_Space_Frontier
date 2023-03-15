using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomPropertyDrawer(typeof(Loot))]
public class LootDrawer : PropertyDrawer
{
    private SerializedProperty _name;
    private SerializedProperty _quantity;
    private SerializedProperty _sprite;
    private SerializedProperty _type;



    public override void OnGUI(Rect position, 
        SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        //fill our properties
        _name = property.FindPropertyRelative("lootName");
        _quantity = property.FindPropertyRelative("quantity");
        _sprite = property.FindPropertyRelative("lootSprite");
        _type = property.FindPropertyRelative("lootType");

        //Drawing Instructions
        Rect foldOutBox = new Rect(position.min.x, position.min.y,
            position.size.x, EditorGUIUtility.singleLineHeight);
        property.isExpanded = EditorGUI.Foldout(foldOutBox, property.isExpanded,label);

        //Drawing here
        if (property.isExpanded)
        {
            DrawTypeProperty(position);
            DrawNameProperty(position);
            DrawQuantityProperty(position);
            DrawSpriteProperty(position);            
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight
      (SerializedProperty property, GUIContent label)
    {
        int totalLines = 1;

        //for the dropdown arrow
        if (property.isExpanded)
        {
            totalLines += 4;
        }

        return (EditorGUIUtility.singleLineHeight * totalLines);
    }

    private void DrawTypeProperty(Rect position)
    {
        EditorGUIUtility.labelWidth = 100;
        Rect drawArea = new Rect(position.min.x,
            position.min.y + (EditorGUIUtility.singleLineHeight * 2),
            position.size.x * .7f, EditorGUIUtility.singleLineHeight);

        EditorGUI.PropertyField(drawArea, _type, new GUIContent("Loot Type:"));
    }

    private void DrawSpriteProperty(Rect position)
    {
        //throw new NotImplementedException();
    }

    private void DrawQuantityProperty(Rect position)
    {
        Rect drawArea = new(position.min.x + (position.width * .5f),
            position.min.y + EditorGUIUtility.singleLineHeight,
            position.size.x * .5f, EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(drawArea, _quantity, new GUIContent("Quantity"));
    }

    private void DrawNameProperty(Rect position)
    {
        EditorGUIUtility.labelWidth = 60;
        float xPos = position.min.x;
        float yPos = position.min.y + EditorGUIUtility.singleLineHeight;
        float width = position.size.x * .4f;
        float height = EditorGUIUtility.singleLineHeight;

        Rect drawArea = new Rect(xPos, yPos, width, height);
        EditorGUI.PropertyField(drawArea, _name, new GUIContent("Name: "));
    }

  
}
