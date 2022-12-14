using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadePickup : MonoBehaviour
{
    [Range(0, 5)] public int numGrenades=1;
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //do the thing
            collision.gameObject.GetComponent<GrenadeManager>().GainGrenades(numGrenades);

            //shhh this is my little sneaky, the action is attached to the render thing 
            //basically it updates the UI
            //this was def not Ryan so go ask someone else

            PlayerInput.Interact.Invoke();
            Destroy(gameObject);
        }
    }
}
