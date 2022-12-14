using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Dodge : MonoBehaviour
{
    public static Action<float> Dodged;
    //use this to pass in the data
    private bool _CanRoll = true; 
    private CharacterController characterController;
    public GameObject[] Cameras;
    private Vector3 RollVector = Vector3.zero;
    [Range(0, 1)] public  WaitForSeconds RollTimer = new WaitForSeconds(0.75f);
    private float Cooldown = 5f;
    [Range(0, 1)] public WaitForSeconds RollCoolDownTime ;
    // Start is called before the first frame update
    void Awake()
    {
        RollCoolDownTime = new WaitForSeconds(Cooldown);
        // public GameObject CameraMain,CameraCrouch,CameraDodge, CameraManager,WeaponCamera;
        //these r the instances of the cameras
        characterController = GetComponent<CharacterController>();
        Cameras = characterController.GetCameras();
        PlayerInput.UseAbility += UseDodge;
        disableCams(false);
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
        PlayerInput.UseAbility -= UseDodge;
    }
    private void UseDodge()
    {
        if (_CanRoll == true)
        {
            Dodged.Invoke(Cooldown);
            StartCoroutine(DoTheRoll());
            SwitchDodgeCam();
        }
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
            characterController.SetSuspendMovement(true);
            yield return RollTimer;
            GetComponent<HealthSystem>().SetInvulnerable(false);
            characterController.SetSuspendMovement(false);
            RollVector = Vector3.zero;
            StartCoroutine(StartRollCooldown());
        
    }
    private IEnumerator StartRollCooldown()
    {
        _CanRoll = false;
        yield return RollCoolDownTime;
        _CanRoll = true;
    }
    public Vector3 GetRollVector()
    {
        return RollVector;
    }

}
