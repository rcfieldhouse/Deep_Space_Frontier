using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
public class Zipline : MonoBehaviour
{
    public bool StartWithLineReady=false;
    public GameObject Player;
    private LineRenderer ZipLine;
    [Range(0, 5)] public float ButtonHoldTime = 1f;
    public Transform StartZipLine, EndZipLine;
    private PlayerInput PlayerInput;
    // Start is called before the first frame update
    public void PlayerReadyToZipline(Transform StartPoint, bool _IsEntering)
    {
        
        PlayerInput = Player.GetComponent<PlayerInput>();
        StartZipLine = StartPoint;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject != StartPoint.gameObject)
                EndZipLine = transform.GetChild(i);
        }
        ZipLine.SetPosition(0, StartZipLine.position);
        ZipLine.SetPosition(1, EndZipLine.position);
        if (_IsEntering == true) PlayerInput.Interact += UseZipline;
        else if (_IsEntering==false) PlayerInput.Interact-= UseZipline;
    }
    public void SetUsable()
    {
        PlayerInput = Player.GetComponent<PlayerInput>();
        PlayerInput.Interact += UseZipline;
    }
    public void UseZipline()
    {
      Player.GetComponent<GUIHolder>().PickupPrompt.SetActive(false);
        Player.GetComponent<CharacterController>().SetZiplinePoint(StartZipLine, EndZipLine);
    }
    public void ExitZipLine()
    {
        if(Player)
        Player.GetComponent<CharacterController>().ExitZipLine();
    }
    public void PlaceZipLine(Transform StartPoint) {

        StartZipLine = StartPoint;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<ZipPoint>().SetLinePlaced();
            transform.GetChild(i).gameObject.SetActive(true);
            if (transform.GetChild(i).gameObject != StartPoint.gameObject)
                EndZipLine = transform.GetChild(i);
        }
        ZipLine.SetPosition(0, StartZipLine.position);
        ZipLine.SetPosition(1, EndZipLine.position);
    }
    private void Awake()
    {
        ZipLine = GetComponent<LineRenderer>();
        for (int i=0; i<transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.AddComponent<ZipPoint>();
        }

        if(StartWithLineReady==true)
        {
            PlaceZipLine(transform.GetChild(0));
            transform.GetChild(0).GetComponent<ZipPoint>().SetLinePlaced();
            transform.GetChild(1).GetComponent<ZipPoint>().SetLinePlaced();
        }
    }
   
}
public class ZipPoint : MonoBehaviour
{
    private bool LinePlaced = false;
    private float elapsed = 0.0f;
    private bool StartTheHold = false, EndTheHold = false, holding = false;
    private PlayerInput PlayerInput;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
        
          GetComponentInParent<Zipline>().Player = other.gameObject;
            if (LinePlaced == false)
            {
                PlayerInput = other.gameObject.GetComponent<PlayerInput>();
                PlayerInput.Interact += StartPlacement;
                PlayerInput.InteractReleased += EndPlacement;
             
            }
            else if (LinePlaced == true)
            {
                other.gameObject.GetComponent<GUIHolder>().PickupPrompt.SetActive(true);
                GetComponentInParent<Zipline>().PlayerReadyToZipline(transform,true);
                GetComponentInParent<Zipline>().ExitZipLine();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        
        if (other.tag == "Player")
        {
          
            if (LinePlaced == false)
            {
              
                holding =false;
                PlayerInput.Interact -= StartPlacement;
                PlayerInput.InteractReleased -= EndPlacement;
                PlayerInput = null;
            }
            else if (LinePlaced == true)
            {
                other.gameObject.GetComponent<GUIHolder>().PickupPrompt.SetActive(false);
                GetComponentInParent<Zipline>().PlayerReadyToZipline(transform, false);
            }
            GetComponentInParent<Zipline>().Player = null;
        }
    }
    public void SetLinePlaced()
    {
        LinePlaced = true;
    }
    public void StartPlacement()
    {
        StartTheHold = true;
    }
    public void EndPlacement()
    {
        EndTheHold = true;
    }

    private void Update()
    {
        if (StartTheHold == true)
        {
             elapsed = 0.0f;
            StartTheHold = false;
            holding = true;
        }
        if (elapsed < GetComponentInParent<Zipline>().ButtonHoldTime&&holding==true)
        {
            if (EndTheHold == true)
            {
                holding = false;
                elapsed = 0.0f;
                EndTheHold = false;
            }
            elapsed += Time.deltaTime;         
        }
        else if (elapsed > GetComponentInParent<Zipline>().ButtonHoldTime)
        {
            GetComponentInParent<Zipline>().PlaceZipLine(transform);
            holding = false;
            elapsed = 0.0f;
            PlayerInput.Interact -= StartPlacement;
            PlayerInput.InteractReleased -= EndPlacement;
            GetComponentInParent<Zipline>().SetUsable();
        }
    }
}
