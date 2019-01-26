using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SheltManager
{
    #region Singleton
    private static SheltManager instance;


    private SheltManager()
    {

    }

    public static SheltManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SheltManager();
            }
            return instance;
        }
    }

    #endregion

    private Timer sheltSpwanerTimer;
    private Timer.toCall spawnCall;
    private int spawned;
    private List<Shelt> shelts;
    private GameObject spawn;
    private GameObject MotherShelt;
    private Shelt selectedOne = null;
    private Vector3 ShadowPosition;

    public void Start()
    {
        //Debug.Log("SheltManager start");

        shelts = new List<Shelt>();
        spawn = GameObject.Find("Spawn");
        MotherShelt = new GameObject();
        MotherShelt.transform.name = "MotherShelt";
        spawnCall = new Timer.toCall(SpawnWork);
        sheltSpwanerTimer = new Timer(GV.Instance.timeBetwenSheltSpawn, spawnCall, true);

        for (int i = 0; i < GV.Instance.sheltToSpawn; i++)
        {
            Shelt newShelt = new Shelt(spawn.transform.position, $"Shelt-{(1+i).ToString()}", MotherShelt);
            shelts.Add(newShelt);
        }

        TimerManager.Instance.AddTimer(this, sheltSpwanerTimer, TimerManager.Timebook.InGame);
    }

    #region SelectManagement
    private void TryToSelect(Vector3 mousePos)
    {

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null)
        {
            if(hit.collider.gameObject.transform.CompareTag("Shelt"))
                selectedOne = hit.collider.gameObject.GetComponent<SheltScript>().myShelt;
        }
        
    }

    private void Deselect()
    {
        ShadowPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ShadowPosition.z = 0;
        selectedOne = null;
    }
    #endregion


    public void Update(InputParams _ip, float dt)
    {

        if (!MasterGameManager.Instance.TimeStop)
        {
            foreach (Shelt shelt in shelts)
            {
                if (shelt.isActive)
                    shelt.Up(dt);
                else
                    break;
            }
        }

        if (_ip.ClickLeftDown)
        {
            //Debug.Log("left click");
            TryToSelect(_ip.MousePos);
        }

        if (_ip.ClickLeftUp)
        {
            Deselect();
        }
    }

    public void FixedUpdate(float fdt)
    {
    }

    #region SpwanFunctions
    private void SpawnWork()
    {
        //Debug.Log("Spawnfunc called");

        shelts[spawned].Activate();
        spawned++;

        if (spawned >= GV.Instance.sheltToSpawn)
        {
            StopSpawning();
        }
    }

    private void StopSpawning()
    {
        sheltSpwanerTimer.OnPause = true;
        sheltSpwanerTimer.Kill();
    }
    #endregion

}
