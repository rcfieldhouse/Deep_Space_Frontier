using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;




[CustomPropertyDrawer(typeof(SeperatorAttribute))]
public class SeperatorDrawer : DecoratorDrawer
{
    public override void OnGUI(Rect position)
    {
        //reference separater attribute
        SeperatorAttribute seperatorAttribute 
            = attribute as SeperatorAttribute;
        //define a line to draw
        Rect seperatorRect = new Rect(position.xMin, position.yMin + 
            seperatorAttribute.Spacing, 
            position.width, 
            seperatorAttribute.Height);


        //draw the line
        EditorGUI.DrawRect(seperatorRect, Color.white);
    }
    public override float GetHeight()
    {
        SeperatorAttribute seperatorAttribute = attribute as SeperatorAttribute;

        float totalSpacing = seperatorAttribute.Spacing 
            + seperatorAttribute.Height 
            + seperatorAttribute.Spacing;

        return totalSpacing;
    }
}
