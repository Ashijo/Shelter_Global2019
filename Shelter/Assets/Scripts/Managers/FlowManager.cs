﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlowManager {

    #region Singleton
    private static FlowManager instance;

    private FlowManager() { }

    public static FlowManager Instance {
        get {
            if (instance == null)
                instance = new FlowManager();

            return instance;
        }
    }
    #endregion

    public GV.SCENENAMES currentScene;
    Flow currentFlow;
    bool flowInitialized = false;

    public void InitializeFlowManager(GV.SCENENAMES _scene) {
        TimerManager.Instance.Init();

        currentScene = _scene;
        currentFlow = CreateFlow(_scene);
    }

    public void Update(float _dt) {
        TimerManager.Instance.Update(_dt);

        if (currentFlow != null && flowInitialized)
            currentFlow.UpdateFlow(_dt, InputManager.Instance.Update());
    }

    public void FixedUpdate(float _dt) {
        if (currentFlow != null && flowInitialized)
            currentFlow.FixedUpdateFlow(_dt);
    }

    public void ChangeFlows(GV.SCENENAMES _flowToLoad) {
        flowInitialized = false;
        currentFlow.Finish();
        currentFlow = CreateFlow(_flowToLoad);
    }

    private Flow CreateFlow(GV.SCENENAMES _flow) {
        Flow flow;

        switch (_flow) {
            case GV.SCENENAMES.StartScene:
                flow = new StartSceneFlow();
                break;
            case GV.SCENENAMES.MainMenu:
                flow = new MainMenuFlow();
                break;
            case GV.SCENENAMES.GameScene:
                flow = new GameFlow();
                break;
            default:
                flow = null;
                break;
        }

        if(flow != null)
            SceneManager.Instance.LoadScene(_flow.ToString(), SceneLoaded);

        return flow;
    }

    public void SceneLoaded() {
        currentFlow.InitializeFlow();
        flowInitialized = true;
    }
}
