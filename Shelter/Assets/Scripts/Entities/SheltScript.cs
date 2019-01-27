using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheltScript : MonoBehaviour
{
    public Shelt myShelt { get; private set; }

    void Start()
    {
    }

    public void Init(Shelt shelt)
    {
        myShelt = shelt;
        gameObject.transform.tag = "Shelt";
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Shelt") || col.transform.CompareTag("Obstacle"))
        {

            if (col.transform.CompareTag("Shelt") || (col.GetContact(0).point.y.Near(gameObject.transform.position.y + .35f, .05f) !=
                col.GetContact(0).point.x.Near(gameObject.transform.position.x + .35f, .05f))) {

                myShelt.Turn();
                myShelt.rush = false;
            }
        }

        if (col.transform.CompareTag("Shelter"))
        {
            SheltManager.Instance.SaveMe(myShelt);
            col.collider.isTrigger = true;
            col.transform.tag = "CloseShelter";
        }
    }

    
}
