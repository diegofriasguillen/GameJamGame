using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Image sliderMiedo;
    public Image sliderSueno;
    public Image sliderTCam;
    public Image sliderBgTCam;
    public Image sliderBgTKid;
    public Color normal;
    public Color normalBgCam;
    public Color penalty;
    public Color penaltyBgCam;

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

    public void RedColorCam()
    {
        sliderTCam.color = penalty;
        sliderBgTCam.color = penaltyBgCam;
        sliderBgTKid.color = penaltyBgCam;
    }
    public void NormalColorCam()
    {
        sliderTCam.color = normal;        
        sliderBgTCam.color = normalBgCam;
        sliderBgTKid.color = normalBgCam;

    }

}
