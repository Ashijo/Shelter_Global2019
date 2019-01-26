using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : Flow
{
    public override void Finish()
    {
    }

    public override void FixedUpdateFlow(float _fdt)
    {
        MasterGameManager.Instance.FixedUpdate(_fdt);
    }

    public override void InitializeFlow()
    {
        MasterGameManager.Instance.Start();
    }

    public override void UpdateFlow(float _dt, InputParams _ip)
    {
        if (_ip.EscapePressed)
        {
            SceneManager.InitToCall init = SwitchToMenu;
            SceneManager.Instance.LoadScene(GV.SCENENAMES.MainMenu.ToString(), init);
        }

        MasterGameManager.Instance.Update(_ip, _dt);
    }

    private void SwitchToMenu()
    {
        TimerManager.Instance.Init();
        FlowManager.Instance.ChangeFlows(GV.SCENENAMES.MainMenu);
    }
}
