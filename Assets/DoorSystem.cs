using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSystem : MonoBehaviour
{
    public Animator animator;
    public bool triggerON;
    public bool estado;
    public GameObject textoPuertaON;
    public GameObject textoPuertaOFF;
    public BoxCollider box;
    void Start()
    {
        //animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.E) && triggerON == true)
        {
            if (estado == true)
            {
                estado =false;
                animator.SetBool("seAbrio", false);
                StartCoroutine(ActivacionTrigger()); 
            }
            else if (estado == false)
            {
                estado = true;
                animator.SetBool("seAbrio", true);
                StartCoroutine(ActivacionTrigger());
            }
        }
        
    }


    private void OnTriggerEnter(Collider other)
    {
        triggerON = true;
        if (other.CompareTag("Player"))
        {
            if (estado == true)
            {
                textoPuertaON.SetActive(true);
                textoPuertaOFF.SetActive(false);
            }
            else if(estado == false)
            {
                textoPuertaOFF.SetActive(true);
                textoPuertaON.SetActive(false);

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        triggerON = false;

        if (other.CompareTag("Player"))
        {
            textoPuertaON.SetActive(false);
            textoPuertaOFF.SetActive(false);
        }
    }

    IEnumerator ActivacionTrigger()
    {
        box.isTrigger = true;
        yield return new WaitForSeconds(0.9f);
        box.isTrigger = false;
    }
}
