using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slider : MonoBehaviour
{
    Text percentageText;
    // Start is called before the first frame update
    void Start()
    {
        percentageText = GetComponent<Text>();
    }

    public void TextUpdate(float value)
    {
        percentageText.text = "Set number of passengers: " + Mathf.RoundToInt(value);
    }
}
