using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOculto : MonoBehaviour
{
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetTrigger("AbrirCerrar");
        }
    }
}
