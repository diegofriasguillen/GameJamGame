using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HoursTimer : MonoBehaviour
{
    public TextMeshProUGUI timeUI;
    public string text = "08:00";
    public float timeStart = 8;
    public float time = 90;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(timer());
    }

    IEnumerator timer()
    {
        yield return new WaitForSeconds(time);
        
            timeStart++;
            TextUpdate();
        StartCoroutine(timer());
    }

    void TextUpdate()
    {
        Debug.Log(timeStart%13);

        if(timeStart % 13 == 0)
        {
            timeStart = 1;
        }
        text = timeStart.ToString("##") + ":00";
        timeUI.text = text;
        Debug.Log(text);
    }

}
