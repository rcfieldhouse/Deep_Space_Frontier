using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class ZoomIn : MonoBehaviour
{ 
    public Animator animator;
    [SerializeField] private bool isScoped = false,IsEquipping = false;
    [SerializeField]  private VolumeProfile volumeProfile;
    public DepthOfField _DepthOfField;
    private int choice=0;
    public PlayerInput PlayerInput;
    public float SniperSensitivity;
    private bool isPrimary=true,IsRunning,ToggleRunSway=true;
    private float ScopedJourney = 0, EquippedJourney = 0, ADSTime=0,EquipTime=0,RunJourney=0,RunTime=0.33f,TransitionPeriodTime=0;
    private Vector3 ScopedPos = new Vector3(-0.3f,-0.03f,0), StationaryPos = Vector3.zero;
    private Vector3 EquipPos1 = new Vector3(0f, -0.5f, 0), EquipPos2 = new Vector3(0f, 0.1f, 0);
    private Quaternion Rot1 = Quaternion.AngleAxis(60.0f, Vector3.right), Rot2 = Quaternion.AngleAxis(-15f, Vector3.right),RunningRot = Quaternion.AngleAxis(0.0f, Vector3.right);
    Vector3 p0, p1, p2, p3;
    public WeaponSwap WeaponSwap;
    private void Awake()
    {
        Invoke(nameof(bandaidfix), 0.25f);
        WeaponSwap.BroadcastChoice += SetWeapon;
        PlayerInput.ADS += HandleAim;
        PlayerInput.Sprinting += SetSprinting;
        DepthOfField dof;
 
        if (volumeProfile.TryGet(out dof)) { _DepthOfField = dof; }
    }
    void bandaidfix()
    {
        EquippedJourney = 0.0f;
        isPrimary = GetComponentInChildren<WeaponInfo>()._IsPrimaryWeapon;
        EquipTime = GetComponentInChildren<WeaponInfo>().EquipTime;
        ADSTime = GetComponentInChildren<WeaponInfo>().ADSTime;
 
    }
    private void OnDestroy()
    {
        PlayerInput.ADS-= HandleAim;
        PlayerInput.Sprinting -= SetSprinting;
        WeaponSwap.BroadcastChoice -= SetWeapon;
        PlayerInput.ADS -= HandleAim;
    }
    public void HandleAim(bool isAiming)
    {
        if (isAiming && choice == 0 && GetComponent<WeaponSwap>().WeaponArray[0].name == "Sniper")
        {
            isScoped = !isScoped;
            animator.SetBool("isScoped", isScoped);
            _DepthOfField.active = true;
        }
        else
        {
            isScoped = !isScoped;
            animator.SetBool("isScoped", isScoped);
            _DepthOfField.active = false;
        }
        if(GetComponent<WeaponSwap>().WeaponArray[0].GetComponent<SniperRifle>() != null  && choice == 0)
            PlayerInput.SetAimWSniper(isAiming);
        
    }
    private void Update()
    {

      
        EquippedJourney += Time.deltaTime;
        if (IsEquipping&& EquippedJourney < EquipTime)
        {
            transform.localPosition = Vector3.Lerp(EquipPos1, EquipPos2, EquippedJourney / EquipTime);
           // transform.rotation = Quaternion.AngleAxis(90.0f, Vector3.right);
          transform.localRotation = Quaternion.Lerp( Rot1, Rot2, EquippedJourney / EquipTime);
   
        }

        if (EquippedJourney > EquipTime && IsEquipping)
        {
            transform.localRotation = Quaternion.Lerp(Rot2, Quaternion.AngleAxis(0.0f, Vector3.right), (EquippedJourney - EquipTime) / ((EquipTime * 1.25f) - EquipTime));
            transform.localPosition = Vector3.Lerp(EquipPos2, StationaryPos, (EquippedJourney - EquipTime) / ((EquipTime * 1.25f) - EquipTime));
     
        }
           
        if (EquippedJourney > EquipTime * 1.25f)
            IsEquipping = false;



        if (IsRunning==true&&IsEquipping==false)
        {
            if (ToggleRunSway)
                RunJourney += Time.deltaTime;
            else if (!ToggleRunSway)
                RunJourney -= Time.deltaTime;

            //not using the ! toggle cause of anim issus 
            //sometimes would infinetly trigger 
            if (RunJourney > RunTime )
                ToggleRunSway = false;    
            if(RunJourney < 0.0f)
                ToggleRunSway = true;

            // Debug.Log(RunJourney / RunTime);
            if (isPrimary)
            {
                p0 = new Vector3(0.3f, -0.1f, 0.0f);
                p1 = new Vector3(0.25f, -0.25f, 0.0f);
                p2 = new Vector3(0.2f, -0.25f, 0.0f);
                p3 = new Vector3(0.15f, -0.1f, 0.0f);
                RunningRot = Quaternion.AngleAxis(-60.0f, Vector3.up);
                
            }
            else
            {
                p0 = new Vector3(0.1f, -0.4f, 0.0f);
                p1 = new Vector3(0.05f, -0.55f, 0.0f);
                p2 = new Vector3(-0.05f, -0.55f, 0.0f);
                p3 = new Vector3(-0.1f, -0.4f, 0.0f);
                RunningRot = Quaternion.AngleAxis(-60.0f, Vector3.right);
            }

            if (TransitionPeriodTime < RunTime)
            {       
                transform.localRotation = Quaternion.Lerp(Quaternion.AngleAxis(0.0f, Vector3.up),RunningRot, TransitionPeriodTime / RunTime);
                transform.localPosition = Vector3.Lerp(new Vector3(0.0f, 0.0f, 0.0f), p0, TransitionPeriodTime / RunTime);
                TransitionPeriodTime += Time.deltaTime*2;
                return;
            }
            transform.localRotation = RunningRot;
            transform.localPosition = GetBezierPosition(RunJourney / RunTime);
            return;
        }
        else if (TransitionPeriodTime < RunTime&&IsEquipping==false)
        {
            transform.localRotation = Quaternion.Lerp(RunningRot,Quaternion.AngleAxis(0.0f, Vector3.up), TransitionPeriodTime / RunTime);
            transform.localPosition = Vector3.Lerp( p0, new Vector3(0.0f, 0.0f, 0.0f), TransitionPeriodTime / RunTime);
            TransitionPeriodTime += Time.deltaTime * 2;
            return;
        }
        else if(IsEquipping == false)
        {
            transform.localRotation = Quaternion.identity;
              transform.localPosition = Vector3.zero;
        }
        if (ADSTime == 0.0f||IsEquipping|| IsRunning) return;

       
        if (isScoped)
           ScopedJourney+=Time.deltaTime;
        else if (!isScoped)
            ScopedJourney -= Time.deltaTime;
        if (ScopedJourney > ADSTime)
            ScopedJourney = ADSTime;
        if (ScopedJourney < 0.0f)
            ScopedJourney = 0.0f;

        transform.localPosition = Vector3.Lerp(StationaryPos, ScopedPos, ScopedJourney/ADSTime);
    }


    Vector3 GetBezierPosition(float t)
    {
        return Mathf.Pow(1f - t, 3f) * p0 + 3f * Mathf.Pow(1f - t, 2f) * t * p1 + 3f * (1f - t) * Mathf.Pow(t, 2f) * p2 + Mathf.Pow(t, 3f) * p3;
    }
    private void SetSprinting (bool var)
    {
        IsRunning = var;
        TransitionPeriodTime = 0.0f;
    }
    private void SetWeapon(int foo)
    {
      
        EquippedJourney = 0.0f;
        IsEquipping = true;
        isPrimary= GetComponentInChildren<WeaponInfo>()._IsPrimaryWeapon;
        EquipTime = GetComponentInChildren<WeaponInfo>().EquipTime;
        ADSTime = GetComponentInChildren<WeaponInfo>().ADSTime;
        //foo is the index in the array DANTE
        choice = foo;
    }
}
