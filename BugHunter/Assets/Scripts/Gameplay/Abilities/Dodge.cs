using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodge : MonoBehaviour
{
    //use this to pass in the data
   
    private CharacterController characterController;
    private GameObject[] Cameras;
    private Vector3 RollVector = Vector3.forward;
    [Range(0, 1)] public  WaitForSeconds RollTimer = new WaitForSeconds(0.75f);
    // Start is called before the first frame update
    void Start()
    {
        // public GameObject CameraMain,CameraCrouch,CameraDodge, CameraManager,WeaponCamera;
        //these r the instances of the cameras
        characterController = GetComponent<CharacterController>();
        Cameras = characterController.GetCameras();
        PlayerInput.UseAbility += UseDodge;
    }
    private void UseDodge()
    {
        StartCoroutine(DoTheRoll());
        SwitchDodgeCam();
    }
    private void disableCams(bool var)
    {   //set all cameras to false, if called by a function that is setting that camera to false
        //set default cam to main
        Cameras[1].SetActive(false);
        Cameras[2].SetActive(false);
        Cameras[0].SetActive(!var);
    }
    private void SwitchDodgeCam()
    {
        disableCams(true);
        Cameras[2].SetActive(true);
        StartCoroutine(RollTime());
    }
    private IEnumerator RollTime()
    {
        Cameras[4].SetActive(false);
        yield return RollTimer;
        disableCams(false);
        Cameras[4].SetActive(true);

    }
    private IEnumerator DoTheRoll()
    {
        RollVector = GetComponent<Rigidbody>().velocity.normalized;
        if (RollVector == Vector3.zero || GetComponent<Rigidbody>().velocity.magnitude < 0.1)
            RollVector = Cameras[4].transform.rotation * Vector3.forward;

        yield return new WaitForEndOfFrame();
        GetComponent<HealthSystem>().SetInvulnerable(true);
        characterController.SetSuspendMovement(true) ;
        yield return RollTimer;
        GetComponent<HealthSystem>().SetInvulnerable(false);
        characterController.SetSuspendMovement(false);
    }
    public Vector3 GetRollVector()
    {
        return RollVector;
    }

}
