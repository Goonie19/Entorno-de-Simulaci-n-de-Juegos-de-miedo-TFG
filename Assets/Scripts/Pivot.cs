using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Pivot : MonoBehaviour
{

    private Quaternion rotation;
    private bool changedRotation = false;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("FPSController(Clone)").GetComponent<FirstPersonController>().getTurnAraunds() >= GameObject.Find("Spawner").GetComponent<DungeonGenerator>().MaxTurnAraunds && !changedRotation)
        {
            rotation = GameObject.Find("FPSController(Clone)").transform.rotation;
            changedRotation = true;
        }
        this.transform.rotation = rotation;
    }
}
