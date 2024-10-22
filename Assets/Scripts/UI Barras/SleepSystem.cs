using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SleepSystem : MonoBehaviour, ISleepSystem
{
    public bool isSleeping;
    [SerializeField] int miedo;
    public int _Miedo { get => miedo;}
    [SerializeField] int maxMiedo = 150;
    [SerializeField] int sueno;
    public int _Sueno { get => sueno;}
    [SerializeField] int Maxsueno = 150;
    [SerializeField] float percentSlp;
    [SerializeField] UIController UI;
    [SerializeField] CambiarCamara camerach;

    [Header("Static stats")]
    float WSpeed = 3;
    float RSpeed = 4.5f;


    private void Start()
    {
        StartCoroutine(GameTick());
    }
    public void TickActualizarStats()
    {
        if (isSleeping)//Si esta durmiendo y hay un aumento solo puede ser miedo para despertar y reducimos el sue�o
        {
            SumMiedo(1);

            SumSueno(-1);
        }
        else //si hay un aumento y no esta durmiendo debe ser sue�o y se reduce el miedo
        {
            SumMiedo(-1);

            SumSueno(+1);

        }

    }

    public void Aumento(int a)
    {
        if (isSleeping) //Si esta durmiendo y hay un aumento solo puede ser miedo para despertar
        {
            SumMiedo(a);
        }
        else //si hay un aumento y no esta durmiendo debe ser sue�o
        {
            SumSueno(a);
        }
    }
    public void SumMiedo(int a)//Actualizar miedo y llamar cambios en UI
    {
        if (miedo > 0 && miedo < Maxsueno)//No restar por menos de 0 y no sumar arriba de Max
        {
            miedo += a;
        }
        else
        {
            Despertar();
        }
        
        float percent = (miedo * 100)/maxMiedo;

        UI.SetValueMiedo(percent);
    }

    private void SumSueno(int a)
    {
        sueno += a;
        percentSlp = (sueno * Maxsueno) / 100;
        Debug.Log(percentSlp);

        switch (percentSlp)
        {
            case float n when (n >= 25 && n <= 49):
                Debug.Log("Vel 75%");
                FPMovement.instance.SetSpeed(WSpeed*.75f,RSpeed*.75f);
                break;
            case float n when (n >= 50 && n < 74):
                Debug.Log("Vel 55%");
                FPMovement.instance.SetSpeed(WSpeed*.55f,RSpeed*.55f);
                break;
            case float n when (n >= 75 && n <= 100):
                Debug.Log("Vel 40%");
                FPMovement.instance.SetSpeed(WSpeed*.40f,RSpeed*.40f);
                break;
            default:
                Debug.Log("Vel 100%");
                FPMovement.instance.SetSpeed(WSpeed,RSpeed);
                break;
        }
        UI.SetValueSueno(percentSlp);
    }

    public void reduccion()
    {
        if (isSleeping)//Si hay una reduccion y esta durmiendo solo puede ser por caramelos y baja el miedo provocado por pesadillas
        {
            if (miedo > 0)
            {
                SumMiedo(-1);//Restamos miedo
            }
        }
        else//si hay una reduccion y no esta durmiendo solo puede ser por caramelo para dormir mejor
        {
            SumSueno(-1);
        }
    }

    public void reduccion(int r)
    {
        if (isSleeping)
        {
            if (miedo > 0)
            {
                SumMiedo(-r);//Restamos miedo (Caramelos)
            }
        }
        else
        {
            SumSueno(r);//sumamos sue�o
        }
    }
    
    public void Despertar()
    {
        //DespertarChanges
        Debug.Log("Despertar");
        isSleeping = false;

    }

    public void Dormir()
    {
        //DormirChanges
        Debug.Log("Dormido");
        isSleeping = true;

    }

    IEnumerator GameTick()
    {
        TickActualizarStats();
        //camerach.Aumentar(1);
        float a = Random.Range(0.2f, 2.0f);
        yield return new WaitForSeconds(a);
        StartCoroutine(GameTick());
    }

    
    
}
