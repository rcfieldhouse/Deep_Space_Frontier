using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TurretAbility : MonoBehaviour
{
    public static Action<float> UsedTurret;
    public static Action ClearedTurret;
    public List<GameObject> Turrets;
    public GameObject TurretPrefab;
    public Camera Cam;
    // Start is called before the first frame update
    private void Awake()
    {
        Turrets = new List<GameObject>();
 
        PlayerInput.UseAbility += PlaceTurret;
        PlayerInput.Undo += ClearTurrets;

        TurretPrefab = Resources.Load<GameObject>("Turret");
        Cam = GameObject.Find("MainCamera").GetComponent<Camera>();
        StartCoroutine(Wait());
    }
    private IEnumerator Wait()
    {      
        yield return new WaitForEndOfFrame();
        UsedTurret.Invoke(Turrets.Count);
    }
    private void OnDestroy()
    {
        PlayerInput.UseAbility -= PlaceTurret;
        PlayerInput.Undo -= ClearTurrets;
    }
    public void PlaceTurret()
    {
        GameObject Turret;
        Vector3 rayOrigin = Cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        Ray ray = Cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        
        if (Physics.Raycast(ray, out hitInfo, 25)&&Turrets.Count<2)
        {
            //&& Turrets.Count<3

            Turret = Instantiate(TurretPrefab);
            Turret.transform.position = hitInfo.point + new Vector3(0.0f, 3.0f, 0.0f);
            Turret.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);

            if (hitInfo.transform.gameObject.tag == "Ground" && (Mathf.Abs(Turret.transform.rotation.x) < 0.15f && Mathf.Abs(Turret.transform.rotation.z) < 0.15f))
            {
                Turrets.Add(Turret);
                UsedTurret.Invoke(Turrets.Count);
            }
            else
                Destroy(Turret);

        }
    }
   
    public void ClearTurrets()
    {
        for (int i = 0; i < Turrets.Count; i++)
        {
            Destroy(Turrets[i]);
        }
        ClearedTurret.Invoke();
        Turrets.Clear();
    }
}