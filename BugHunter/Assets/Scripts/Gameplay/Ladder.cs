using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private GameObject Prompt,Player;
    private bool _IsInTrigger = false;
    private void Start()
    {
        PlayerInput.Interact += UseLadder;
        Prompt = GameObject.Find("PickupPrompt");
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        Prompt.SetActive(true);
      
        if (other.tag == "Player")
        {
            _IsInTrigger = true;
            Player = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _IsInTrigger = false;
            Prompt.SetActive(false);
            Player.GetComponent<CharacterController>().SetIfOnLadder(false);
            Player = null;
        }
    }
    private void UseLadder()
    {
        if (Player.GetComponent<CharacterController>().GetIfOnLadder() == true)
        {
            //get off the ladder function
            _IsInTrigger = false;
            Prompt.SetActive(false);
            Player.GetComponent<CharacterController>().SetIfOnLadder(false);
            Player = null;
        }
        if (_IsInTrigger == true)
        {
            Prompt.SetActive(false) ;
            Player.GetComponent<CharacterController>().SetIfOnLadder(true);
        }
    }
}