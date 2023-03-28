using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ClientPlayerInput : MonoBehaviour
{
    public PlayerInput PlayerInput;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 vector;
        vector.x = Input.GetAxis("Mouse X");
        vector.y = Input.GetAxis("Mouse Y");
        PlayerInput.LookInput(vector);
        Vector2 vec;
        vec.x = Input.GetAxisRaw("Horizontal");
        vec.y = Input.GetAxisRaw("Vertical");
        PlayerInput.MoveInput(vec);

        PlayerInput.MouseScrollInput(Input.GetAxisRaw("Mouse ScrollWheel"));

        if (Input.GetKeyUp(KeyCode.LeftShift))
            PlayerInput.SprintingFalse(); 
        if (Input.GetKeyDown(KeyCode.LeftShift))
            PlayerInput.SprintingTrue();     
        if (Input.GetKeyDown("space"))
            PlayerInput.Jump();
        if (Input.GetKeyDown(KeyCode.G))
            PlayerInput.ToggleThrowable();
        if (Input.GetKeyDown(KeyCode.C))
            PlayerInput.UseClassAbility();
        if (Input.GetKeyDown(KeyCode.Q))
            PlayerInput.CookGrenade();
        if (Input.GetKeyUp(KeyCode.Q))
            PlayerInput.ReleaseGrenade();
        if (Input.GetKeyDown(KeyCode.Escape))
            PlayerInput.PausePlayer();
        if (Input.GetKeyDown(KeyCode.E))
            PlayerInput.BeginInteract();
        if (Input.GetKeyUp(KeyCode.E))
            PlayerInput.EndInteract();
        if (Input.GetKeyDown(KeyCode.R))
            PlayerInput.PlayerReload();
        if (Input.GetButtonDown("Fire1"))
            PlayerInput.ShootGun();
        if (Input.GetButtonUp("Fire1"))
            PlayerInput.ChamberGun();
        if (Input.GetButtonDown("Fire2"))
            PlayerInput.Aim();
        if (Input.GetButtonUp("Fire2"))
            PlayerInput.ReleaseAim();
        if (Input.GetKeyDown(KeyCode.Alpha1))
            PlayerInput.SwapPrimaryWeapon();
        if (Input.GetKeyDown(KeyCode.Alpha2))
            PlayerInput.SwapSecondaryWeapon();
        if (Input.GetKeyDown(KeyCode.O))
            PlayerInput.RevivePlayer();
        if (Input.GetKeyDown(KeyCode.L))
            PlayerInput.GiveUpAndDie();

    }
}
