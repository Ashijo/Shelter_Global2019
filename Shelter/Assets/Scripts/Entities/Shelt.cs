using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelt
{
    private GameObject shelt;
    private float lifePoint;
    private Color color;

    public bool isActive { get; private set; }

    public Shelt(Vector3 position, string name, GameObject parent)
    {
        shelt = GameObject.Instantiate(Resources.Load("Prefabs/Shelt") as GameObject);
        lifePoint = 10;
        color = Color.green;
        shelt.transform.name = name;
        shelt.transform.SetParent(parent.transform);
        shelt.SetActive(false);
    }

    public void Up(float dt)
    {
        Vector3 pos = shelt.transform.position;
        pos.x += dt ;
        shelt.transform.position = pos;
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

}
