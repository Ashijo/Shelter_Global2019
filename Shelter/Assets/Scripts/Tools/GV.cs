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
   
    public readonly int timeBetwenSheltSpawn = 5;
    public readonly int sheltToSpawn = 20;
    public readonly float SheltNormalSpeed = 1f;
    public readonly float SheltMaxSpeed = 2f;

    public readonly float timeBetwenSprt = (1f / 14f);
    public readonly float timeBetwenRushSprt = (1f / 26f);

    public readonly float deathSpeed = 7.5f;
    public readonly int SheltToSave = 10;

}