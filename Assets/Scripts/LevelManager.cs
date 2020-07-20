using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    private static float Light = 0.3f;

    private static float rLight = 7;

    private static int numRooms = 4;

    private static float fov = 50;

    public List<AudioClip> music;

    private static AudioClip selectedSong;

    private static float musicVolume;

    private static float effectVolume;

    private static float pitch = 1;

    private static Color lightColor;

    public List<Color> colors;

    // Start is called before the first frame update
    void Start()
    {
        lightColor = this.colors[0];
        selectedSong = music[0];
    }

    public void Close()
    {
        Application.Quit();
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

    public void setRadiusLights(float rLights)
    {
        rLight = rLights;
    }

    public static float getRadiusLights()
    {
        return rLight;
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

    public void setEffectVolume(float volume)
    {
        effectVolume = volume;
    }

    public static float getEffectVolume()
    {
        return effectVolume;
    }

    public void setPitch(float fpitch)
    {
        pitch = fpitch;
    }

    public static float getPitch()
    {
        return pitch;
    }

    public void setColor(int color)
    {
        lightColor = colors[color];
    }

    public static Color getColor()
    {
        return lightColor;
    }
}
