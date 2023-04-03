namespace Client { 

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using UnityEngine.InputSystem.Controls;
    
    public class InputManager : MonoBehaviour
    {
        InputAction LookAction;
    
        public Keys pressedKey;
        public enum Keys
        {
            None,
            W,
            A,
            S,
            D,
            Mouse1,
            Mouse2,
            MouseScroll,
        }
    
        private void Awake()
        {
            LookAction = GetComponent<PlayerInput>().actions["Look"];
        }
    
        // Use this for initialization
        void Start()
        {
            pressedKey = Keys.None;
            Cursor.lockState = CursorLockMode.Locked;
        }
    
        public void OnLook(InputAction.CallbackContext ctxt)
        {
            Debug.Log(ctxt.ReadValue<Vector2>());
            //if (ctxt.performed)
                NetworkSend.SendLookData(ctxt.ReadValue<Vector2>());
                
            //if (ctxt.canceled)
                //NetworkSend.SendLookData(Vector2.zero);
        }
    
        public void OnMove(InputAction.CallbackContext ctxt)
        {
            if(ctxt.performed)
                NetworkSend.SendMoveData(ctxt.ReadValue<Vector2>());
            if (ctxt.canceled)
                NetworkSend.SendMoveData(Vector2.zero);
        }
    
        public void MovementPrediction()
        {
            //Synchronise client clocks with the Server Clocks
    
            //OnTick for pre-Physics calcs
            //    movement command from client (this is to improve preserved latency i.e. movement prediction)
            //    client does a tentative movement
    
            //PostOnTick post physics
            //    player moves on serverside
            //    Server corrects position if necessary
            //    client updates to corrected position
            //    Timestamps (found in data packet header) determine where client should be ona  particular frame
            //    Velocity is used to bridge the gap between server packet send rate
    
        }
        public struct PlayerState
        {
            //Animation States
            //Death States
            //Position of Line Renderer for weapons
            //Look Direction
    
        }
    
        public struct NetworkLobby
        {
            // Connection State for players
    
            // player disconnect (connection)
            // player reconnect  (connectionState)
            // player connection speed 
            // ping rates
        }
    
        public struct LevelState
        {
            // Enemy Position (enum)
            // Enemy Death States (enum)
            // Enemy Patrol States (enum)
            // Projectile Spawned (position)
            // Barriers Destroyed (bool[])
            // Boss Killed (bool)
            // Player Win/loss (bool)
            // Cutscene Finish (bool)
        }
    }

}