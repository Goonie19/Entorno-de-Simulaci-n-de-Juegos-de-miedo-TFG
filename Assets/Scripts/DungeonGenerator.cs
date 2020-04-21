using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour {

    public GameObject firstRoom;
    public GameObject player;

    private int numRooms = 10;

    public List<GameObject> RoomList;
    public List<GameObject> RoomListEnd;
    private List<GameObject> Dungeon = new List<GameObject>();

    public bool Factibilidad(GameObject room, int sizeOfList) {

        bool fact = true;

        if (room.GetComponent<Room>().numDoors + sizeOfList > numRooms - 1 && sizeOfList > 0)
            fact = false;

        return fact;
    }

    public void CambiarPuertas(GameObject room, int grades)
    {
        switch(grades)
        {
            case 90:
                {
                    for(int i = 0; i < room.GetComponent<Room>().numDoors; ++i)
                    {
                        switch(room.transform.GetChild(i).gameObject.GetComponent<Door>().idDoor)
                        {
                            case "abajo": 
                                room.transform.GetChild(i).gameObject.GetComponent<Door>().idDoor = "izquierda";break;

                            case "izquierda":
                                room.transform.GetChild(i).gameObject.GetComponent<Door>().idDoor = "arriba";break;

                            case "arriba":
                                room.transform.GetChild(i).gameObject.GetComponent<Door>().idDoor = "derecha";break;

                            case "derecha":
                                room.transform.GetChild(i).gameObject.GetComponent<Door>().idDoor = "abajo";break;
                        }

                        
                    }
                };break;

            case 180:
                {
                    for(int i = 0; i < room.GetComponent<Room>().numDoors; ++i)
                    {
                        switch(room.transform.GetChild(i).gameObject.GetComponent<Door>().idDoor)
                        {
                            case "abajo":
                                room.transform.GetChild(i).gameObject.GetComponent<Door>().idDoor = "arriba";break;

                            case "izquierda":
                                room.transform.GetChild(i).gameObject.GetComponent<Door>().idDoor = "derecha";break;

                            case "arriba":
                                room.transform.GetChild(i).gameObject.GetComponent<Door>().idDoor = "abajo";break;

                            case "derecha":
                                room.transform.GetChild(i).gameObject.GetComponent<Door>().idDoor = "izquierda";break;
                        }
                    }
                };break;

            case -90:
                {
                    for(int i = 0; i < room.GetComponent<Room>().numDoors; ++i)
                    {
                        switch(room.transform.GetChild(i).gameObject.GetComponent<Door>().idDoor)
                        {
                            case "abajo":
                                room.transform.GetChild(i).gameObject.GetComponent<Door>().idDoor = "derecha";break;

                            case "izquierda":
                                room.transform.GetChild(i).gameObject.GetComponent<Door>().idDoor = "abajo";break;

                            case "arriba":
                                room.transform.GetChild(i).gameObject.GetComponent<Door>().idDoor = "izquierda";break;

                            case "derecha":
                                room.transform.GetChild(i).gameObject.GetComponent<Door>().idDoor = "arriba";break;
                        }
                    }
                };break;
        }
    }

    public List<GameObject> CopyList()
    {
        List<GameObject> copy = new List<GameObject>();

        for(int i = 0; i < RoomList.Count; ++i)
        {
            copy.Add(RoomList[i]);
        }

        return copy;
    }

    public List<GameObject> CopyListEnd()
    {
        List<GameObject> copy = new List<GameObject>();

        for (int i = 0; i < RoomListEnd.Count; ++i)
        {
            copy.Add(RoomListEnd[i]);
        }

        return copy;
    }

    public Queue<GameObject> AddDoors(Queue<GameObject> Doors, GameObject room, int numDoors)
    {

        for(int i = 0; i < numDoors; ++i)
        {
            if(!room.transform.GetChild(i).gameObject.GetComponent<Door>().conected)
            {
                Doors.Enqueue(room.transform.GetChild(i).gameObject);
            }
        }

        return Doors;
    }

    public void createRoom(GameObject room, GameObject door)
    {
        Quaternion rot = room.transform.rotation;
        switch(door.GetComponent<Door>().idDoor)
        {
            case "izquierda":
                {
                    rot *= Quaternion.Euler(0, -90, 0);

                    Vector3 pos = door.transform.position;
                    pos.x = pos.x - room.transform.GetChild(0).gameObject.GetComponent<Door>().xCoord - 3 + door.gameObject.GetComponent<Door>().modifier;
                    pos.z = pos.z + room.transform.GetChild(0).gameObject.GetComponent<Door>().zCoord;

                    GameObject r = Instantiate(room, pos, rot);

                    r.transform.GetChild(0).gameObject.GetComponent<Door>().conected = true;

                    Dungeon.Add(r);

                    CambiarPuertas(r, -90);

                };break;

            case "arriba":
                {
                    Quaternion rotation = room.transform.rotation;

                    Vector3 pos = door.transform.position;
                    pos.z = pos.z + room.transform.GetChild(0).gameObject.GetComponent<Door>().xCoord + 3 - door.gameObject.GetComponent<Door>().modifier;
                    pos.x = pos.x + room.transform.GetChild(0).gameObject.GetComponent<Door>().zCoord;

                    GameObject r = Instantiate(room, pos, rotation);
                    r.transform.GetChild(0).gameObject.GetComponent<Door>().conected = true;

                    Dungeon.Add(r);
                }; break;

            case "derecha":
                {
                    rot *= Quaternion.Euler(0,90,0);

                    Vector3 pos = door.transform.position;
                    pos.x = pos.x + room.transform.GetChild(0).gameObject.GetComponent<Door>().xCoord + 3 - door.gameObject.GetComponent<Door>().modifier;
                    pos.z = pos.z + room.transform.GetChild(0).gameObject.GetComponent<Door>().zCoord;

                    GameObject r = Instantiate(room, pos, rot);
                    r.transform.GetChild(0).gameObject.GetComponent<Door>().conected = true;

                    Dungeon.Add(r);

                    CambiarPuertas(r, 90);
                }; break;
            
            case "abajo":
                {
                    rot *= Quaternion.Euler(0, 180, 0);

                    Vector3 pos = door.transform.position;
                    pos.x = pos.x - room.transform.GetChild(0).gameObject.GetComponent<Door>().zCoord;
                    pos.z = pos.z - room.transform.GetChild(0).gameObject.GetComponent<Door>().xCoord - 3 + door.gameObject.GetComponent<Door>().modifier;

                    GameObject r = Instantiate(room, pos, rot);
                    r.transform.GetChild(0).gameObject.GetComponent<Door>().conected = true;

                    Dungeon.Add(r);

                    CambiarPuertas(r, 180);

                    
                };break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        Vector3 SpawnPos = new Vector3(0, 0.5f, 0);

        Queue<GameObject> Doors = new Queue<GameObject>();
        List<GameObject> Rooms = new List<GameObject>(RoomList);
        List<GameObject> RoomsEnd = CopyListEnd();

        Dungeon.Add(Instantiate(firstRoom));

        for (int i = 0; i < 3; ++i)
            Doors.Enqueue(Dungeon[0].transform.GetChild(i).gameObject);
        

        while (Doors.Count < numRooms - 1 && Doors.Count > 0) {

            int r = Random.Range(0, Rooms.Count -1);

    
            createRoom(Rooms[r].gameObject, Doors.Dequeue());
            Doors = AddDoors(Doors, Dungeon[Dungeon.Count - 1].gameObject, Dungeon[Dungeon.Count -1].transform.GetComponent<Room>().numDoors);
        }

       /*while (roomspawned < numRooms && Doors.Count > 0) {
            int r = Random.Range(0,2);

            createRoom(RoomsEnd[r].gameObject, Doors[0].gameObject);
            Doors.RemoveAt(0);

            roomspawned++;
        }*/

        Instantiate(player, SpawnPos, player.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
