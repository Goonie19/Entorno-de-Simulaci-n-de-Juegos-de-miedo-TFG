using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    private static float Light = 0.3f;

    private static int numRooms = 4;

    private static float fov = 50;

    public List<AudioClip> music;

    private static AudioClip selectedSong;

    private static float musicVolume;

    private static float pitch = 1;

    private static Color lightColor;
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

    public void setFov(float fieldOfView)
    {
        fov = fieldOfView;
    }

    public static float getFov()
    {
        return fov;
    }

    public void setMusic(int song)
    {
        selectedSong = music[song];
    }

    public static AudioClip getMusic()
    {
        return selectedSong;
    }

    public void setVolume(float volume)
    {
        musicVolume = volume;
    }

    public static float getVolume()
    {
        return musicVolume;
    }

    public void setPitch(float fpitch)
    {
        pitch = fpitch;
    }

    public static float getPitch()
    {
        return pitch;
    }

    public void setColor(Color color)
    {
        lightColor = color;
    }

    public static Color getColor()
    {
        return lightColor;
    }
}
