using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntManager : MonoBehaviour
{
    public GameObject Player;
    //array of grunts for hive mind behaviour
    public GameObject[] grunt= new GameObject[10];
    // Start is called before the first frame update
    void Start()
    {
        //GetComponentInChildren<GroundAi>().player = Player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
