using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleZoom : MonoBehaviour
{
    [SerializeField] private Material mat;

    // Update is called once per frame
    private void Update()
    {
        Vector2 screenPixels = Camera.main.WorldToScreenPoint(transform.position);
        screenPixels = new Vector2(screenPixels.x / Screen.width, screenPixels.y / Screen.height);

        mat.SetVector("ObjectToScreenPosition", screenPixels);
    }
}
