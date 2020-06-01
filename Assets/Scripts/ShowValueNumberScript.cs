using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowValueNumberScript : MonoBehaviour
{

    Text NumberText;
    // Start is called before the first frame update
    void Start()
    {
        NumberText = GetComponent<Text>();
    }

    public void textUpdate(float percentage)
    {
        NumberText.text = "" + percentage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
