using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonPickup : MonoBehaviour
{
    private GameObject icon,reticle;
    private GameObject RelicCannon,RelicCannonInstance,Player;
    private bool _InRange = false;
    private PlayerInput PlayerInput;
    // Start is called before the first frame update


    private void Pickup()
    {
        if (_InRange==true)
        {
            RelicCannon = Resources.Load<GameObject>("RelicCannon");
            RelicCannonInstance= Instantiate(RelicCannon);
      
            RelicCannonInstance.transform.SetParent(Player.transform.parent.GetComponentInChildren<WeaponSwap>().gameObject.transform);
            Player.transform.parent.GetComponentInChildren<WeaponSwap>().WeaponArray.Add(RelicCannonInstance);

             icon = Player.GetComponent<GUIHolder>().GUI.GetComponentInChildren<GunIconUI>().transform.GetChild(3).gameObject;

            Player.GetComponent<GUIHolder>().GUI.GetComponentInChildren<GunIconUI>().Icons.Add(icon);

           
            reticle = Player.GetComponent<GUIHolder>().GUI.GetComponentInChildren<CrosshairsHolder>().transform.GetChild(5).gameObject;
            Player.transform.parent.GetChild(1).GetComponentInChildren<WeaponSwap>().RecticleArray.Add(reticle);
          

            Player.GetComponent<GUIHolder>().GUI.GetComponentInChildren<AmmoChangeUI>().magazineSize.Add(RelicCannonInstance.GetComponent<WeaponInfo>());

            Player.GetComponent<GUIHolder>().PickupPrompt.SetActive(false);

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
            Player.GetComponent<GUIHolder>().PickupPrompt.SetActive(true);
            PlayerInput = Player.GetComponent<PlayerInput>();
            PlayerInput.Interact += Pickup;
        }
      
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Player.GetComponent<GUIHolder>().PickupPrompt.SetActive(false);
            Player = null;
            _InRange = false;
            PlayerInput = null;
        }
    }
}
