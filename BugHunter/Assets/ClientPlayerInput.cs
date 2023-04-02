using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ClientPlayerInput : MonoBehaviour
{
	public PlayerInput playerInput;

	public void Start()
	{
		playerInput = GetComponent<PlayerInput>();
	}

	// Update is called once per frame
	void Update()
	{
		Vector2	vector;
		//vector.x = Input.GetAxis("Mouse X");
		//vector.y = Input.GetAxis("Mouse Y");
		//playerInput.LookInput(vector);
		Vector2 vec;
		//vec.x = Input.GetAxisRaw("Horizontal");
		//vec.y = Input.GetAxisRaw("Vertical");
		//playerInput.MoveInput(vec);

		playerInput.MouseScrollInput(Input.GetAxisRaw("Mouse ScrollWheel"));

		if (Input.GetKeyUp(KeyCode.LeftShift))
			playerInput.SprintingFalse(); 
		if (Input.GetKeyDown(KeyCode.LeftShift))
			playerInput.SprintingTrue();     
		if (Input.GetKeyDown("space"))
			playerInput.Jump();
		if (Input.GetKeyDown(KeyCode.G))
			playerInput.ToggleThrowable();
		if (Input.GetKeyDown(KeyCode.C))
			playerInput.UseClassAbility();
		if (Input.GetKeyDown(KeyCode.Q))
			playerInput.CookGrenade();
		if (Input.GetKeyUp(KeyCode.Q))
			playerInput.ReleaseGrenade();
		if (Input.GetKeyDown(KeyCode.Escape))
			playerInput.PausePlayer();
		if (Input.GetKeyDown(KeyCode.E))
			playerInput.BeginInteract();
		if (Input.GetKeyUp(KeyCode.E))
			playerInput.EndInteract();
		if (Input.GetKeyDown(KeyCode.R))
			playerInput.PlayerReload();
		if (Input.GetButtonDown("Fire1"))
			playerInput.ShootGun();
		if (Input.GetButtonUp("Fire1"))
			playerInput.ChamberGun();
		if (Input.GetButtonDown("Fire2"))
			playerInput.Aim();
		if (Input.GetButtonUp("Fire2"))
			playerInput.ReleaseAim();
		if (Input.GetKeyDown(KeyCode.Alpha1))
			playerInput.SwapPrimaryWeapon();
		if (Input.GetKeyDown(KeyCode.Alpha2))
			playerInput.SwapSecondaryWeapon();
		if (Input.GetKeyDown(KeyCode.O))
			playerInput.RevivePlayer();
		if (Input.GetKeyDown(KeyCode.L))
			playerInput.GiveUpAndDie();

	}
}
