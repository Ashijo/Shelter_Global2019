using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheltScript : MonoBehaviour
{
    public Shelt myShelt { get; private set; }

    public void Init(Shelt shelt)
    {
        myShelt = shelt;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Shelt") || col.transform.CompareTag("Obstacle"))
        {
            //Debug.Log("ShellCollid");
            myShelt.Turn();
            myShelt.rush = false;
        }

        if (col.transform.CompareTag("Shelter"))
        {
            SheltManager.Instance.SaveMe(myShelt);
            col.collider.isTrigger = true;
            col.transform.tag = "CloseShelter";
        }
    }

    
}
