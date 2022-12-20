using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSliderScript : MonoBehaviour
{
    Text itemValue;

    [SerializeField]int maxValue = 5;
    int currentValue = 0;

    // Start is called before the first frame update
    void Start()
    {
        itemValue = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //SliderUpdate();
    }

    public void SetItemMaxValue(int value)
    {
        maxValue = value;
        Debug.Log("maxValue: " + maxValue);
    }

    public int GetItemMaxValue()
    {
        return currentValue;
    }
    public void TextUpdate(float value)
    {
        currentValue = (int)(value * maxValue);
        itemValue.text = currentValue.ToString();
        
    }

    //public void SliderUpdate()
    //{
    //    var slider = GetComponentInChildren<Slider>();
    //    float temp = int.Parse(itemValue.text);
    //    if (temp > maxValue)
    //    {
    //        itemValue.text = maxValue.ToString();
    //        temp = maxValue;
    //    }
    //    if(temp < 0)
    //    {
    //        itemValue.text = "0";
    //        temp = 0;
    //    }
           
    //    slider.value = temp / maxValue;
    //}
}
