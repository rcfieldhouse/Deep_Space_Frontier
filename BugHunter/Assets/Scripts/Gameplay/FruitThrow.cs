using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitThrow : MonoBehaviour
{
    private Transform Transform;
    public GameObject player;
    private Rigidbody Rigidbody;

    [Range(0.0f,30.0f)] public float DecayTime=0.0f;
    [SerializeField]
    private LayerMask whatIsBarrier;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>(); 
        player = GameObject.Find("MixamoCharacter");
        Rigidbody.isKinematic = true; 
    }

    // Update is called once per frame

    public void ThrowFruit(Vector3 ThrowVector,Transform transform)
    {
        Transform = transform;
        Rigidbody.isKinematic = false;
        Rigidbody.transform.parent = null;
        Rigidbody.velocity = ThrowVector;
        StartCoroutine(ResetOriginalFruit());
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Barrier")
        {
            collision.gameObject.AddComponent<Decay>().SetDecayTime(DecayTime);
            //collision.gameObject.SetActive(false);
            ResetFruit();
        }
    
    }
    private IEnumerator ResetOriginalFruit()
    {
        yield return new WaitForSeconds(7.5f);
        ResetFruit();
        Transform.gameObject.SetActive(true);
    }
    public void ResetFruit()
    {
        StopCoroutine(ResetOriginalFruit());
        Rigidbody.gameObject.transform.SetParent(player.transform);
        gameObject.transform.localPosition = new Vector3(0.0f, 2.5f, 1.5f);
        Rigidbody.isKinematic = true;
        gameObject.SetActive(false);
    }
}
