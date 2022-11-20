using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class TitleScreen : MonoBehaviour
{
    public List<Transform> Icons;
    // Start is called before the first frame update
    void OnEnable()
    {

        //init at 1 to not get the text 
    
        for (int i = 0; i < transform.childCount ; i++)
        {
            Icons.Add(transform.GetChild(i));
            if (i > 0) Icons[i].gameObject.SetActive(false);
        }
        gameObject.GetComponent<Image>().enabled = true;
        Icons[0].gameObject.SetActive(true);
    }

   
    // Update is called once per frame


    public void PlayClicked()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
             Icons[i].gameObject.SetActive(true);
        }

        transform.parent.GetComponentInChildren<Options>().gameObject.SetActive(false);
        gameObject.GetComponent<Image>().enabled = false;
        transform.parent.GetComponentInChildren<QuitGame>().gameObject.SetActive(false);
        Icons[0].gameObject.SetActive(false);
    }
}
