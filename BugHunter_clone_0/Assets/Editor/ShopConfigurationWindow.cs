using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ShopConfigurationWindow : EditorWindow
{
    //private 
    [MenuItem("Window/Tools/Shop Configuration")]
    public static void ShowWindow()
    {
        GetWindow<ShopConfigurationWindow>();

    }
    private void OnGUI()
    {
        //window code here
    }
}
