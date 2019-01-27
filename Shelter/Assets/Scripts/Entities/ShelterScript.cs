using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelterScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EnvironmentManager.Instance.RegisterShelter(this);
    }

    public void ResetImg()
    {
        GetComponent<SpriteRenderer>().sprite = GV.ws.house[0];
    }

    public void CloseHouse()
    {
        GetComponent<SpriteRenderer>().sprite = GV.ws.house[1];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
