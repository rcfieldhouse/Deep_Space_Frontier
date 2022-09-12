using UnityEngine;
public class CameraBehaviour : MonoBehaviour
{

    [SerializeField] private float bobSpeed=0.5f,timer; 
    [SerializeField] private Vector3 bobDistance = new Vector3(0.0f,0.5f, 0.0f);
    private Vector3 offset = new Vector3(0.0f, 0.0f, 0.5f);
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
       
        timer = bobSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation=player.transform.rotation;
     // if (timer > 0.0f)
     // {
     //transform.position += bobDistance;
     //     timer -= Time.deltaTime;
     // }
     // else
     // {
     //     bobDistance *= -1;
     //     timer += bobSpeed;
     // }
    }
}
