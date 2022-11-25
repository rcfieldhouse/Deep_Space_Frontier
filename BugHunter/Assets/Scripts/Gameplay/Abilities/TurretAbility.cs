using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAbility : MonoBehaviour
{
    public List<GameObject> Turrets;
    private GameObject TurretPrefab;
    public Camera Cam;
    // Start is called before the first frame update
    private void Awake()
    {
        PlayerInput.UseAbility += PlaceTurret;
        TurretPrefab = GameObject.Find("Turret");
        Cam = GameObject.Find("MainCamera").GetComponent<Camera>();
    }
    public void PlaceTurret()
    {
        Vector3 rayOrigin = Cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        Ray ray = Cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        GameObject Turret;
        if (Physics.Raycast(ray, out hitInfo, 25))
        {
            //&& Turrets.Count<3
            Turret = Instantiate(TurretPrefab);
            Turret.transform.position = hitInfo.point+new Vector3(0.0f,3.0f,0.0f);
            Turret.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
            Turrets.Add(Turret);
        }
    }
}