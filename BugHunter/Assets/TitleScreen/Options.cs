using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Options : MonoBehaviour
{
    public List<Transform> Icons;
    // Start is called before the first frame update
    void Start()
    {
       
        for (int i = 0; i < transform.childCount; i++)
        {
            Icons.Add(transform.GetChild(i));
            if (i > 0) Icons[i].gameObject.SetActive(false);
        }
    }

    // Update is called once per frame

    public void OnOptionsClicked()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Icons[i].gameObject.SetActive(true);
        }
        transform.parent.GetComponentInChildren<TitleScreen>().gameObject.SetActive(false);
        gameObject.GetComponent<Image>().enabled = false;
        Icons[0].gameObject.SetActive(false);
    }
}
