using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform[] waypoints; // Puntos en el mapa
    public Transform jugador; // Referencia al jugador
    public float rangoPersecucion = 10f; // Rango en el que el enemigo comenzar� a perseguir al jugador
    public float rangoBusqueda = 15f; // Rango en el que el enemigo comenzar� a buscar al jugador
    public float tiempoEntreCambio = 5f; // Tiempo entre cambios de comportamiento (perseguir al jugador o ir a waypoints)
    public int miedo=0;
    private NavMeshAgent agente;
    private int siguienteWaypoint = 0;
    private bool persiguiendoJugador = false;
    private float tiempoCambio;
    public bool busqueda=false;
    //public int random;
    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        tiempoCambio = tiempoEntreCambio;
        CambiarComportamiento();
    }

    void Update()
    {
        float distanciaJugador = Vector3.Distance(jugador.position, transform.position);
        

        // Si el jugador est� en rango de b�squeda, busca al jugador
        if (distanciaJugador <= rangoBusqueda && busqueda == false)
        {
            busqueda = true;
            int random = Random.Range(50, 150);
            if (random + miedo >=150  && random + miedo <= 250)
            {
                int random2 = Random.Range(0, 10);
                if (random2 >= 8) 
                {
                    persiguiendoJugador = true;
                }
            }
            else if (random + miedo >= 251)
            {
                int random2 = Random.Range(0, 10);
                if (random2 >= 8)
                {
                    persiguiendoJugador = true;
                }
            }
            ResetCD();
        }

        if (persiguiendoJugador)
        {
            // Si el jugador est� en rango de persecuci�n, persigue al jugador
            if (distanciaJugador <= rangoPersecucion)
            {
                PerseguirJugador();
            }
            else
            {
                // Si el jugador no est� dentro del rango de persecuci�n, vuelve a los waypoints
                CambiarComportamiento();
            }
        }

        tiempoCambio -= Time.deltaTime;

        if (tiempoCambio <= 0f)
        {
            CambiarComportamiento();
            tiempoCambio = tiempoEntreCambio;
        }
    }

    void CambiarComportamiento()
    {
        if (persiguiendoJugador)
        {
            // Ir hacia el jugador
            PerseguirJugador();
        }
        else
        {
            // Ir al siguiente waypoint
            IrAlSiguienteWaypoint();
        }
    }

    void PerseguirJugador()
    {
        agente.SetDestination(jugador.position);
    }

    void IrAlSiguienteWaypoint()
    {
        if (waypoints.Length == 0) return;

        agente.SetDestination(waypoints[siguienteWaypoint].position);   
        siguienteWaypoint = (siguienteWaypoint + 1) % waypoints.Length; // Ciclar entre los waypoints
    }

    IEnumerator ResetCD()

    {
      busqueda = true;
      yield return new WaitForSeconds(10f);
    }
    IEnumerator MonsterPersecucion() 
    
    {


        yield return new WaitForSeconds(10f);
    }
}