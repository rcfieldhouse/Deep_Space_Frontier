using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Unity.Netcode;

public class MiddleCutsceneManager : NetworkBehaviour
{
    public GameObject FirstCutsceneParent;
    public GameObject SecondCutsceneParent;

    public GameObject BrokenTreeBarrier;
    public GameObject PlaceholderTree;
    public GameObject GameplayQueen;

    public GameObject PlayerCutscenePos;

    public PlayableDirector director;
    public GameObject CutsceneAnimTree;
    private bool PlayerInCutscene = false;
    private bool MCutsceneEnded = false;

    //public GameObject Player1;
    //public GameObject Player2;
    //public GameObject Player3;

    // Start is called before the first frame update
    void Start()
    {
        MCutsceneEnded = false;
        GameplayQueen.SetActive(false);
    }

    private void Update()
    {
        if (director.state != PlayState.Playing && PlayerInCutscene == true)
        {
            if(MCutsceneEnded == false)
            {
                CutsceneEnded();
                MCutsceneEnded = true;
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Need This logic when a player enters the cutscene trigger
        if (other.gameObject.tag == "Player")
        {
            //GameObject[] AllPlayers = GameObject.FindGameObjectsWithTag("Player");

            FirstCutsceneParent.SetActive(true);
            PlayerInCutscene = true;
            PlaceholderTree.SetActive(false);
            // i'm teleporting the player's position to inside the cutscene area so they don't get softlocked outside the cutscene area
            // and so they don't take damage from enemies chasing them before triggering the cutscene
            // currently just teleports the object with player tag that enters the trigger

           //for (int i = 0; i < AllPlayers.Length; i++)
           //{
           //    AllPlayers[i].transform.position = PlayerCutscenePos.transform.position;
           //}
            other.gameObject.transform.position = PlayerCutscenePos.transform.position;

        }

    }
    private void OnTriggerExit(Collider other)
    {

    }

    private void CutsceneEnded()
    {
        BrokenTreeBarrier.SetActive(true);
        CutsceneAnimTree.SetActive(false);

        FirstCutsceneParent.SetActive(false);

        GameplayQueen.SetActive(true);
    }

   //[ServerRpc(RequireOwnership = false)]
   //public void DanteServerRPC(ulong id)
   //{
   //        GameObject PlayerObject = NetworkManager.Singleton.ConnectedClients[(int)id].PlayerObject;
   //
   //        FirstCutsceneParent.SetActive(true);
   //        PlayerInCutscene = true;
   //        PlaceholderTree.SetActive(false);
   //        PlayerObject.gameObject.transform.position = PlayerCutscenePos.transform.position;
   //}
}
