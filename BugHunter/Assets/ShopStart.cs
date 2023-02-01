using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopStart : MonoBehaviour
{
    bool playerIsInShopRange = false;
    NPC shopOwner;

    // Start is called before the first frame update
    void Start()
    {
        PlayerInput.Interact += OpenShop;
        shopOwner.GetComponent<Merchant>();
    }

    private void OnDisable()
    {
        PlayerInput.Interact -= OpenShop;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.body.tag == "Player")
        {
            playerIsInShopRange = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.body.tag == "Player")
        {
            playerIsInShopRange = false;
        }
    }

    private void OpenShop()
    {
        if (playerIsInShopRange)
        {
            shopOwner.VendorUI();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
