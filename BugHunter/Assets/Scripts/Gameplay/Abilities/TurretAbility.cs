using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Netcode;
public class TurretAbility : NetworkBehaviour
{
    public static Action<int> UsedTurret;
    public static Action ClearedTurret;
    public List<GameObject> Turrets;
    public NetworkObject TurretPrefab;
    public Camera Cam;
    private PlayerInput Player;
    private int TurretCount = 2;
    private float CurrentRegenTime=0;

    GameObject Turret;

    // Start is called before the first frame update
    private void Awake()
    {
        Turrets = new List<GameObject>();
        Player = GetComponent<PlayerInput>();
        Player.UseAbility += PlaceTurret;
        Player.Undo += ClearTurrets;
        
        TurretPrefab = Resources.Load<GameObject>("Turret").GetComponent<NetworkObject>();
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
        Debug.Log("Pomf");
        if (TurretCount <= 0)
            return;
        Debug.Log("Pomf2");
        RaycastHit Hit;

        
        Vector3 rayOrigin = Cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        if (!Physics.Raycast(rayOrigin, Cam.transform.forward * 25.0f, out Hit, 25.0f))
            return;

        TurretCount--;
        NetworkObject TurretInstance = Instantiate(TurretPrefab, Hit.point+(Vector3.up*2), Quaternion.FromToRotation(Vector3.up, Hit.normal));
        TurretInstance.GetComponent<NetworkObject>().Spawn();

        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Turret_Place");

        Debug.Log("Pomf3");

        if (tag == "Ground" && (Mathf.Abs(TurretInstance.transform.rotation.x) < 0.15f && Mathf.Abs(TurretInstance.transform.rotation.z) < 0.15f))
        {
            Turrets.Add(Turret);
            UsedTurret.Invoke(TurretCount);
        }
        else
            Destroy(Turret);

    }

    [ServerRpc]
    public void PlaceTurretServerRpc(Vector3 point, Vector3 normal, string tag)
    {
        TurretCount--;
        NetworkObject TurretInstance = Instantiate(TurretPrefab, point, Quaternion.FromToRotation(Vector3.up, normal));
        TurretInstance.SpawnWithOwnership(OwnerClientId);
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Turret_Place");

        if (tag == "Ground" && (Mathf.Abs(TurretInstance.transform.rotation.x) < 0.15f && Mathf.Abs(TurretInstance.transform.rotation.z) < 0.15f))
        {
            Turrets.Add(Turret);
            UsedTurret.Invoke(TurretCount);
        }
        else
            Destroy(Turret);

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