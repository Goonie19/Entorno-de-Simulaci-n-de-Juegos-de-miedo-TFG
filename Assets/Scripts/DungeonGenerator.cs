using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour {

    public GameObject firstRoom;
    public GameObject player;

    public List<GameObject> RoomList;

    public List<GameObject> AddDoors(List<GameObject> Doors, int index, int numDoors, int AvoidDoor)
    {

        for(int i = 0; i < numDoors; ++i)
        {
            if(i != AvoidDoor)
            {
                Doors.Add(RoomList[index].transform.GetChild(i).gameObject);
            }
        }

        return Doors;
    }

    // Start is called before the first frame update
    void Start()
    {

        Vector3 SpawnPos = new Vector3(0, 0.5f, 0);

        int randomNumberRoom = Random.Range(0, 15);

        List<GameObject> Doors = new List<GameObject>();

        List<GameObject> Rooms = RoomList;


        Instantiate(firstRoom);

        Vector3 spawnpos2 = firstRoom.transform.GetChild(0).transform.position;
        spawnpos2.Set(spawnpos2.x, spawnpos2.y, spawnpos2.z - 3.5f * 3f);


        //Change rotation of door
        Vector3 Rot = RoomList[randomNumberRoom].transform.rotation.eulerAngles;
        Rot = new Vector3(Rot.x, Rot.y + 180, Rot.z);

        Instantiate(RoomList[randomNumberRoom], spawnpos2, Quaternion.Euler(Rot));

        for(int j = 0; j < 3; ++j)
        {
            Doors.Add(firstRoom.transform.GetChild(j).gameObject);
        }

        int i = 12;
        while(i >= 0 && Doors.Count > 0)
        {
            randomNumberRoom = Random.Range(0, 15);
            int numDoors = Rooms[randomNumberRoom].GetComponent<Room>().numDoors;
            int randomNumberDoor = Random.Range(0, numDoors - 1);

            if (numDoors <= i)
            {
                Doors = AddDoors(Doors, randomNumberRoom, numDoors, randomNumberDoor);

                //Invertir el angulo de la puerta para obtener que orientacion tendrá la habitacion
                Vector3 rotation = Doors[0].transform.rotation.eulerAngles;

                if (rotation.y == -90 || rotation.y == 0)
                    rotation.Set(rotation.x, rotation.y + 180, rotation.z);
                
                if(rotation.y == 90 || rotation.y == 180)
                    rotation.Set(rotation.x, rotation.y - 180, rotation.z);

                i -= numDoors;
            }


        }

        Instantiate(player, SpawnPos, player.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
