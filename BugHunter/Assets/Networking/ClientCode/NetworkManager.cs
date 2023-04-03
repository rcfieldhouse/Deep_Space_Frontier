namespace Client
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using KaymakNetwork.Network;
    
    public class NetworkManager : MonoBehaviour
    {
        public string ip;
        // Start is called before the first frame update
    
        void Start()
        {
            DontDestroyOnLoad(this);
    
            NetworkConfig.InitNetwork();
            NetworkConfig.ConnectToServer(ip);
        }
    
        private void OnApplicationQuit()
        {
            Debug.Log("Quit");
            NetworkConfig.DisconectFromServer();
        }
    }

}