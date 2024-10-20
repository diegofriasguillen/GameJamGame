using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using TMPro;

public class CambiarCamara : MonoBehaviour
{
    public Camera camara1;
    public Camera camara2;
    public GameObject almaCuerpo;
    public SleepSystem sleepSystem;
    public GameObject textoCuerpo;
    public GameObject luzNocturna;
    public bool colisionConCuerpo=false;
   
    void Start()
    {
          
        camara2.enabled = false;
        almaCuerpo.SetActive(false);
        textoCuerpo.SetActive (false);
        //RenderSettings.ambientIntensity = 1f;
        colisionConCuerpo = false;
        luzNocturna.SetActive(false);
    }




    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.Space) && colisionConCuerpo == true)
        {
            sleepSystem.isSleeping = false;
            gameObject.transform.position = almaCuerpo.transform.position;
            gameObject.transform.rotation = almaCuerpo.transform.rotation;
        }


        if (sleepSystem.isSleeping == false)
        {
            almaCuerpo.gameObject.transform.position = gameObject.transform.position;
            almaCuerpo.gameObject.SetActive(false);
            luzNocturna.SetActive(false);
            textoCuerpo.gameObject.SetActive(false);

        }


        if (sleepSystem.isSleeping == true) 
        {
            almaCuerpo.gameObject.SetActive(true);
            luzNocturna .SetActive(true); 
        }



        if (Input.GetKeyDown(KeyCode.R))
        {
            if (sleepSystem.isSleeping == true)
            {
                camara1.enabled = false;
                camara2.enabled = true;
          
            }

        }
        else if (Input.GetKeyUp(KeyCode.R))
        {

            if (sleepSystem.isSleeping == true)
            {
                camara1.enabled = true;
                camara2.enabled = false;
            }

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (sleepSystem.isSleeping == false)
            {
                sleepSystem.isSleeping =true;
            }
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Alma") && sleepSystem.isSleeping==true)
        {
            textoCuerpo.gameObject.SetActive(true);
            colisionConCuerpo = true;
        }
        else
        {
            textoCuerpo.gameObject.SetActive(false);
            colisionConCuerpo = false;
        }

    }
}