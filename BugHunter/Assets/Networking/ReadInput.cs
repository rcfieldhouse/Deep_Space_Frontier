using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadInput : MonoBehaviour
{
    InputField field;

    public void Awake()
    {
        field = GetComponent<InputField>();
    }
    public void ReadStringInput(string message)
    {
        NetworkSend.SendMessage(message);
        field.text = "";
    }
}
