using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menus : MonoBehaviour
{

    public string tagUI;

    // Start is called before the first frame update
    void Start()
    {
        switch(tagUI)
        {
            case "LightRadius":
                {
                    this.GetComponent<Slider>().value = LevelManager.getRadiusLights();
                };break;
            case "LightColor":
                {
                    this.GetComponent<Dropdown>().value = LevelManager.getIColor();
                };break;
            case "FOV":
                {
                    this.GetComponent<Slider>().value = LevelManager.getFov();
                };break;
            case "Music":
                {
                    this.GetComponent<Dropdown>().value = LevelManager.getIMusic();
                }; break;
            case "MusicVolume":
                {
                    this.GetComponent<Slider>().value = LevelManager.getVolume();
                }; break;
            case "MusicPitch":
                {
                    this.GetComponent<Slider>().value = LevelManager.getPitch();
                }; break;
            case "EffectsVolume":
                {
                    this.GetComponent<Slider>().value = LevelManager.getEffectVolume();
                }; break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
