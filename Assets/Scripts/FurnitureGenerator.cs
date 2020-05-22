using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureGenerator : MonoBehaviour
{

    private List<GameObject> angFurnitureList;

    private List<GameObject> wallFloorFurniture;

    private List<GameObject> furniture;

    private GameObject lightning;

    private List<GameObject> dungeon;

    private int maxLightsRoom = 10;

    public bool Fact(GameObject obj, Vector3 pos, int indexRoom)
    {
        bool fact = true;

        int i = 0;

        while (fact && i < dungeon[indexRoom].GetComponent<Room>().getFurnitureOfRoom().Count)
        { 
            if (Vector3.Distance(pos, dungeon[indexRoom].GetComponent<Room>().getFurnitureOfRoom()[i].transform.position) <
                obj.transform.GetComponent<Decoration>().radius + dungeon[indexRoom].GetComponent<Room>().getFurnitureOfRoom()[i].transform.GetComponent<Decoration>().radius)
                fact = false;
            else
                ++i;
        }




        return fact;
    }

    public FurnitureGenerator(List<GameObject> dungeon, List<GameObject> angFurnitureList, List<GameObject> wallFloorFurniture, GameObject lightning)
    {
        this.dungeon = dungeon;
        this.angFurnitureList = angFurnitureList;
        this.wallFloorFurniture = wallFloorFurniture;
        furniture = new List<GameObject>();
        this.lightning = lightning;
    }

    public void GenerateFurnitureRoom(int i)
    {
        int maxL = dungeon[i].transform.GetComponent<Room>().maxLights;
        int intentos = 300;

        for(int j = 0; j < dungeon[i].transform.childCount; ++j)
        {
            if (dungeon[i].transform.GetChild(j).GetComponent<Angle>())
            {
                int r = Random.Range(0, angFurnitureList.Count);

                dungeon[i].transform.GetComponent<Room>().addFurniture(Instantiate(angFurnitureList[r], dungeon[i].transform.GetChild(j).transform.GetChild(0).transform.position, dungeon[i].transform.GetChild(j).transform.rotation));
            }
        }

        int l = 0;

        while(l < dungeon[i].transform.childCount && maxL > 0)
        {
            if (dungeon[i].transform.GetChild(l).GetComponent<Wall>() && intentos > 0)
            {

                Quaternion rot = dungeon[i].transform.GetChild(l).transform.rotation;
                rot *= Quaternion.Euler(0, 180, 0);

                Vector3 pos = dungeon[i].transform.GetChild(l).transform.GetChild(2).transform.position;
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
                    dungeon[i].transform.GetComponent<Room>().addFurniture(Instantiate(lightning, pos, rot));
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


        l = 0;
        int maxFurnitureWall = dungeon[i].transform.GetComponent<Room>().maxFurnitureWall;
        intentos = 300;

        while (l < dungeon[i].transform.childCount && maxFurnitureWall > 0)
        {
            if (dungeon[i].transform.GetChild(l).GetComponent<Wall>() && intentos > 0)
            {
                int r = Random.Range(0, wallFloorFurniture.Count);

                if (Fact(wallFloorFurniture[r], dungeon[i].transform.GetChild(l).transform.position, i))
                {
                    --maxFurnitureWall;
                    dungeon[i].transform.GetComponent<Room>().addFurniture(Instantiate(wallFloorFurniture[r], dungeon[i].transform.GetChild(l).transform.position, dungeon[i].transform.GetChild(l).transform.rotation));
                    ++l;
                }
                else
                    --intentos;
            }
            else
                ++l;

        }


    }

    public void generateFurniture()
    {

        for(int i = 0; i < dungeon.Count; ++i)
        {
            GenerateFurnitureRoom(i);
        }

    }

    
}
