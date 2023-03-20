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
    private float ScopedJourney = 0, EquippedJourney = 0, ADSTime=0,EquipTime=0;
    private Vector3 ScopedPos = new Vector3(-0.3f,-0.03f,0), StationaryPos = Vector3.zero;
    private Vector3 EquipPos1 = new Vector3(0f, -0.5f, 0), EquipPos2 = new Vector3(0f, 0.05f, 0);
    private Quaternion Rot1 = Quaternion.AngleAxis(60.0f, Vector3.right), Rot2 = Quaternion.AngleAxis(-10f, Vector3.right);
    private void Awake()
    {
        Invoke(nameof(bandaidfix), 0.25f);
        WeaponSwap.BroadcastChoice += SetWeapon;
        PlayerInput.ADS += HandleAim;
        DepthOfField dof;
        if (volumeProfile.TryGet(out dof)) { _DepthOfField = dof; }
    }
    void bandaidfix()
    {
        EquipTime = GetComponentInChildren<WeaponInfo>().EquipTime;
        ADSTime = GetComponentInChildren<WeaponInfo>().ADSTime;
    }
    private void OnDestroy()
    {
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

        if (ADSTime == 0.0f||IsEquipping) return;
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
    private void SetWeapon(int foo)
    {
        EquippedJourney = 0.0f;
        IsEquipping = true;
        EquipTime = GetComponentInChildren<WeaponInfo>().EquipTime;
        ADSTime = GetComponentInChildren<WeaponInfo>().ADSTime;
        //foo is the index in the array DANTE
        choice = foo;
    }
}
