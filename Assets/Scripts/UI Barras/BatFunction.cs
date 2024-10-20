using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatFunction : MonoBehaviour
{
    public bool active;
    public bool isunique;
    public GameObject b1;
    public GameObject b2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !active)
        {
            BatFunction bat = other.GetComponentInChildren<BatFunction>();
            bat.transform.gameObject.SetActive(true);
            bat.ActiveModel();
            this.gameObject.SetActive(false);
            active = true;
        }
    }

    public void ActiveModel()
    {
        b1.SetActive(true);
        b2.SetActive(true);
        isunique = true;
        active = true;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            
        }
    }

}
