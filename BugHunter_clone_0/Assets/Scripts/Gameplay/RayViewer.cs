using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayViewer : MonoBehaviour
{
    public float weaponRange = 50f;                       // Distance in Unity units over which the Debug.DrawRay will be drawn

    [SerializeField]
    private Camera fpsCam;                                // Holds a reference to the first person camera

    private void Awake()
    {
        weaponRange = GetComponent<Gun>().WeaponRange;
    }
    void Update()
    {
        // Create a vector at the center of our camera's viewport
        Vector3 lineOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

        // Draw a line in the Scene View  from the point lineOrigin in the direction of fpsCam.transform.forward * weaponRange, using the color green
        Debug.DrawRay(lineOrigin, fpsCam.transform.forward * weaponRange, Color.green);


        for (int i = 1; i < 9; i++)
        {
          //  Debug.Log(i / 9.0f);
            Color color = Color.Lerp(Color.green, Color.red, i/9.0f);
            Debug.DrawRay(lineOrigin+(fpsCam.transform.forward * weaponRange +   fpsCam.transform.forward * weaponRange* (((i - 1) / 3.0f))), (fpsCam.transform.forward * weaponRange + fpsCam.transform.forward * weaponRange * (((i) / 3.0f) )), color);
            //Debug.Log(((float)i / 3.0f)*WeaponRange);

        }

        
    }
}
