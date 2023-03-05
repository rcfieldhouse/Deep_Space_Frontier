using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowSlime : MonoBehaviour
{
    public Transform Slime;
    // Update is called once per frame
    void Update()
    {
        transform.position = Slime.position;
    }
}
