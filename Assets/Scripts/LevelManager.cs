using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    private static float Light;

    private static int numRooms;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectOption(string sNameOption)
    {
        SceneManager.LoadScene(sNameOption);
    }

    public void setLights(float pLights)
    {
        Light = pLights;
    }

    public static float getLight()
    {
        return Light;
    }

    public void setRooms(float rooms) 
    {
        numRooms = (int) rooms;
    }

    public static int getRooms()
    {
        return numRooms;
    }
}
