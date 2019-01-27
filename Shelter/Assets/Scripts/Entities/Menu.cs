using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public void Quit()
    {
        Debug.Log ("has quit");
        Application.Quit();
    }

    public void GoToGame() {
        SceneManager.InitToCall initFlow = SwitchToGame;
        SceneManager.Instance.LoadScene(GV.SCENENAMES.GameScene.ToString(), initFlow);
    }

    private void SwitchToGame()
    {
        FlowManager.Instance.ChangeFlows(GV.SCENENAMES.GameScene);
    }
    
}
