using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ThrowableSwap : MonoBehaviour
{

    public List<Transform> Icons;
    private int Selection = 0;
    [SerializeField] private GrenadeManager GrenadeManager;
    // Start is called before the first frame update
    void Awake()
    {
        PlayerInput.Interact += BeginRender;
        PlayerInput.TabThrowable += ChooseIcon;
        PlayerInput.Throw += StartTheRender;
        for (int i = 0; i<transform.childCount-1;i++)
        {
            Icons.Add(transform.GetChild(i));
        }
        StartCoroutine(FindDeBoi());
      
    }
    private void OnDestroy()
    {
        PlayerInput.Interact -= BeginRender;
        PlayerInput.TabThrowable -= ChooseIcon;
        PlayerInput.Throw -= StartTheRender;
    }
    private IEnumerator FindDeBoi()
    {
        yield return new WaitForSeconds(0.1f);
        GrenadeManager = transform.parent.parent.gameObject.GetComponentInChildren<GrenadeManager>();
      
        DisplayNum(Selection);
    }
    private void ChooseIcon()
    {
        Selection++;
        if (Selection > 1)
            Selection = 0;
        DisplayInfo();
            }
    void BeginRender()
    {
        StartTheRender(gameObject.transform.rotation);
    }
    private void StartTheRender(Quaternion quaternion)
    {
        StartCoroutine(WaitToRender());
    }
    private IEnumerator WaitToRender()
    {
        yield return new WaitForEndOfFrame();
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
 
    public void DisplayNum(int foo)
    {
   
        if (foo == 0)
        {
          GetComponentInChildren<TextMeshProUGUI>().text=GrenadeManager.GetNumNades().ToString();
        }
        else if (foo == 1)
        {
            int i=0;
            if (GrenadeManager.GetHasFruit() == false) i = 0;
            if (GrenadeManager.GetHasFruit() == true) i = 1;
            GetComponentInChildren<TextMeshProUGUI>().text = i.ToString();
        }
    }
}
