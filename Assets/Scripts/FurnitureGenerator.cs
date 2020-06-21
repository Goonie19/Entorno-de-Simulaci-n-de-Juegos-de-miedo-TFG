using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureGenerator : MonoBehaviour
{

    private List<GameObject> angFurnitureList;

    private List<GameObject> wallFloorFurniture;

    private List<GameObject> FloorFurniture;

    private List<GameObject> furniture;

    private GameObject lightning;

    private GameObject dungeon;

    private float lightPercent = LevelManager.getLight();

    public bool Fact(GameObject obj, Vector3 pos, int indexRoom)
    {
        bool fact = true;

        int i = 0;

        while (fact && i < dungeon.transform.GetChild(indexRoom).GetComponent<Room>().getFurnitureOfRoom().Count)
        { 
            if (Vector3.Distance(pos, dungeon.transform.GetChild(indexRoom).GetComponent<Room>().getFurnitureOfRoom()[i].transform.position) <
                obj.transform.GetComponent<Decoration>().radius + dungeon.transform.GetChild(indexRoom).GetComponent<Room>().getFurnitureOfRoom()[i].transform.GetComponent<Decoration>().radius)
                fact = false;
            else
                ++i;
        }


        for(int l = 0; l < dungeon.transform.GetChild(indexRoom).GetComponent<Room>().numDoors + 1 && fact; ++l)
        {
            if (Vector3.Distance(pos, dungeon.transform.GetChild(indexRoom).GetComponent<Room>().transform.GetChild(l).transform.position) <
               obj.transform.GetComponent<Decoration>().radius + dungeon.transform.GetChild(indexRoom).GetComponent<Room>().transform.GetChild(l).GetComponent<Door>().getRadius())
                fact = false;
        }

        return fact;
    }

    public FurnitureGenerator(GameObject dungeon, List<GameObject> angFurnitureList, List<GameObject> wallFloorFurniture, List<GameObject> FloorFurniture, GameObject lightning)
    {
        this.dungeon = dungeon;
        this.angFurnitureList = angFurnitureList;
        this.wallFloorFurniture = wallFloorFurniture;
        furniture = new List<GameObject>();
        this.FloorFurniture = FloorFurniture;
        this.lightning = lightning;
    }

    public void GenerateFurnitureRoom(int i)
    {
        float maxLf = dungeon.transform.GetChild(i).transform.GetComponent<Room>().maxLights * lightPercent;
        int maxL = (int) maxLf;
        int intentos = 300;

        for(int j = 0; j < dungeon.transform.GetChild(i).transform.childCount; ++j)
        {
            if (dungeon.transform.GetChild(i).transform.GetChild(j).GetComponent<Angle>())
            {
                int r = Random.Range(0, angFurnitureList.Count);

                GameObject gAng = Instantiate(angFurnitureList[r], dungeon.transform.GetChild(i).transform.GetChild(j).transform.GetChild(0).transform.position, dungeon.transform.GetChild(i).transform.GetChild(j).transform.rotation);
                gAng.transform.parent = dungeon.transform;

                dungeon.transform.GetChild(i).transform.GetComponent<Room>().addFurniture(gAng);
            }
        }

        int l = 0;

        while(l < dungeon.transform.GetChild(i).transform.childCount && maxL > 0)
        {
            if (dungeon.transform.GetChild(i).transform.GetChild(l).GetComponent<Wall>() && intentos > 0)
            {

                Quaternion rot = dungeon.transform.GetChild(i).transform.GetChild(l).transform.rotation;
                rot *= Quaternion.Euler(0, 180, 0);

                Vector3 pos = dungeon.transform.GetChild(i).transform.GetChild(l).transform.GetChild(2).transform.position;
                pos.y -= 1.25f;

                if (rot.eulerAngles.y > -1 && rot.eulerAngles.y < 1)
                    pos.x += 1.25f;
                if (rot.eulerAngles.y > 89 && rot.eulerAngles.y < 91)
                    pos.z -= 1.25f;
                if (rot.eulerAngles.y > 179 && rot.eulerAngles.y < 181)
                    pos.x -= 1.25f;
                if (rot.eulerAngles.y > 269 && rot.eulerAngles.y < 271)
                    pos.z += 1.25f;

                if (Fact(lightning, pos, i))
                {
                    GameObject gLight = Instantiate(lightning, pos, rot);
                    gLight.transform.parent = dungeon.transform;
                    dungeon.transform.GetChild(i).transform.GetComponent<Room>().addFurniture(gLight);
                    --maxL;
                    intentos = 300;
                    ++l;
                }
                else
                    --intentos;

            }
            else
                ++l;
        }

        //instatiating wall furniture
        l = dungeon.transform.GetChild(i).transform.childCount - 1;
        int maxFurnitureWall = dungeon.transform.GetChild(i).transform.GetComponent<Room>().maxFurnitureWall;
        intentos = 300;

        while (l > 0 && maxFurnitureWall > 0)
        {
            if (dungeon.transform.GetChild(i).transform.GetChild(l).GetComponent<Wall>() && intentos > 0)
            {

                Quaternion rot2 = dungeon.transform.GetChild(i).transform.GetChild(l).transform.rotation;
                int r = Random.Range(0, wallFloorFurniture.Count);

                Vector3 pos = dungeon.transform.GetChild(i).transform.GetChild(l).transform.GetChild(0).transform.position;

                if (rot2.eulerAngles.y > -1 && rot2.eulerAngles.y < 1)
                    pos.x -= wallFloorFurniture[r].GetComponent<Decoration>().wallDistance;
                if (rot2.eulerAngles.y > 89 && rot2.eulerAngles.y < 91)
                    pos.z += wallFloorFurniture[r].GetComponent<Decoration>().wallDistance;
                if (rot2.eulerAngles.y > 179 && rot2.eulerAngles.y < 181)
                    pos.x += wallFloorFurniture[r].GetComponent<Decoration>().wallDistance;
                if (rot2.eulerAngles.y > 269 && rot2.eulerAngles.y < 271)
                    pos.z -= wallFloorFurniture[r].GetComponent<Decoration>().wallDistance;


                if (Fact(wallFloorFurniture[r], dungeon.transform.GetChild(i).transform.GetChild(l).transform.position, i)) {
                    --maxFurnitureWall;
                    GameObject gWall = Instantiate(wallFloorFurniture[r], pos, dungeon.transform.GetChild(i).transform.GetChild(l).transform.rotation * Quaternion.Euler(0, 90, 0));
                    gWall.transform.parent = dungeon.transform;
                    dungeon.transform.GetChild(i).transform.GetComponent<Room>().addFurniture(gWall);
                    --l;
                }
                else
                    --intentos;
            }
            else
                --l;

        }

        //instantiating floor furniture
        l = dungeon.transform.GetChild(i).transform.childCount - 2;
        intentos = 300;
        int floors = dungeon.transform.GetChild(i).GetComponent<Room>().floors;

        while(floors > 0 && i != 0)
        {
            if (dungeon.transform.GetChild(i).transform.GetChild(l).GetComponent<Floor>() && intentos > 0)
            {
                int r = Random.Range(0, FloorFurniture.Count);

                if (Fact(FloorFurniture[r], dungeon.transform.GetChild(i).transform.GetChild(l).transform.position, i))
                {
                    --floors;
                    GameObject g = Instantiate(FloorFurniture[r], dungeon.transform.GetChild(i).transform.GetChild(l).transform.position, dungeon.transform.GetChild(i).transform.GetChild(l).transform.rotation);
                    g.transform.parent = dungeon.transform;
                    dungeon.transform.GetChild(i).transform.GetComponent<Room>().addFurniture(g);
                    --l;
                }
                else
                    --intentos;
            }
        }

    }

    public void generateFurniture()
    {

        for(int i = 0; i < LevelManager.getRooms(); ++i)
        {
            GenerateFurnitureRoom(i);
        }

    }

    
}
