using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void Start()
    {
        //Debug.Log("MasterGameManager start");
        TimerManager.Instance.InGame = true;
        SheltManager.Instance.Start();
        EnvironmentManager.Instance.Start();
    }

    public void Update(InputParams _ip, float dt)
    {
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


}
