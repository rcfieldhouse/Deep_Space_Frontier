using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DamageIndicator : MonoBehaviour
{
    float num=0,offset;
    GameObject obj,Holder,text;
    private void Awake()
    {
        if (GetComponentInChildren<MeshRenderer>())
            offset =(GetComponentInChildren<MeshRenderer>().bounds.center.y + GetComponentInChildren<MeshRenderer>().bounds.extents.y);

        if (GetComponentInChildren<SkinnedMeshRenderer>())
        {
            Debug.Log(GetComponentInChildren<SkinnedMeshRenderer>().bounds.center.y + " + "+ GetComponentInChildren<SkinnedMeshRenderer>().bounds.extents.y);

            offset = (GetComponentInChildren<SkinnedMeshRenderer>().bounds.center.y + GetComponentInChildren<SkinnedMeshRenderer>().bounds.extents.y);
        }
           
       if (GetComponentInParent<AI>() != null)
           offset /= transform.parent.localScale.y;

        offset /= 2;

        Debug.Log(offset);
        if (gameObject.GetComponentInChildren<DamageIDHolder>() != null)
        {
            Holder = GetComponentInChildren<DamageIDHolder>().gameObject;
            text = GetComponentInChildren<TextMeshPro>().gameObject;
            gameObject.GetComponent<DamageIndicator>().RemoveThis();
                return;
        }
           
        obj = new GameObject();
        Holder = Instantiate(obj, transform, false);
        text = Instantiate(obj, Holder.transform, false);
        Destroy(obj);

     Holder.AddComponent<DamageIDHolder>();
     text.AddComponent<TextMeshPro>();

     Holder.transform.localPosition = Vector3.zero;

     Holder.GetComponent<DamageIDHolder>().transform.localPosition = Vector3.zero + offset*Vector3.up; 
     text.GetComponent<TextMeshPro>().fontSize = 8 ;
     text.GetComponent<TextMeshPro>().alignment = TextAlignmentOptions.Center;
     text.GetComponent<TextMeshPro>().text = 0.ToString();
    }
    public void SetIndicator(Transform transform,int Damage)
    {
        Holder.GetComponent<DamageIDHolder>().transform.LookAt(transform);
        Holder.GetComponent<DamageIDHolder>().transform.Rotate(Vector3.up, 180.0f);
        text.GetComponent<TextMeshPro>().text = (int.Parse(text.GetComponent<TextMeshPro>().text)+Damage).ToString();
        StartCoroutine(FancySchmancyTextEffect());
    }
    public IEnumerator FancySchmancyTextEffect()
    {
        while (num<1.0f)
        {
            num += 0.01f;
            yield return new WaitForSeconds(0.01f);
            text.GetComponent<TextMeshPro>().alpha = 1.0f - num;
            Holder.GetComponent<DamageIDHolder>().transform.localPosition = Vector3.zero + (offset + num) * Vector3.up;
        }
        Destroy(Holder);
        Destroy(this);
    }
    public void RemoveThis()
    {
        StopAllCoroutines();
        //this is to remove additional scripts on multiple hits
        Destroy(this);
    }
}
