using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    public int numDoors;
    public int numWalls;
    public float radius;
    public int maxLights;
    public int maxFurnitureWall;
    public int maxFurnitureFloor;

    public bool big;

    private List<GameObject> furnitureOfRoom = new List<GameObject>();

    public int id;

    public List<GameObject> getFurnitureOfRoom()
    {
        return furnitureOfRoom;
    }

    public void addFurniture(GameObject furniture)
    {
        furnitureOfRoom.Add(furniture);
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
