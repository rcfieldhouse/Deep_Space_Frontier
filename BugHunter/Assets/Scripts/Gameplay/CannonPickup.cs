using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonPickup : MonoBehaviour
{
    private GameObject Prompt,icon,reticle;
    public GameObject RelicCannon,RelicCannonInstance,Player;
    private bool _InRange = false;
    // Start is called before the first frame update
    void Awake()
    {
        Prompt = GameObject.Find("PickupPrompt");
        PlayerInput.Interact += Pickup;
        StartCoroutine(Wait());
    }
    private IEnumerator Wait()
    {
        //timing it so it works 

        yield return new WaitForEndOfFrame();
        Prompt.SetActive(false);
    }
    private void Pickup()
    {
        if (_InRange==true)
        {
            RelicCannon = Resources.Load<GameObject>("RelicCannon");
            RelicCannonInstance= Instantiate(RelicCannon);
      
            RelicCannonInstance.transform.SetParent(Player.transform.parent.GetComponentInChildren<WeaponSwap>().gameObject.transform);
            Player.transform.parent.GetComponentInChildren<WeaponSwap>().WeaponArray.Add(RelicCannonInstance);

             icon = GameObject.Find("GunIcons").transform.GetChild(3).gameObject;

             GameObject.Find("GunIcons").GetComponent<GunIconUI>().Icons.Add(icon);

            // GameObject.Find("WeaponHolder").GetComponent<WeaponSwap>().RecticleArray.Add(reticle);
      

            Prompt.SetActive(false);
            Destroy(this.gameObject);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player = other.gameObject;
            _InRange = true;
            Prompt.SetActive(true);
        }
      
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Player = null;
            _InRange = false;
            Prompt.SetActive(false);
        }
    }
}
