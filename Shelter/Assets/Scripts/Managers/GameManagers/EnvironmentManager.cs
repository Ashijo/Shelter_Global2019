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

    public void Start()
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
