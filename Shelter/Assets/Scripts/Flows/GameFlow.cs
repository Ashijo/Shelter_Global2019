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
        Debug.Log("GameFlow start");
        MasterGameManager.Instance.Start();
    }

    public override void UpdateFlow(float _dt, InputParams _ip)
    {
        MasterGameManager.Instance.Update(_ip, _dt);
    }
}
