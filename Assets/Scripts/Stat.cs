using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stat : MonoBehaviour
{
    private Image content;
    private float currentFill;

    [SerializeField]
    private float lerpSpeed;

    private float myMaxValue { get; set; }

    private float currentValue;

    public float MyCurrentValue
    {
        get
        {
            return currentValue;
        }

        set
        {
            if (value > myMaxValue)
            {
                currentValue = myMaxValue;     //making sure player health does not excede the cap
            }
            else if (value < 0)
            {
                currentValue = 0;              //making sure health does not go below 0
            }
            else
            {
                currentValue = value;
            }
            currentFill = currentValue / myMaxValue;
        }
    }


    // Use this for initialization
    void Start ()
    {
        content = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (currentFill != content.fillAmount)
        {
            content.fillAmount = Mathf.Lerp(content.fillAmount, currentFill, Time.deltaTime * lerpSpeed);   //making the health bar move smoothly
        }
	}
    public void Initialized(float currentValue, float maxValue)
    {
        myMaxValue = maxValue;
        MyCurrentValue = currentValue;
    }
}
