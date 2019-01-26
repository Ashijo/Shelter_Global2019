using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelt
{
    private GameObject shelt;
    private float lifePoint;
    private Color color;
    private bool direction = true;
    private bool rush = false;
    private Vector3 RushPos;
    private Vector3 BeginRushPos;

    public bool isActive { get; private set; }

    public Shelt(Vector3 position, string name, GameObject parent)
    {
        shelt = GameObject.Instantiate(Resources.Load("Prefabs/Shelt") as GameObject);
        lifePoint = 10;
        color = Color.green;
        shelt.transform.name = name;
        shelt.transform.SetParent(parent.transform);
        shelt.SetActive(false);
        shelt.AddComponent<SheltScript>();
        shelt.GetComponent<SheltScript>().Init(this);
    }

    public void Up(float dt)
    {
        Vector3 pos = shelt.transform.position;
        pos.x += dt * (rush ? GV.Instance.SheltMaxSpeed : GV.Instance.SheltNormalSpeed ) * (direction ? 1f : -1f);
        shelt.transform.position = pos;

        if (rush)
        {
            if (shelt.transform.position.x.Near(RushPos.x))
            {
                rush = false;
            }

            if (shelt.transform.position.x.Near(BeginRushPos.x, dt/10f))
            {
                rush = false;
            }
        }


    }

    public void RushTo(Vector3 position)
    {
        BeginRushPos = shelt.transform.position;
        RushPos = position;
        direction = position.x > shelt.transform.position.x;
        rush = true;
    }

    public void Activate()
    {
        isActive = true;
        shelt.SetActive(true);
    }

    public void DeActivate()
    {
        isActive = false;
        shelt.SetActive(false);
    }

    public void Turn()
    {
        direction = !direction;
    }

    public override string ToString()
    {
        return shelt.transform.position.ToString();
    }
}
