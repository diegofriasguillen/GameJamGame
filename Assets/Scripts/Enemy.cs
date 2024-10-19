using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform[] waypoints; // Puntos en el mapa
    public Transform jugador; // Referencia al jugador
    public float rangoPersecucion = 10f; // Rango en el que el enemigo comenzará a perseguir al jugador
    public float rangoBusqueda = 15f; // Rango en el que el enemigo comenzará a buscar al jugador
    public float tiempoEntreCambio = 5f; // Tiempo entre cambios de comportamiento (perseguir al jugador o ir a waypoints)

    private NavMeshAgent agente;
    private int siguienteWaypoint = 0;
    private bool persiguiendoJugador = false;
    private float tiempoCambio;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        tiempoCambio = tiempoEntreCambio;
        CambiarComportamiento();
    }

    void Update()
    {
        float distanciaJugador = Vector3.Distance(jugador.position, transform.position);

        // Si el jugador está en rango de búsqueda, busca al jugador
        if (distanciaJugador <= rangoBusqueda)
        {
            persiguiendoJugador = true;
        }

        if (persiguiendoJugador)
        {
            // Si el jugador está en rango de persecución, persigue al jugador
            if (distanciaJugador <= rangoPersecucion)
            {
                PerseguirJugador();
            }
            else
            {
                // Si el jugador no está dentro del rango de persecución, vuelve a los waypoints
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
}