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


    private bool directionInUse = false;
    private bool selectorsInUse = false;


    // Update is called once per frame
    public InputParams Update() {
       

        return new InputParams();
	}

}


public class InputParams{
    public bool Consumed { get; private set; }

    public InputParams() {
       

    }

    public void Use() {
        Consumed = true;
    }
}
