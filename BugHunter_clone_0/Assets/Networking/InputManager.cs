using System;
using System.Collections;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Keys pressedKey;
    public float rotation;
    public enum Keys
    {
        None,
        MouseX,
        MouseY,
        Space,
        LeftShift,
        GetAxisRaw,
        Escape,
        W,
        A,
        S,
        D,
        Z,
        F,
        G,
        R,
        C,
        Q,
        E,
        Mouse1,
        Mouse2,
        MouseScroll,
    }
    // Use this for initialization
    void Start()
    {
        pressedKey = Keys.None;
    }

    // Update is called once per frame
    void Update()
    {
        CheckRotation();
        CheckCamera();
        CheckInput();

        rotation = GameManager.instance.UnwrapEulerAngles(transform.localEulerAngles.y);
    }
    //this is for a top-down solution will need to modify the raycast to work with a first person game.
    private void CheckCamera()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.red);

            transform.LookAt(pointToLook); 
        }
    }

    private void CheckRotation()
    {
        gameObject.transform.localEulerAngles =
        new Vector3(gameObject.transform.localEulerAngles.x,
        GameManager.instance.WrapEulerAngles(rotation),
        gameObject.transform.eulerAngles.z);

        NetworkSend.SendPlayerRotation(rotation);
    }

    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            pressedKey = Keys.Z;
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            pressedKey = Keys.None;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            pressedKey = Keys.Q;
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            pressedKey = Keys.None;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            pressedKey = Keys.E;
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            pressedKey = Keys.None;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            pressedKey = Keys.C;
        }
        else if (Input.GetKeyUp(KeyCode.C))
        {
            pressedKey = Keys.None;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            pressedKey = Keys.W;
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            pressedKey = Keys.None;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            pressedKey = Keys.A;
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            pressedKey = Keys.None;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            pressedKey = Keys.S;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            pressedKey = Keys.None;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            pressedKey = Keys.D;
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            pressedKey = Keys.None;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            pressedKey = Keys.Space;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            pressedKey = Keys.None;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            pressedKey = Keys.LeftShift;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            pressedKey = Keys.None;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pressedKey = Keys.Escape;
        }
        else if (Input.GetKeyUp(KeyCode.Escape))
        {
            pressedKey = Keys.None;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            pressedKey = Keys.LeftShift;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            pressedKey = Keys.None;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            pressedKey = Keys.R;
        }
        else if (Input.GetKeyUp(KeyCode.R))
        {
            pressedKey = Keys.None;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            pressedKey = Keys.F;
        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            pressedKey = Keys.None;
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            pressedKey = Keys.G;
        }
        else if (Input.GetKeyUp(KeyCode.G))
        {
            pressedKey = Keys.None;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            pressedKey = Keys.Mouse1;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            pressedKey = Keys.None;
        }

        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            pressedKey = Keys.Mouse2;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse2))
        {
            pressedKey = Keys.None;
        }

        NetworkSend.SendKeyInput(pressedKey);
    }
}
