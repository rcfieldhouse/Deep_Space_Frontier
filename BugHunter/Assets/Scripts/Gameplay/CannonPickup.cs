using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonPickup : MonoBehaviour
{
    private GameObject Prompt,icon,reticle;
    private GameObject RelicCannon,RelicCannonInstance,Player;
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


            reticle = GameObject.Find("Crosshairs").transform.GetChild(5).gameObject;
            GameObject.Find("WeaponHolder").GetComponent<WeaponSwap>().RecticleArray.Add(reticle);

            GameObject.Find("Ammo Counter").GetComponent<AmmoChangeUI>().magazineSize.Add(RelicCannonInstance.GetComponent<WeaponInfo>());
            Prompt.SetActive(false);

            RelicCannonInstance.transform.localPosition = new Vector3(0.3f, -0.25f, 0.667f);
            RelicCannonInstance.transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
            RelicCannonInstance.SetActive(false);
            _InRange = false;
            PlayerInput.Interact -= Pickup;
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
