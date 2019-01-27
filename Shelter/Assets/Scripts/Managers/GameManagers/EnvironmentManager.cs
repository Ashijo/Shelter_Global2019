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
    private GameObject WaterMother;
    public void Start()
    {
        waterRegister = new List<GameObject>();
        waterSpawners = new List<GameObject>();

        foreach (GameObject spawner in GameObject.FindGameObjectsWithTag("WaterSpawner"))
        {
            waterSpawners.Add(spawner);
        }
        
    }

    public void GenerateWater(Vector3 pos)
    {

        GameObject newWater = GameObject.Instantiate(Resources.Load("Prefabs/Water") as GameObject);
        newWater.transform.position = pos;
        waterRegister.Add(newWater);

        if (WaterMother == null)
        {
            WaterMother = new GameObject();
            WaterMother.transform.name = "WaterMother";
        }

        newWater.transform.SetParent(WaterMother.transform);
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
                    if (shelt.isActive && waterRegister[i] != null &&waterRegister[i].activeInHierarchy)
                        shelt.looseHP = CheckCollision(shelt, waterRegister[i]);
                }
                catch (Exception e)
                {
                    Debug.Log(e.ToString());
                }
            }
            if (waterRegister[i] == null || waterRegister[i].transform.position.y < -6f)
            {
                if(waterRegister[i]!= null)
                    GameObject.Destroy(waterRegister[i]);

                waterRegister.Remove(waterRegister[i]);

            }
        }
    }

    public void FixedUpdate(float fdt)
    {
    }

    private bool CheckCollision(Shelt shelt, GameObject water)
    {
        return shelt.shelt.transform.position.x < water.transform.position.x + .05 &&
               shelt.shelt.transform.position.x + .35 > water.transform.position.x &&
               shelt.shelt.transform.position.y < water.transform.position.y + .3 &&
               .35 + shelt.shelt.transform.position.y > water.transform.position.y;

    }

}
