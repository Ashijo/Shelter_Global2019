using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public float WaterSPS;
    public float RandomRange;
    private float timeSinceLast;
    Vector3 pos;

    void Start()
    {
        pos = transform.position;
    }

    public void UpdateW(float dt)
    {
        timeSinceLast += dt;

        if (timeSinceLast >= WaterSPS)
        {
            pos.x += Random.value * RandomRange - RandomRange/2;
            timeSinceLast = 0;
            EnvironmentManager.Instance.GenerateWater(pos);
            pos.x = transform.position.x;
        }
    }

}
