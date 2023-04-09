using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TurretAbility : MonoBehaviour
{
    public static Action<int> UsedTurret;
    public static Action ClearedTurret;
    public List<GameObject> Turrets;
    public GameObject TurretPrefab;
    public Camera Cam;
    private PlayerInput Player;
    private int TurretCount = 2;
    private float CurrentRegenTime=0;
    // Start is called before the first frame update
    private void Awake()
    {
        Turrets = new List<GameObject>();
        Player = GetComponent<PlayerInput>();
        Player.UseAbility += PlaceTurret;
        Player.Undo += ClearTurrets;
        
        TurretPrefab = Resources.Load<GameObject>("Turret");
        Cam = transform.parent.GetChild(1).GetChild(3).GetComponent<Camera>();
        Invoke(nameof(Wait), 0.1f);
    }
    private void Update()
    {
        if (TurretCount != 2)
            CurrentRegenTime += Time.deltaTime;
        if (CurrentRegenTime > 20 && TurretCount < 2)
        {
            CurrentRegenTime = 0;
            TurretCount++;
            UsedTurret.Invoke(TurretCount);
        }    
    }
    private void Wait()
    {      
        UsedTurret.Invoke(TurretCount);
    }
    private void OnDestroy()
    {
        Player.UseAbility -= PlaceTurret;
        Player.Undo -= ClearTurrets;
    }
    public void PlaceTurret()
    {
        if (TurretCount <= 0)
            return;

        RaycastHit Hit;

        GameObject Turret;
        Vector3 rayOrigin = Cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        if (Physics.Raycast(rayOrigin, Cam.transform.forward * 25.0f, out Hit, 25.0f))
        {
            TurretCount--;
            //&& Turrets.Count<3
            FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Turret_Place");
            Turret = Instantiate(TurretPrefab);
            Turret.transform.position = Hit.point + new Vector3(0.0f, 3.0f, 0.0f);
            Turret.transform.rotation = Quaternion.FromToRotation(Vector3.up, Hit.normal);

            if (Hit.transform.gameObject.tag == "Ground" && (Mathf.Abs(Turret.transform.rotation.x) < 0.15f && Mathf.Abs(Turret.transform.rotation.z) < 0.15f))
            {
                Turrets.Add(Turret);
                UsedTurret.Invoke(TurretCount);
            }
            else
                Destroy(Turret);

        }
    }
   
    public void ClearTurrets()
    {
        for (int i = 0; i < Turrets.Count; i++)
        {
           Turrets[i].GetComponent<HealthSystem>().ModifyHealth(transform, -Turrets[i].GetComponent<HealthSystem>().GetHealth());
        }
        Turrets.Clear();
    }
}