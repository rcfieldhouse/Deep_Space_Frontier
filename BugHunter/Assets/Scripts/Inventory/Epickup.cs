using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Epickup : MonoBehaviour
{
    private GameObject Prompt;
    // this script is attached to collectable items and adds an item to the list when colliding with the player
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private float PickupRange;

    public void Start()
    {
        PlayerInput.PickupItem += Pickup;
        Prompt = GameObject.Find("PickupPrompt");
        StartCoroutine(AAAAAHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH());
       // Prompt.SetActive(false);
    }
    // adds the item to the inventory list then destroys it's self
    void Pickup()
    {
      if(Physics.CheckSphere(transform.position, PickupRange, whatIsPlayer)==true)
        {
            GameObject.Find("MixamoCharacter").GetComponent<GrenadeManager>().SetHasFruit(true,gameObject.transform);
            Prompt.SetActive(false);
            this.gameObject.SetActive(false);
        }
      
    }

    public void ResetFruit()
    {
        this.gameObject.SetActive(true);
    }

    // when the player collides with this object call the pickup function

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, PickupRange);

    }
    public void OnTriggerEnter(Collider other)
    {
        Prompt.SetActive(true);
    }
    public void OnTriggerExit(Collider other)
    {
        Prompt.SetActive(false);
    }
    private IEnumerator AAAAAHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH()
    {
        //timing it so it works 

        yield return new WaitForEndOfFrame();
        Prompt.SetActive(false);
    }
}
