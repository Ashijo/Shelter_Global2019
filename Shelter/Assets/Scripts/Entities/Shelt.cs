using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelt
{
    private GameObject shelt;
    private float lifePoint;
    private Color color;
    private bool direction = true;
    public bool rush = false;
    private Vector3 RushPos;
    private Vector3 BeginRushPos;

    public bool onFallEnter;
    public bool fall;
    public bool onFallOut = false;
    public bool onFallOutFirst = false;

    private int spriteCurs;
    private float timeSinceLastSprt;

    public bool isActive { get; private set; }

    public Shelt(Vector3 position, string name, GameObject parent)
    {
        shelt = GameObject.Instantiate(Resources.Load("Prefabs/Shelt") as GameObject);
        lifePoint = 10f;
        color = Color.green;
        shelt.transform.name = name;
        shelt.transform.position = position;
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
            if (shelt.transform.position.x.Near(RushPos.x) || shelt.transform.position.x.Near(BeginRushPos.x, dt / 10f))
            {
                rush = false;
            }
        }


        if (fall && !(shelt.GetComponent<Rigidbody2D>().velocity.y < -0.1f))
        {
            onFallOut = true;
            onFallEnter = true;
            fall = false;
            onFallOutFirst = true;
        }
        else
            fall = shelt.GetComponent<Rigidbody2D>().velocity.y < -0.1f;

        timeSinceLastSprt += dt;

        
        if (rush ? timeSinceLastSprt >= GV.Instance.timeBetwenRushSprt : timeSinceLastSprt >= GV.Instance.timeBetwenSprt)
        {
            timeSinceLastSprt = 0;
            shelt.GetComponent<SpriteRenderer>().flipX = !direction;

            if (fall && onFallEnter)
            {
                spriteCurs = 0;
                onFallEnter = false;
            }
            else if (onFallOutFirst)
            {
                spriteCurs = 0;
                onFallOutFirst = false;
            }
            else
            {
                spriteCurs++;
            }

            if (fall)
            {
            spriteCurs = spriteCurs % GV.ws.fall.Length;
            shelt.GetComponent<SpriteRenderer>().sprite = GV.ws.fall[spriteCurs];
            }
            else if (onFallOut)
            {
                shelt.GetComponent<SpriteRenderer>().sprite = GV.ws.recover[spriteCurs];
                if (spriteCurs == GV.ws.recover.Length - 1)
                {
                    onFallOut = false;
                    spriteCurs = 0;
                }
            }
            else
            {
            spriteCurs = spriteCurs % GV.ws.move.Length;
            shelt.GetComponent<SpriteRenderer>().sprite = GV.ws.move[spriteCurs];
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
