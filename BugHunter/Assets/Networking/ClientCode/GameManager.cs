namespace Client { 
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;


    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        public static Dictionary<int, GameObject> playerList = new Dictionary<int, GameObject>();
        public static Queue<int> idToSet = new Queue<int>();
        public GameObject prefab;

        private void Awake()
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        private void Update()
        {

        }

    }
}
