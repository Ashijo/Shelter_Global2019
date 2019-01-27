using System.Collections;
using System.Collections.Generic;
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
    public void Start()
    {
        waterSpawners = new List<GameObject>();

        foreach (GameObject spawner in GameObject.FindGameObjectsWithTag("WaterSpawner"))
        {
            waterSpawners.Add(spawner);
        }
        
    }

    public void GenerateWater(Vector3 pos)
    {
    }

    // Update is called once per frame
    public void Update(InputParams ip, float dt)
    {
        
    }

    public void FixedUpdate(float fdt)
    {
    }



}
