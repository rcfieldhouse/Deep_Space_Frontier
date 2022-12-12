using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportAbility : MonoBehaviour
{
    public CommandProcessor _CommandProcessor;
    public Camera Cam;
    private GameObject TeleportInstance;

    [SerializeField] private GameObject teleporterPrefab;
    //private GameObject turretPrefab;
    [SerializeField] private GameObject TeleportOrb;
    // Start is called before the first frame update

    private void Awake()
    {
        _CommandProcessor = GameObject.Find("GameManager").GetComponent<CommandProcessor>();
        Cam = GameObject.Find("MainCamera").GetComponent<Camera>();
        teleporterPrefab = GameObject.Find("Teleporter");
        TeleportOrb = GameObject.Find("ProximtyMine");

        PlayerInput.UseAbility += PlaceTeleport;
        PlayerInput.Undo += Undo;


        TeleportInstance = Instantiate(TeleportOrb);
    }
    private void OnDestroy()
    {
        PlayerInput.UseAbility -= PlaceTeleport;
        PlayerInput.Undo -= Undo;
    }
    public void PlaceTeleport()
    {
        Vector3 rayOrigin = Cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        Ray ray = Cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        GameObject myTeleporter;

        //Hardcoding 25 as the range
        if (Physics.Raycast(ray, out hitInfo, 25))
        {
            myTeleporter = Instantiate(teleporterPrefab);
            myTeleporter.transform.position = hitInfo.point;
            myTeleporter.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);

            MoveCommand moveCommand = new MoveCommand(TeleportInstance, hitInfo.point);


            _CommandProcessor.ExecuteCommand(moveCommand);
        }
    }
    public void Undo() { 
            _CommandProcessor.Undo();
    }
}
