using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterGameManager
{
    #region Singleton
    private static MasterGameManager instance;


    private MasterGameManager()
    {

    }

    public static MasterGameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new MasterGameManager();
            }
            return instance;
        }
    }

    #endregion
    // Start is called before the first frame update

    public bool TimeStop { get; private set; }
    private bool endGame = false;

    private GameObject endImg;
    private Sprite[] endSprites;
    private bool switcher;
    private float timeSinceLastSwitch;

    private Vector3 camPos;

    public void Start()
    {
        //Debug.Log("MasterGameManager start");
        endImg = GameObject.Find("EndImg");
        endImg.SetActive(false);
        SoundManager.Instance.PlayMusic();
        TimerManager.Instance.InGame = true;
        SheltManager.Instance.Start();
        SheltManager.Instance.Reset();

        EnvironmentManager.Instance.Start();
    }

    public void Finish()
    {
        //endImg.SetActive(true);
    }

    public void Update(InputParams _ip, float dt)
    {
        
        if (_ip.EscapePressed)
        {
            endGame = false;
            endImg.SetActive(true);

            SceneManager.InitToCall init = SwitchToMenu;
            SceneManager.Instance.LoadScene(GV.SCENENAMES.MainMenu.ToString(), init);
        }

        if (endGame)
        {
            SoundManager.Instance.StopMusic();
            EnvironmentManager.Instance.Finish();
            Time.timeScale = 1;
            endImg.SetActive(true);
            timeSinceLastSwitch += dt;

            if (timeSinceLastSwitch > 1)
            {
                timeSinceLastSwitch = 0;
                endImg.GetComponent<Image>().sprite = endSprites[switcher ? 1 : 0];
                switcher = !switcher;
            }

            return;
        }

        if (_ip.APress || _ip.DPress)
        {
            if (_ip.APress && Camera.main.transform.position.x > 0)
            {
                camPos = Camera.main.transform.position;
                camPos.x -= GV.Instance.CamSpeed;
                Camera.main.transform.position = camPos;
            }

            if (_ip.DPress && Camera.main.transform.position.x < GV.Instance.MaxXposCam)
            {
                camPos = Camera.main.transform.position;
                camPos.x += GV.Instance.CamSpeed;
                Camera.main.transform.position = camPos;
            }
        }

        SheltManager.Instance.Update(_ip, dt);
        EnvironmentManager.Instance.Update(_ip, dt);


        TimeStop = _ip.SpacePress;
        TimerManager.Instance.InGame = !TimeStop;
        Time.timeScale = TimeStop ? 0 : 1;

    }

    public void FixedUpdate(float fdt)
    {
        SheltManager.Instance.FixedUpdate(fdt);
        EnvironmentManager.Instance.FixedUpdate(fdt);
    }

    public void EndGame(bool isWin)
    {
        TimerManager.Instance.InGame = false;
        endGame = true;

        endSprites = isWin ? GV.ws.winScreen : GV.ws.looseScreen;
        endImg.SetActive(true);
        endImg.GetComponent<Image>().sprite = endSprites[0];
    }

    private void SwitchToMenu()
    {
        TimerManager.Instance.Init();
        FlowManager.Instance.ChangeFlows(GV.SCENENAMES.MainMenu);
    }

}
