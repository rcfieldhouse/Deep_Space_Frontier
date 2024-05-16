using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ThrowableSwap : MonoBehaviour
{

    public List<Transform> Icons;
    private int Selection = 0;
    [SerializeField] private GrenadeManager GrenadeManager;
    private PlayerInput Player;
    // Start is called before the first frame update
    void Awake()
    {
        

        for (int i = 0; i<transform.childCount-1;i++)
        {
            Icons.Add(transform.GetChild(i));
        }
        Invoke(nameof(FindDeBoi), 0.1f);
      
    }
    private void OnDestroy()
    {
        Player.TabThrowable -= ChooseIcon;

    }
    private void FindDeBoi()
    {
        GrenadeManager = transform.parent.parent.gameObject.GetComponentInChildren<GrenadeManager>();
        Player = GrenadeManager.gameObject.GetComponent<PlayerInput>();
        Player.TabThrowable += ChooseIcon;
        DisplayNum(Selection);
    }
    private void ChooseIcon()
    {
        Selection++;
        if (Selection > 1)
            Selection = 0;
        DisplayInfo();
    }
    public void DisplayInfo()
    {
        for (int i = 0; i < Icons.Count; i++)
        {
            Icons[i].gameObject.SetActive(false);
        }
        Icons[Selection].gameObject.SetActive(true);
        DisplayNum(Selection);
    }
 
    public void DisplayNum(int Choice)
    {   
        if (Choice == 0)
        {
          GetComponentInChildren<TextMeshProUGUI>().text=GrenadeManager.GetNumNades().ToString();
        }
        else if (Choice == 1)
        {
            int i=0;
            if (GrenadeManager.GetHasFruit() == false) i = 0;
            if (GrenadeManager.GetHasFruit() == true) i = 1;
            GetComponentInChildren<TextMeshProUGUI>().text = i.ToString();
        }
    }
}
