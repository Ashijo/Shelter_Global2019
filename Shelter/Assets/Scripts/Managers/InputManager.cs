using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager {

    #region Singleton
    private static InputManager instance;


    private InputManager() {

    }

    public static InputManager Instance {
        get {
            if (instance == null) {
                instance = new InputManager();
            }
            return instance;
        }
    }

    #endregion

    // Update is called once per frame
    public InputParams Update()
    {
        Vector3 mousePos = Input.mousePosition;
        bool clickLeftDown = Input.GetMouseButtonDown(0);
        bool clickLeft = Input.GetMouseButton(0);
        bool clickLeftUp = Input.GetMouseButtonUp(0);
        bool spacePress = Input.GetKey(KeyCode.Space);

        return new InputParams(mousePos, clickLeftDown, clickLeft, clickLeftUp, spacePress);
	}

}


public class InputParams{
    public bool Consumed { get; private set; }

    public Vector3 MousePos   { get; private set; }
    public bool ClickLeftDown { get; private set; }
    public bool ClickLeft     { get; private set; }
    public bool ClickLeftUp   { get; private set; }
    public bool SpacePress    { get; private set; }


    public InputParams(Vector3 MousePos, bool ClickLeftDown, bool ClickLeft, bool ClickLeftUp, bool SpacePress)
    {
        Consumed = false;

        this.MousePos = MousePos;
        this.ClickLeftDown = ClickLeftDown;
        this.ClickLeft = ClickLeft;
        this.ClickLeftUp = ClickLeftUp;
        this.SpacePress = SpacePress;
    }

    public void Use() {
        Consumed = true;
    }
}
