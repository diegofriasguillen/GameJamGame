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
    public GameObject manosVivas;
    public GameObject manosFantasma;
    public GameObject Vela;
    public bool colisionConCuerpo=false;
    public CameraFollowCursor cameraFollowCursor;
   
    void Start()
    {
        manosFantasma.gameObject.SetActive(false);
        camara2.enabled = false;
        almaCuerpo.SetActive(false);
        textoCuerpo.SetActive (false);
        RenderSettings.ambientIntensity = 0.06f;
        colisionConCuerpo = false;
        luzNocturna.SetActive(false);
    }




    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(colisionConCuerpo == true && sleepSystem.isSleeping==true) 
            {
                sleepSystem.isSleeping = false;
                gameObject.transform.position = almaCuerpo.transform.position;
                gameObject.transform.rotation = almaCuerpo.transform.rotation;
            }
            else if (sleepSystem.isSleeping == false)
            {
                sleepSystem.isSleeping = true;

            }

        }
        


        if (sleepSystem.isSleeping == false)
        {
            almaCuerpo.gameObject.transform.position = gameObject.transform.position;
            almaCuerpo.gameObject.SetActive(false);
            luzNocturna.SetActive(false);
            textoCuerpo.gameObject.SetActive(false);
            cameraFollowCursor.view = false;
            manosFantasma.SetActive(false);
            manosVivas.SetActive(true);
            Vela.SetActive(true);
        }


        if (sleepSystem.isSleeping == true) 
        {
            almaCuerpo.gameObject.SetActive(true);
            luzNocturna .SetActive(true);
            manosFantasma.SetActive(true);
            manosVivas.SetActive(false);
            Vela.SetActive(false);
        }



        if (Input.GetKeyDown(KeyCode.R))
        {
            if (sleepSystem.isSleeping == true)
            {
                camara1.enabled = false;
                camara2.enabled = true;
                cameraFollowCursor.view = true;
          
            }

        }
        else if (Input.GetKeyUp(KeyCode.R))
        {

            if (sleepSystem.isSleeping == true)
            {
                camara1.enabled = true;
                camara2.enabled = false;
                cameraFollowCursor.view = false;

            }

        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Alma") && sleepSystem.isSleeping==true)
        {
            textoCuerpo.gameObject.SetActive(true);
            colisionConCuerpo = true;
        }
    

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Alma") && sleepSystem.isSleeping == true)
        {
            textoCuerpo.gameObject.SetActive(false);
            colisionConCuerpo = false;
        }
    }
}