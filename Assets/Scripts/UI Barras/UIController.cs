using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Image sliderMiedo;
    public Image sliderSueno;
    public Image sliderTCam;


    public void SetValueMiedo(float percentage)
    {
        sliderMiedo.fillAmount = percentage / 100;
    }
    public void SetValueSueno(float percentage)
    {
        sliderSueno.fillAmount = percentage/100;
    }
    public void SetValueCam(float percentage)
    {
        sliderTCam.fillAmount = percentage/100;
    }


}
