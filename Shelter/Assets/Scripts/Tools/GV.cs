using System;
using System.Collections.Generic;
using UnityEngine;

public class GV {

    #region Singleton
    private static GV instance;


    private GV() {

    }

    public static GV Instance {
        get {
            if (instance == null) {
                instance = new GV();
            }
            return instance;
        }
    }

    #endregion

    public static WS ws;

    // GLOBAL VARIABLES
    public enum SCENENAMES { DUMMY, MainMenu, StartScene, GameScene}
   
    public readonly int timeBetwenSheltSpawn = 3;
    public readonly int sheltToSpawn = 12;
    public readonly float SheltNormalSpeed = 1f;
    public readonly float SheltMaxSpeed = 2f;

    public readonly float timeBetwenSprt = (1f / 14f);
    public readonly float timeBetwenRushSprt = (1f / 26f);

    public readonly float deathSpeed = 8.5f;
    public readonly int SheltToSave = 8;

    public readonly float CamSpeed = 8f;
    public readonly float MaxXposCam = 20f;

    public readonly float jumpForce = 4;

}