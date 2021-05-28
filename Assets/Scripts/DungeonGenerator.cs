using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.AI;
using UnityEngine.UI;

public class DungeonGenerator : MonoBehaviour {

    public GameObject firstRoom;
    public GameObject player;

    public List<GameObject> enemies;

    private GameObject enemy;

    private GameObject enemyInstance;

    private GameObject ExportDungeon;

    private FurnitureGenerator FurnitureGenerator;

    public GameObject wall;
    public GameObject lightning;

    private int numRooms = LevelManager.getRooms();

    public List<GameObject> RoomList;
    public List<GameObject> angFurnitureList;
    public List<GameObject> wallFloorFurnitureList;
    public List<GameObject> FloorFurniture;

    private int numDoors = 0;

    private int lastIndex = 0;

    public float fov;

    public int MaxTurnAraunds;

    public bool Factibilidad(GameObject room, Vector3 position, int indexAvoid) {

        bool fact = true;

        int j = 0;

        while (fact && j < ExportDungeon.transform.childCount)
        {
            if(Vector3.Distance(ExportDungeon.transform.GetChild(j).transform.position, position) < room.GetComponent<Room>().radius + ExportDungeon.transform.GetChild(j).GetComponent<Room>().radius && j !=indexAvoid)
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

                        r.transform.parent = ExportDungeon.transform;
                        
                        lastIndex++;

                        CambiarPuertas(ExportDungeon.transform.GetChild(ExportDungeon.transform.childCount - 1).gameObject, -90);
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
                        r.transform.parent = ExportDungeon.transform;

                        lastIndex++;                    }
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
                        r.transform.parent = ExportDungeon.transform;

                        lastIndex++;

                        CambiarPuertas(ExportDungeon.transform.GetChild(ExportDungeon.transform.childCount - 1).gameObject, 90);
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
                        r.transform.parent = ExportDungeon.transform;

                        CambiarPuertas(ExportDungeon.transform.GetChild(ExportDungeon.transform.childCount - 1).gameObject, 180);
                    }

                    
                };break;
        }
        return fact;
    }

    // Start is called before the first frame update
    void Start()
    {
        PauseMenu.gameIsPaused = false;
        Vector3 SpawnPos = new Vector3(0, 1.16f, 0);

        ExportDungeon = new GameObject();

        int intentos = 300;

        Queue<GameObject> Doors = new Queue<GameObject>();
        List<GameObject> Rooms = new List<GameObject>(RoomList);
        

        Instantiate(firstRoom).transform.parent = ExportDungeon.transform;

        lightning.transform.GetChild(0).GetComponent<Light>().color = LevelManager.getColor();

        for (int i = 0; i < 3; ++i)
            Doors.Enqueue(ExportDungeon.transform.GetChild(0).transform.GetChild(i).gameObject);

        numDoors = 0;
        

        while (numDoors < numRooms - 1 && Doors.Count > 0) {

            int r = Random.Range(0, Rooms.Count -1);

            if (createRoom(Rooms[r].gameObject, Doors.Peek())) {
                Doors.Dequeue();
                Doors = AddDoors(Doors, ExportDungeon.transform.GetChild(ExportDungeon.transform.childCount - 1).gameObject, ExportDungeon.transform.GetChild(ExportDungeon.transform.childCount - 1).transform.GetComponent<Room>().numDoors);
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

        for (int s = 0; s < ExportDungeon.transform.childCount; ++s)
        {
            for (int j = 0; j < ExportDungeon.transform.GetChild(s).GetComponent<Room>().numDoors + 1; ++j)
            {
                if(!ExportDungeon.transform.GetChild(s).transform.GetChild(j).GetComponent<Door>().conected)
                {
                    Vector3 pos = ExportDungeon.transform.GetChild(s).transform.GetChild(j).GetChild(1).position;
                    Quaternion rot = ExportDungeon.transform.GetChild(s).transform.GetChild(j).GetChild(1).rotation;
                    Destroy(ExportDungeon.transform.GetChild(s).transform.GetChild(j).GetChild(1).gameObject);
                    Instantiate(wall, pos, rot).transform.parent = ExportDungeon.transform.GetChild(s).transform.GetChild(j).transform;
                }
            }
        }

        FurnitureGenerator = new FurnitureGenerator(ExportDungeon, angFurnitureList, wallFloorFurnitureList, FloorFurniture, lightning);

        FurnitureGenerator.generateFurniture();

        player.transform.GetChild(0).GetComponent<Camera>().fieldOfView = LevelManager.getFov();

        if(LevelManager.getMusic())
            GetComponent<AudioSource>().clip = LevelManager.getMusic();
        GetComponent<AudioSource>().volume = LevelManager.getVolume();
        GetComponent<AudioSource>().pitch = LevelManager.getPitch();
        GetComponent<AudioSource>().Play();

        ExportDungeon.isStatic = true;

        ExportDungeon.AddComponent<NavMeshSurface>().collectObjects = CollectObjects.Children;

        ExportDungeon.GetComponent<NavMeshSurface>().BuildNavMesh();

        player = Instantiate(player, SpawnPos, player.transform.rotation);

        enemies[0].transform.GetComponent<EnemyScriptChaser>().target = player;

        enemy = enemies[0];

        player.GetComponent<FirstPersonController>().setEnemyBack(false);

            InvokeRepeating("enemyInstantiateBack", Random.Range(30f, 120f), 100f);

       

    }

    // Update is called once per frame
    void Update()
    {
        if (fov != LevelManager.getFov())
        {
            fov = LevelManager.getFov();
            player.transform.GetChild(0).GetComponent<Camera>().fieldOfView = fov;
        }

        if(GetComponent<AudioSource>().pitch != LevelManager.getPitch())
            GetComponent<AudioSource>().pitch = LevelManager.getPitch();

        if (GetComponent<AudioSource>().volume != LevelManager.getVolume())
            GetComponent<AudioSource>().volume = LevelManager.getVolume();

        if(GetComponent<AudioSource>().clip != LevelManager.getMusic() && GetComponent<AudioSource>().clip != null)
        {
            GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().clip = LevelManager.getMusic();
            GetComponent<AudioSource>().Play();
        }

        if (player.GetComponent<FirstPersonController>().getTurnAraunds() == 0 && player.GetComponent<FirstPersonController>().getEnemyBack())
            MaxTurnAraunds = (int) Random.Range(2f, 10f);

        if (player.GetComponent<FirstPersonController>().getTurnAraunds() > MaxTurnAraunds)
            enemyInstance.transform.parent.GetComponent<Pivot>().enabled = true;
        

    }

    void enemyInstantiateBack()
    {
        if (LevelManager.getJumpScare())
        {
            if (!player.GetComponent<FirstPersonController>().getEnemyBack())
            {
                if (enemy != null)
                {
                    if (enemyInstance== null || Vector3.Distance(GameObject.Find("FPSController(Clone)").transform.position, enemy.transform.position) > 60)
                    {
                        Vector3 spawnpos = player.transform.position - (player.transform.forward * 1.4f);
                        spawnpos.y = 0;
                        enemy.GetComponent<NavMeshAgent>().enabled = false;
                        enemy.GetComponent<Rigidbody>().useGravity = false;
                        enemy.GetComponent<BoxCollider>().enabled = false;
                        if (enemyInstance != null)
                            Destroy(enemyInstance);
                        enemyInstance = Instantiate(enemy, spawnpos, player.transform.rotation);
                        player.transform.GetChild(1).transform.rotation = player.transform.rotation;
                        enemyInstance.transform.parent = player.transform.GetChild(1).transform;


                        player.GetComponent<FirstPersonController>().setEnemyBack(true);
                    }
                }
            }
        }
    }
}
