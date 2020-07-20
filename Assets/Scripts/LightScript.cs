using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    private float radius = 0;

    private Color color;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(radius != LevelManager.getRadiusLights())
        {
            radius = LevelManager.getRadiusLights();
            this.gameObject.transform.GetChild(0).GetComponent<Light>().range = radius;
        }
        if(color != LevelManager.getColor() && LevelManager.getColor() != null)
        {
            color = LevelManager.getColor();
            this.gameObject.transform.GetChild(0).GetComponent<Light>().color = color;
        }

    }
}
