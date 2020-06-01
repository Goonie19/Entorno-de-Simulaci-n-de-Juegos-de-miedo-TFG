using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowValueScript : MonoBehaviour
{

    Text percentageText;
    // Start is called before the first frame update
    void Start()
    {
        percentageText = GetComponent<Text>();
    }


    public void textUpdate(float percentage)
    {
        percentageText.text = Mathf.RoundToInt(percentage * 100) + "%";
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
