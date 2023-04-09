using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerAnim : MonoBehaviour
{
    GameObject Parent;
    int Direction = 1;
    // Start is called before the first frame update
    private void Awake()
    {
        Parent = transform.parent.gameObject;
    }
    // Update is called once per frame
    void Update()
    {

        transform.position += Direction*Vector3.up / 50;
        if (Mathf.Abs(transform.position.y - Parent.transform.position.y) > 1.0f)
        {
            Direction *= -1;
        }
        transform.Rotate(Vector3.forward * (120 * Time.deltaTime));
    }
}
