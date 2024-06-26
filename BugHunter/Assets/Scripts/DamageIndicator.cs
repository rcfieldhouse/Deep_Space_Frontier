using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DamageIndicator : MonoBehaviour
{
    float num=0,offset;
    public Transform DamageReceivedFrom;
    public GameObject obj,Holder,text;
   [HideInInspector] public bool reworkindicator=false;
   [HideInInspector] public Vector3 HitPos,LocalHit;
    private void Awake()
    {
        offset = 0;
        if (GetComponentInChildren<MeshRenderer>())
            offset =(GetComponentInChildren<MeshRenderer>().bounds.center.y + GetComponentInChildren<MeshRenderer>().bounds.extents.y);

        if (GetComponentInChildren<SkinnedMeshRenderer>())
            offset = (GetComponentInChildren<SkinnedMeshRenderer>().bounds.center.y + GetComponentInChildren<SkinnedMeshRenderer>().bounds.extents.y);
              
       if (GetComponentInParent<AI>() != null)
           offset /= transform.parent.localScale.y;

        offset /= 2;

        if (gameObject.GetComponentInChildren<DamageIDHolder>() != null)
        {
            Holder = GetComponentInChildren<DamageIDHolder>().gameObject;
            text = GetComponentInChildren<TextMeshPro>().gameObject;
            HitPos = gameObject.GetComponent<DamageIndicator>().HitPos;
            LocalHit = gameObject.GetComponent<DamageIndicator>().LocalHit;
            reworkindicator = gameObject.GetComponent<DamageIndicator>().reworkindicator;
            gameObject.GetComponent<DamageIndicator>().RemoveThis();
            if (Holder.transform.lossyScale.magnitude <= 1.0f)
                text.GetComponent<TextMeshPro>().fontSize = 20 * (2 - Holder.transform.lossyScale.magnitude);
            else text.GetComponent<TextMeshPro>().fontSize = 20;
            return;
        }
           
        obj = new GameObject();
        Holder = Instantiate(obj, transform, false);
        text = Instantiate(obj, Holder.transform, false);
        Destroy(obj);

     Holder.AddComponent<DamageIDHolder>();
     text.AddComponent<TextMeshPro>();

     Holder.transform.localPosition = Vector3.zero;
        if (Holder.transform.lossyScale.magnitude <= 1.0f)
            text.GetComponent<TextMeshPro>().fontSize = 20 * (2 - Holder.transform.lossyScale.magnitude);
        else text.GetComponent<TextMeshPro>().fontSize = 20;
        text.GetComponent<TextMeshPro>().alignment = TextAlignmentOptions.Center;
     text.GetComponent<TextMeshPro>().text = 0.ToString();

       // if (transform.parent.gameObject.GetComponent<TargetRange>() != null) offset = 5;
       // Holder.GetComponent<DamageIDHolder>().transform.localPosition = Vector3.zero + offset * Vector3.up;
        if (reworkindicator)
        {
            offset = 0.0f;
            Holder.GetComponent<DamageIDHolder>().transform.position = (Vector3.up * 1.25f) + HitPos;
        }

    }
    public void SetIndicator(Transform transform,int Damage,bool _IsCrit)
    {
        DamageReceivedFrom = transform;
        Holder.GetComponent<DamageIDHolder>().transform.LookAt(transform);
        Holder.GetComponent<DamageIDHolder>().transform.Rotate(Vector3.up, 180.0f);
        text.GetComponent<TextMeshPro>().text = (int.Parse(text.GetComponent<TextMeshPro>().text)+Damage).ToString();
        Holder.layer = 12;
        text.layer = 12;
        if (_IsCrit == true)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Creature/Enemy_Hurt Crit");
            text.GetComponent<TextMeshPro>().color = Color.red;
        }

        else
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Creature/Enemy_Hurt");
            text.GetComponent<TextMeshPro>().color = Color.white;
        }
        StartCoroutine(FancySchmancyTextEffect());
    }
    public void SetHisPos(Vector3 vec)
    {
        reworkindicator = true;
        HitPos = vec;
        LocalHit = HitPos - transform.position;
        Holder.GetComponent<DamageIDHolder>().transform.position = (Vector3.up * 1.25f) + vec;
        offset = 0.0f;
    }
    public IEnumerator FancySchmancyTextEffect()
    {
        while (num<1.0f)
        {
            //Debug.Log(Vector3.zero + (offset + num) * Vector3.up);
            num += 0.01f;
            yield return new WaitForSeconds(0.01f);
            if (text)
            text.GetComponent<TextMeshPro>().alpha = 1.0f - num;
            if (Holder) 
            Holder.GetComponent<DamageIDHolder>().transform.position = HitPos + (num * Vector3.up)+(Vector3.up*1.25f)- (HitPos - (transform.position+LocalHit));
        }
        Destroy(Holder);
        Destroy(this);
    }
    private void Update()
    {
        Holder.GetComponent<DamageIDHolder>().transform.LookAt(DamageReceivedFrom);
        Holder.GetComponent<DamageIDHolder>().transform.Rotate(Vector3.up, 180.0f);
    }
    public void RemoveThis()
    {
        StopAllCoroutines();
        //this is to remove additional scripts on multiple hits
        Destroy(this);
    }
}
