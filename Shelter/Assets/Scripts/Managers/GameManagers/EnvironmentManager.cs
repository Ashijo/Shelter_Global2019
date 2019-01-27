using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnvironmentManager 
{

    #region Singleton
    private static EnvironmentManager instance;


    private EnvironmentManager()
    {

    }

    public static EnvironmentManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new EnvironmentManager();
            }
            return instance;
        }
    }

    #endregion

    private List<GameObject> waterSpawners;
    private List<GameObject> waterRegister;
    private List<ShelterScript> shelters;
    private GameObject WaterMother;

    private List<Timer> waterDestroyer;
    private Timer.toCall wrapper;

    public void Start()
    {
        wrapper = DeRegister;
        waterDestroyer = new List<Timer>();
        waterRegister = new List<GameObject>();
        waterSpawners = new List<GameObject>();

        foreach (GameObject spawner in GameObject.FindGameObjectsWithTag("WaterSpawner"))
        {
            waterSpawners.Add(spawner);
        }
        
    }

    public void DeRegister()
    {
        waterRegister[0].SetActive(false);
        GameObject.Destroy(waterRegister[0]);
        waterRegister.RemoveAt(0);
        waterDestroyer.RemoveAt(0);
    }

    public void GenerateWater(Vector3 pos)
    {

        GameObject newWater = GameObject.Instantiate(Resources.Load("Prefabs/Water") as GameObject);
        newWater.transform.position = pos;
        waterRegister.Add(newWater);

        waterDestroyer.Add(new Timer(10f, wrapper));
        TimerManager.Instance.AddTimer(this, waterDestroyer[waterDestroyer.Count -1], TimerManager.Timebook.InGame);

        if (WaterMother == null)
        {
            WaterMother = new GameObject();
            WaterMother.transform.name = "WaterMother";
        }

        newWater.transform.SetParent(WaterMother.transform);
    }

    public void RegisterShelter(ShelterScript toRegister)
    {
        if (shelters == null)
        {
            shelters = new List<ShelterScript>();
        }
    }

    // Update is called once per frame
    public void Update(InputParams ip, float dt)
    {
        foreach (GameObject spawner in waterSpawners)
        {
            spawner.GetComponent<SpawnerScript>().UpdateW(dt);
        }

        for(int i = waterRegister.Count - 1; i >= 0; i--)
        {
            foreach (Shelt shelt in SheltManager.Instance.shelts)
            {
                try
                {
                    if (shelt.isActive && waterRegister[i] != null && waterRegister[i].activeInHierarchy)
                        shelt.looseHP = CheckCollision(shelt, waterRegister[i]);
                }
                catch (Exception e)
                {
                    Debug.Log(e.ToString());
                }
            }
            if (waterRegister[i] == null || waterRegister[i].transform.position.y < -20f)
            {
                if(waterRegister[i]!= null)
                    GameObject.Destroy(waterRegister[i]);

                waterRegister.Remove(waterRegister[i]);
                waterDestroyer[0].Kill();
                waterDestroyer.RemoveAt(0);

            }
        }
    }

    public void FixedUpdate(float fdt)
    {
    }

    public void Finish()
    {
        foreach (ShelterScript shelter in shelters)
        {
            shelter.ResetImg();
        }

    }

    private bool CheckCollision(Shelt shelt, GameObject water)
    {
        return shelt.shelt.transform.position.x - .5 < water.transform.position.x + .1 &&
               shelt.shelt.transform.position.x + .5 > water.transform.position.x &&
               shelt.shelt.transform.position.y - .35 < water.transform.position.y + 1 &&
               .35 + shelt.shelt.transform.position.y > water.transform.position.y;

    }

}
