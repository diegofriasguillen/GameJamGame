using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parpadeo : MonoBehaviour
{
    public bool iniciar = false;
    public Light point;
    void Start()
    {
        point = gameObject.GetComponent<Light>();
        point.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (iniciar == true)
        {
            iniciar = false;
            StartCoroutine(parpadeoo());
        }
    }

    IEnumerator parpadeoo() 
    {
        point.enabled = true;
        yield return new WaitForSeconds(0.1f);
        point.enabled = false;
        yield return new WaitForSeconds(0.1f);
        point.enabled = true;
        yield return new WaitForSeconds(0.1f);
        point.enabled = false;
        yield return new WaitForSeconds(0.1f);
        point.enabled = true;
        yield return new WaitForSeconds(0.1f);
        point.enabled = false;
        yield return new WaitForSeconds(0.1f);
        point.enabled = true;
        point.enabled = false;

    }
}
