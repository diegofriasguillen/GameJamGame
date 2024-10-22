using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using TMPro;
using System.Collections;

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
    public AudioSource ambiente;
    public float tempCam;
    public float contador;

    void Start()
    {
        tempCam = 10;
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
       
        if (tempCam <10 && cameraFollowCursor.view == false)
        {
            tempCam = tempCam+Time.deltaTime;
        }
        if (cameraFollowCursor.view == true)
        {
            tempCam = tempCam - Time.deltaTime;
        }

        if (sleepSystem.isSleeping == true && ambiente.pitch<3f)
        {
            ambiente.pitch = ambiente.pitch + 0.01f;
        }

        if (sleepSystem.isSleeping == false && ambiente.pitch >1.1f)
        {
            ambiente.pitch = ambiente.pitch - 0.01f;
        }

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
            if (sleepSystem.isSleeping == true )
            {
                contador = 0;
                camara1.enabled = false;
                camara2.enabled = true;
                cameraFollowCursor.view = true;              
            }
            else if (tempCam < 0)
            {
                camara1.enabled = true;
                camara2.enabled = false;
                cameraFollowCursor.view = false;
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
            else if (true)
            {

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

    private IEnumerator ProcesarDeteccion()
    {
        if (true)
        {

        }
        yield return new WaitForSeconds(1f);       
    }
}