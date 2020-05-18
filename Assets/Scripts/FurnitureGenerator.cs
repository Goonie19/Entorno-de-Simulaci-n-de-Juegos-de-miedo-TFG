using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureGenerator : MonoBehaviour
{

    public List<GameObject> angFurnitureList;

    public List<GameObject> wallFloorFurniture;

    private List<GameObject> furniture;

    public List<GameObject> lightning;

    private List<GameObject> dungeon;

    public FurnitureGenerator(List<GameObject> dungeon, List<GameObject> angFurnitureList, List<GameObject> wallFloorFurniture)
    {
        this.dungeon = dungeon;
        this.angFurnitureList = angFurnitureList;
        this.wallFloorFurniture = wallFloorFurniture;
    }

    public void GenerateFurnitureRoom(int i)
    {
        for(int j = 0; j < dungeon[i].transform.childCount; ++j)
        {
            if(dungeon[i].transform.GetChild(j).GetComponent<Angle>())
            {
                int r = Random.Range(0, angFurnitureList.Count);

                furniture.Add(Instantiate(angFurnitureList[r], dungeon[i].transform.GetChild(j).transform.GetChild(0).transform.position, dungeon[i].transform.GetChild(j).transform.rotation));
            }
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
