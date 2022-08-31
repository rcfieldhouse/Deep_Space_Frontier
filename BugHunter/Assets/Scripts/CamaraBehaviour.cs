using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraBehaviour : MonoBehaviour
{
        // Start is called before the first frame update
        public Transform target;
        private Vector3 difference = new Vector3(0.0f, 0.0f, 0.0f);
        private void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            gameObject.transform.position = target.position - difference;
        }
    }
