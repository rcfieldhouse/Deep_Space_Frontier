using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAbility : MonoBehaviour
{
    public CommandProcessor _CommandProcessor;
    public Camera Cam;

    [SerializeField]
    private GameObject teleporterPrefab;
    private GameObject turretPrefab;
    private GameObject turretInstance;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlaceTurret()
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

            MoveCommand moveCommand = new MoveCommand(turretInstance, hitInfo.point);


            _CommandProcessor.ExecuteCommand(moveCommand);
        }
    }
}
