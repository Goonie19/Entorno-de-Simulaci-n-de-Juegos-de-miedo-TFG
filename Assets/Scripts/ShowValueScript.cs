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
        if (percentageText.text != null)
            percentageText.text = Mathf.RoundToInt(this.transform.parent.GetComponent<Slider>().value * 100).ToString()  + "%";
    }


    public void textUpdate(float percentage)
    {
        if(percentageText != null)
            percentageText.text = Mathf.RoundToInt(percentage * 100).ToString() + "%";
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
