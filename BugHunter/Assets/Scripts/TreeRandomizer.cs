using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeRandomizer : MonoBehaviour
{
    public float Randomness = 0.0f;
    private Vector3 vec= new Vector3 (0.0f,0.0f,0.0f);
    // Start is called before the first frame update
    void Start()
    {
        vec.x = Random.Range(1 - Randomness, 1 + Randomness);
        vec.y = Random.Range(1 - Randomness, 1 + Randomness);
        vec.z = Random.Range(1 - Randomness, 1 + Randomness);
        Debug.Log(vec);
        //welp you cant do a vec * a vec soooooo we have this cursed bit of code
        //i apologize to anyone involved
        gameObject.transform.localScale = new Vector3(vec.x * gameObject.transform.localScale.x,vec.y * gameObject.transform.localScale.y, vec.z* gameObject.transform.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {
    
    }
}
