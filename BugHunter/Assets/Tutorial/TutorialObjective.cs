using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum TutorialStep
{
    Controls, FruitBombs,RockPiles,SupplyDrops
}
public class TutorialObjective : MonoBehaviour
{
    private bool TutorialShown = false,DisabledByPlayer=false;
    public TutorialStep TutorialStep;

    // Start is called before the first frame update
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && TutorialShown == false)
        {
            TutorialSigns.instance.SetStep(TutorialStep);
            TutorialShown = true;
            DisabledByPlayer = true;
        }
    }
    
    private void OnDestroy()
    {
        DisabledByPlayer = false;
        TutorialShown = true;
    }
    private void OnDisable()
    {
        if (TutorialShown == false&& DisabledByPlayer==true)
        TutorialSigns.instance.SetStep(TutorialStep);

        TutorialShown = true;
    }
}
