using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager {

    // Axis
    public static float MainHorizontal()
    {
        return (Input.GetKey(KeyCode.D)) ? 1 :
               (Input.GetKey(KeyCode.A)) ? -1 :
                Input.GetAxis("Left_Joystick_Horizontal");
    }
    public static float MainVertical()
    {
        return Input.GetAxis("Left_Joystick_Vertical");
        //float r = 0.0f;
        //r += Input.GetAxis("Left_Joystick_Vertical");
        //r += Input.GetAxis("Right_Joystick_Vertical");
        //return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static Vector3 MainJoystick()
    {
        return new Vector3(MainHorizontal(), 0, MainVertical());
    }

    // Buttons
    public static bool Jump()
    {
        return Input.GetButtonDown("Jump");
    }
    public static bool Kick()
    {
        return Input.GetButtonDown("Kick");
    }
    public static bool Punch()
    {
        return Input.GetButtonDown("Punch");
    }
    public static bool SuperUppercut()
    {
        return Input.GetButtonDown("SuperUppercut");
    }
}
