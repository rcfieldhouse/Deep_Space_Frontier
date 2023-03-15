using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCrouch : MonoBehaviour
{
    private void Awake()
    {
        Invoke(nameof(CorrectTheThing), 0.1f);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.localPosition =new Vector3(0.0f,0.5f- (0.5f* (0.7f-Mathf.Abs(transform.parent.rotation.x))),0.0f);
    }
    private void CorrectTheThing()
    {
        for (int i = 0; i < 3; i++)
        {
            transform.parent.GetChild(i).gameObject.SetActive(false);
        }
        transform.parent.GetChild(0).gameObject.SetActive(true);
    }
}
