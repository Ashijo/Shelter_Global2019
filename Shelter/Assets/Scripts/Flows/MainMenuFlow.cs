using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuFlow : Flow {


    public override void Finish() {
    }

    public override void FixedUpdateFlow(float _fdt) {
    }

    public override void InitializeFlow() {
    }

    public override void UpdateFlow(float _dt, InputParams _ip) {

        if (_ip.SpacePress)
        {
            SceneManager.InitToCall initFlow = SwitchToGame;
            SceneManager.Instance.LoadScene(GV.SCENENAMES.GameScene.ToString(), initFlow);
        }

        if (_ip.EscapePressed)
        {
            Application.Quit();
        }
    }

    private void SwitchToGame()
    {
        FlowManager.Instance.ChangeFlows(GV.SCENENAMES.GameScene);
    }
}
