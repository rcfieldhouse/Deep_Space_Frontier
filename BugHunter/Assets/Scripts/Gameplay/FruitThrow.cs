using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitThrow : MonoBehaviour
{
    public GameObject player;
    private Rigidbody Rigidbody;
    [SerializeField]
    private LayerMask whatIsBarrier;
    // Start is called before the first frame update
    void Start()
    {
          Rigidbody = GetComponent<Rigidbody>(); 
          player = GameObject.Find("MixamoCharacter");
        
    }

    // Update is called once per frame

    public void ThrowFruit(Vector3 ThrowVector)
    {
        Rigidbody.isKinematic = false;
        Rigidbody.transform.parent = null;
        Rigidbody.velocity = ThrowVector;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Barrier")
        {
            collision.gameObject.SetActive(false);
            ResetFruit();
        }
     
    }
    public void ResetFruit()
    {
        Rigidbody.gameObject.transform.SetParent(player.transform);
        gameObject.transform.localPosition = new Vector3(0.0f, 2.0f, 1.0f);
        gameObject.SetActive(false);
    }
}
