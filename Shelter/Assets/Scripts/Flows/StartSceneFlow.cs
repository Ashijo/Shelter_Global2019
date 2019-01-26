using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneFlow : Flow {


    public override void Finish() {
    }

    public override void FixedUpdateFlow(float _fdt) {
    }

    public override void InitializeFlow() {
        FlowManager.Instance.ChangeFlows(GV.SCENENAMES.MainMenu);
    }

    public override void UpdateFlow(float _dt, InputParams _ip) {
    }

}
