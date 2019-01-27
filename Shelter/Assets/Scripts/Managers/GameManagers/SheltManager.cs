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
    public List<Shelt> shelts { get; private set; }
    private GameObject spawn;
    private GameObject MotherShelt;
    private Shelt selectedOne;
    private Vector3 ShadowPosition;
    private GameObject Shadow;
    private float shadowSwitch;


    public int SaveShelt;
    public int DeadShelt;

    private int shadowCurs;

    public void Start()
    {
        spawned = 0;
        shelts = new List<Shelt>();
        spawn = GameObject.Find("Spawn");
        MotherShelt = new GameObject();
        MotherShelt.transform.name = "MotherShelt";
        spawnCall = new Timer.toCall(SpawnWork);
        sheltSpwanerTimer = new Timer(GV.Instance.timeBetwenSheltSpawn, spawnCall, true);
        Shadow = GameObject.Instantiate(Resources.Load("Prefabs/Shadow") as GameObject);
        Shadow.SetActive(false);

        for (int i = 0; i < GV.Instance.sheltToSpawn; i++)
        {
            Shelt newShelt = new Shelt(spawn.transform.position, $"Shelt-{(1+i).ToString()}", MotherShelt);
            shelts.Add(newShelt);
        }

        TimerManager.Instance.AddTimer(this, sheltSpwanerTimer, TimerManager.Timebook.InGame);
        SpawnWork();
    }

    #region SelectManagement
    private void TryToSelect(Vector3 mousePos)
    {

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null)
        {
            if(hit.collider.gameObject.transform.CompareTag("Shelt"))
                selectedOne = hit.collider.gameObject.GetComponent<SheltScript>().myShelt;
            if (selectedOne != null)
            {
                Shadow.SetActive(true);
            }
        }
        
    }

    private void Deselect()
    {
        ShadowPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ShadowPosition.z = 0;
       
        selectedOne.RushTo(ShadowPosition);
        Shadow.SetActive(false);
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
            }
        }

        if (Shadow.activeInHierarchy)
        {
            ShadowPosition = Camera.main.ScreenToWorldPoint(_ip.MousePos);
            ShadowPosition.z = 0;
            Shadow.transform.position = ShadowPosition;

            shadowSwitch += dt;

            if (shadowSwitch > GV.Instance.timeBetwenSprt)
            {
                shadowSwitch = 0;
                shadowCurs = shadowCurs % GV.ws.shadow.Length;
                Shadow.GetComponent<SpriteRenderer>().sprite = GV.ws.shadow[shadowCurs];
                shadowCurs++;
            }
        }

        if (_ip.ClickLeftDown)
        {
            //Debug.Log("left click");
            TryToSelect(_ip.MousePos);
        }

        if (_ip.ClickLeftUp && selectedOne != null)
        {
            Deselect();
        }
    }

    public void FixedUpdate(float fdt)
    {
    }

    public void SaveMe(Shelt toSave)
    {
        SaveShelt++;
        toSave.DeActivate();
        CheckGameState();
    }

    public void KillMe(Shelt toKill)
    {
        DeadShelt++;
        toKill.DeActivate();
        CheckGameState();
    }

    private void CheckGameState()
    {

        if (SaveShelt + DeadShelt == GV.Instance.sheltToSpawn)
        {
            MasterGameManager.Instance.EndGame(SaveShelt >= GV.Instance.SheltToSave);
        }
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
