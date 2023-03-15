using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeClass : MonoBehaviour
{
    private GUIHolder gui;
    private PlayerInput PlayerInput;
    private GameObject Player;
    public GameObject prefab;
    public ClassType ClassType;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
            return;

        Player = other.transform.gameObject;
        gui = Player.transform.parent.GetComponentInChildren<GUIHolder>();
        gui.PickupPrompt.SetActive(true);
        PlayerInput = Player.GetComponent<PlayerInput>();
        PlayerInput.Interact += ChangeClassType;
        Debug.Log("change");
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player")
            return;

        gui.PickupPrompt.SetActive(false);     
        PlayerInput.Interact -= ChangeClassType;
        PlayerInput = null ;
        Player = null;
        gui = null;
        Debug.Log("change");
    }
    public void ChangeClassType()
    {
        Transform SpawnPoint = Player.transform.GetChild(0).transform;
        Destroy(Player.transform.parent.gameObject);
        Vector3 offset = (Vector3.Normalize(Player.transform.GetChild(0).transform.position - transform.position));
        GameObject NewPlayer=Instantiate(prefab, SpawnPoint.position,Quaternion.identity);
        NewPlayer.transform.GetChild(0).GetComponent<ClassCreator>().SetClass(ClassType);
    }
}
