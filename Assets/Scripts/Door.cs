using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator animator; 
    private bool playerInRange = false; 

    private void Update()
    {
        // Abre la puerta si el jugador está en rango y presiona la tecla "F"
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            bool isOpen = animator.GetBool("IsOpen");
            animator.SetBool("IsOpen", !isOpen);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra es el jugador
        if (other.CompareTag("Player")) // Asegúrate de que el jugador tenga la etiqueta "Player"
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Verifica si el objeto que sale es el jugador
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
