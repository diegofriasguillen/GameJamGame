using UnityEngine;

public class CambiarCamara : MonoBehaviour
{
    public Camera camara1;
    public Camera camara2;
    public GameObject almaCuerpo;
    public SleepSystem sleepSystem;
    void Start()
    {
        // Al inicio, activamos la cámara 1 y desactivamos la cámara 2
        camara1.enabled = true;
        camara2.enabled = false;
    }

    void Update()
    {
        // cuando duerme

        if (sleepSystem.isSleeping == false)
        {
            almaCuerpo.gameObject.transform.position = gameObject.transform.position;
            gameObject.SetActive(false);
        }


        if (sleepSystem.isSleeping == true) 
        {
          
        }
        // Si se presiona la tecla "R", intercambiamos entre las cámaras
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (true)
            {

            }
            camara1.enabled = !camara1.enabled;
            camara2.enabled = !camara2.enabled;
        }
    }
}