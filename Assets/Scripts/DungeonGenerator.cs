using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class DungeonGenerator : MonoBehaviour {

    public GameObject firstRoom;
    public GameObject player;

    private FurnitureGenerator FurnitureGenerator;

    public GameObject wall;
    public GameObject lightning;

    private int numRooms = LevelManager.getRooms();

    public List<GameObject> RoomList;
    private List<GameObject> Dungeon = new List<GameObject>();

    public List<GameObject> angFurnitureList;
    public List<GameObject> wallFloorFurnitureList;
    public List<GameObject> FloorFurniture;

    private int numDoors = 0;

    private int lastIndex = 0;

    public bool Factibilidad(GameObject room, Vector3 position, int indexAvoid) {

        bool fact = true;

        int j = 0;

        while (fact && j < Dungeon.Count)
        {
            if(Vector3.Distance(Dungeon[j].transform.position, position) < room.GetComponent<Room>().radius + Dungeon[j].GetComponent<Room>().radius && j !=indexAvoid)
            {            
                fact = false;
            }
            ++j;
        }

        return fact;
    }

    public void CambiarPuertas(GameObject room, int grades)
    {
        switch(grades)
        {
            case 90:
                {
                    for(int i = 0; i < room.GetComponent<Room>().numDoors + 1; ++i)
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
                    for(int i = 0; i < room.GetComponent<Room>().numDoors + 1; ++i)
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
                    for(int i = 0; i < room.GetComponent<Room>().numDoors + 1; ++i)
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
    


    public Queue<GameObject> AddDoors(Queue<GameObject> Doors, GameObject room, int numDoors)
    {

        for(int i = 0; i < numDoors + 1; ++i)
        {
            if(!room.transform.GetChild(i).gameObject.GetComponent<Door>().conected)
            {
                Doors.Enqueue(room.transform.GetChild(i).gameObject);
            }
        }

        return Doors;
    }

    public bool createRoom(GameObject room, GameObject door)
    {
        bool fact = true;
        Quaternion rot = room.transform.rotation;
        switch(door.GetComponent<Door>().idDoor)
        {
            case "izquierda":
                {
                    rot *= Quaternion.Euler(0, -90, 0);

                    Vector3 pos = door.transform.position;
                    pos.x = pos.x - room.transform.GetChild(0).gameObject.GetComponent<Door>().xCoord - 3 + door.gameObject.GetComponent<Door>().modifier;
                    pos.z = pos.z + room.transform.GetChild(0).gameObject.GetComponent<Door>().zCoord;

                    int indexAvoid = door.transform.parent.GetComponent<Room>().id;

                    fact = Factibilidad(room, pos, indexAvoid);

                    if(fact)
                    {
                        GameObject r = Instantiate(room, pos, rot);

                        r.transform.GetChild(0).gameObject.GetComponent<Door>().conected = true;
                        door.gameObject.GetComponent<Door>().conected = true;

                        r.GetComponent<Room>().id = lastIndex + 1;
                        lastIndex++;

                        Dungeon.Add(r);

                        CambiarPuertas(Dungeon[Dungeon.Count - 1], -90);
                    }

                };break;

            case "arriba":
                {
                    

                    Vector3 pos = door.transform.position;
                    pos.z = pos.z + room.transform.GetChild(0).gameObject.GetComponent<Door>().xCoord + 3 - door.gameObject.GetComponent<Door>().modifier;
                    pos.x = pos.x + room.transform.GetChild(0).gameObject.GetComponent<Door>().zCoord;

                    int indexAvoid = door.transform.parent.GetComponent<Room>().id;

                    fact = Factibilidad(room, pos, indexAvoid);

                    if(fact)
                    {
                        GameObject r = Instantiate(room, pos, rot);
                        r.transform.GetChild(0).gameObject.GetComponent<Door>().conected = true;
                        door.gameObject.GetComponent<Door>().conected = true;

                        r.GetComponent<Room>().id = lastIndex + 1;
                        lastIndex++;

                        Dungeon.Add(r);
                    }
                }; break;

            case "derecha":
                {
                    rot *= Quaternion.Euler(0,90,0);

                    Vector3 pos = door.transform.position;
                    pos.x = pos.x + room.transform.GetChild(0).gameObject.GetComponent<Door>().xCoord + 3 - door.gameObject.GetComponent<Door>().modifier;
                    if(room.GetComponent<Room>().room)
                        pos.z = pos.z +(-1 *  room.transform.GetChild(0).gameObject.GetComponent<Door>().zCoord);
                    else
                        pos.z = pos.z + room.transform.GetChild(0).gameObject.GetComponent<Door>().zCoord;

                    int indexAvoid = door.transform.parent.GetComponent<Room>().id;

                    fact = Factibilidad(room, pos, indexAvoid);

                    if(fact)
                    {
                        GameObject r = Instantiate(room, pos, rot);
                        r.transform.GetChild(0).gameObject.GetComponent<Door>().conected = true;
                        door.gameObject.GetComponent<Door>().conected = true;

                        r.GetComponent<Room>().id = lastIndex + 1;
                        lastIndex++;

                        Dungeon.Add(r);

                        CambiarPuertas(Dungeon[Dungeon.Count - 1], 90);
                    }
                }; break;
            
            case "abajo":
                {
                    rot *= Quaternion.Euler(0, 180, 0);

                    Vector3 pos = door.transform.position;
                    pos.x = pos.x - room.transform.GetChild(0).gameObject.GetComponent<Door>().zCoord;
                    pos.z = pos.z - room.transform.GetChild(0).gameObject.GetComponent<Door>().xCoord - 3 + door.gameObject.GetComponent<Door>().modifier;

                    int indexAvoid = door.transform.parent.GetComponent<Room>().id;

                    fact = Factibilidad(room, pos, indexAvoid);

                    if(fact)
                    {
                        GameObject r = Instantiate(room, pos, rot);
                        r.transform.GetChild(0).gameObject.GetComponent<Door>().conected = true;
                        door.gameObject.GetComponent<Door>().conected = true;
                        r.GetComponent<Room>().id = lastIndex + 1;
                        lastIndex++;

                        Dungeon.Add(r);

                        CambiarPuertas(Dungeon[Dungeon.Count - 1], 180);
                    }

                    
                };break;
        }
        return fact;
    }

    // Start is called before the first frame update
    void Start()
    {

        Vector3 SpawnPos = new Vector3(0, 0.5f, 0);

        int intentos = 300;

        Queue<GameObject> Doors = new Queue<GameObject>();
        List<GameObject> Rooms = new List<GameObject>(RoomList);
        

        Dungeon.Add(Instantiate(firstRoom));

        for (int i = 0; i < 3; ++i)
            Doors.Enqueue(Dungeon[0].transform.GetChild(i).gameObject);

        numDoors = 0;
        

        while (numDoors < numRooms - 1 && Doors.Count > 0) {

            int r = Random.Range(0, Rooms.Count -1);

            if (createRoom(Rooms[r].gameObject, Doors.Peek())) {
                Doors.Dequeue();
                Doors = AddDoors(Doors, Dungeon[Dungeon.Count - 1].gameObject, Dungeon[Dungeon.Count - 1].transform.GetComponent<Room>().numDoors);
                numDoors += 1;
                intentos = 300;
            } else
            {
                --intentos;
            }
            if (intentos <= 0 && Doors.Count > 0)
            {
                Doors.Dequeue();
                intentos = 300;
            }
        }

        for (int s = 0; s < Dungeon.Count; ++s)
        {
            for (int j = 0; j < Dungeon[s].GetComponent<Room>().numDoors + 1; ++j)
            {
                if(!Dungeon[s].transform.GetChild(j).GetComponent<Door>().conected)
                {
                    Vector3 pos = Dungeon[s].transform.GetChild(j).GetChild(1).position;
                    Quaternion rot = Dungeon[s].transform.GetChild(j).GetChild(1).rotation;
                    Destroy(Dungeon[s].transform.GetChild(j).GetChild(1).gameObject);
                    Instantiate(wall, pos, rot);
                }
            }
        }

        FurnitureGenerator = new FurnitureGenerator(Dungeon, angFurnitureList, wallFloorFurnitureList, FloorFurniture, lightning);

        FurnitureGenerator.generateFurniture();

        player.transform.GetChild(0).GetComponent<Camera>().fieldOfView = LevelManager.getFov();

        GetComponent<AudioSource>().clip = LevelManager.getMusic();
        GetComponent<AudioSource>().volume = LevelManager.getVolume();
        GetComponent<AudioSource>().pitch = LevelManager.getPitch();
        GetComponent<AudioSource>().Play();

        Instantiate(player, SpawnPos, player.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
