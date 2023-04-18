using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneBoomBoom : MonoBehaviour
{
    [Range(0, 30)] public float  AOEBombSpread;
    public GameObject AOE_Orb;
    public Transform VollyLaunchPoint;
    public List<Vector3> AOEBombingLocations;
    [Range(0, 50)] public float LaunchHeight;
    private void Awake()
    {
        AOEBombSpread *= 5;
        Vector3 vector2 = new Vector3(AOEBombSpread, 0.0f, 0.0f);
        Vector3 vector3 = new Vector3(-AOEBombSpread, 0.0f, 0.0f);
        Vector3 vector4 = new Vector3(0.0f, 0.0f, AOEBombSpread);
        Vector3 vector5 = new Vector3(0.0f, 0.0f, -AOEBombSpread);
        Vector3 vector6 = new Vector3(AOEBombSpread / 2, 0.0f, AOEBombSpread / 2);
        Vector3 vector7 = new Vector3(AOEBombSpread / 2, 0.0f, -AOEBombSpread / 2);
        Vector3 vector8 = new Vector3(-AOEBombSpread / 2, 0.0f, AOEBombSpread / 2);
        Vector3 vector9 = new Vector3(-AOEBombSpread / 2, 0.0f, -AOEBombSpread / 2);

        Vector3 vector10 = new Vector3(AOEBombSpread * 1.25f, 0.0f, AOEBombSpread / 2);
        Vector3 vector11 = new Vector3(-AOEBombSpread * 1.25f, 0.0f, AOEBombSpread / 2);
        Vector3 vector12 = new Vector3(-AOEBombSpread / 2, 0.0f, AOEBombSpread * 1.25f);
        Vector3 vector13 = new Vector3(-AOEBombSpread / 2, 0.0f, -AOEBombSpread * 1.25f);

        Vector3 vector14 = new Vector3(AOEBombSpread * 1.25f, 0.0f, -AOEBombSpread / 2);
        Vector3 vector15 = new Vector3(-AOEBombSpread * 1.25f, 0.0f, -AOEBombSpread / 2);
        Vector3 vector16 = new Vector3(AOEBombSpread / 2, 0.0f, AOEBombSpread * 1.25f);
        Vector3 vector17 = new Vector3(AOEBombSpread / 2, 0.0f, -AOEBombSpread * 1.25f);

        AOEBombingLocations.Add(vector2);
        AOEBombingLocations.Add(vector3);
        AOEBombingLocations.Add(vector4);
        AOEBombingLocations.Add(vector5);
        AOEBombingLocations.Add(vector6);
        AOEBombingLocations.Add(vector7);
        AOEBombingLocations.Add(vector8);
        AOEBombingLocations.Add(vector9);
        AOEBombingLocations.Add(vector10);
        AOEBombingLocations.Add(vector11);
        AOEBombingLocations.Add(vector12);
        AOEBombingLocations.Add(vector13);
        AOEBombingLocations.Add(vector14);
        AOEBombingLocations.Add(vector15);
        AOEBombingLocations.Add(vector16);
        AOEBombingLocations.Add(vector17);
    }
    public void DoTheBoom()
    {
        Vector3 P1 = VollyLaunchPoint.position + Vector3.up * 10;
        AOEBombAttack(P1);
    }
    void AOEBombAttack(Vector3 P1)
    {
        for (int j = 0; j < AOEBombingLocations.Count; j++)
        {
            Vector3 P2 = VollyLaunchPoint.position + transform.rotation * AOEBombingLocations[j];
            GameObject obj = Instantiate(AOE_Orb, VollyLaunchPoint.position + Vector3.up * 10, Quaternion.identity);
            obj.gameObject.AddComponent<CurveyTrajectory>().SetValues(P1, P2, LaunchHeight, j);
        }
    }
}
