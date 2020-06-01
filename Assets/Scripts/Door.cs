using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public float xCoord;
    public float zCoord;

    public string idDoor;

    public bool conected = false;

    public float modifier = 0;

    private float radius = 1.5f;

    public float getRadius()
    {
        return radius;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
