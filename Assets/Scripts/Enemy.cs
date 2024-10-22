using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("References")]
    public Transform[] waypoints;
    public Transform jugador;
    private NavMeshAgent agente;
    private SphereCollider detectionZone;

    [Header("Behavior Parameters")]
    [Range(0, 100)] public int miedo = 0;
    public float tiempoEntreCambio = 5f;
    public float tiempoRecuperacionBusqueda = 10f;
    public float tiempoPersecucion = 10f;

    // Estado del enemigo
    private int siguienteWaypoint = 0;
    private bool persiguiendoJugador = false;
    private bool enBusqueda = false;
    private float tiempoCambio;
    private bool jugadorEnZona = false;

    private void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        if (agente == null)
        {
            Debug.LogError("NavMeshAgent no encontrado en " + gameObject.name);
            enabled = false;
            return;
        }

        detectionZone = gameObject.AddComponent<SphereCollider>();
        detectionZone.isTrigger = true;
        detectionZone.radius = 10f; // Puedes ajustar este valor desde el inspector

        tiempoCambio = tiempoEntreCambio;
        CambiarComportamiento();
    }

    private void Update()
    {
        if (!jugador)
        {
            Debug.LogWarning("Jugador no asignado en " + gameObject.name);
            return;
        }

        // Si el jugador está en la zona y no estamos en búsqueda, intentamos detectarlo
        if (jugadorEnZona && !enBusqueda)
        {
            StartCoroutine(ProcesarDeteccion());
        }

        // Si estamos persiguiendo pero el jugador sale de la zona, cambiamos a patrulla
        if (persiguiendoJugador && !jugadorEnZona)
        {
            CambiarAPatrulla();
        }

        // Actualizar tiempo de cambio de comportamiento
        ActualizarTiempoCambio();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que entró es el jugador
        if (other.transform == jugador)
        {
            jugadorEnZona = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Verificar si el objeto que salió es el jugador
        if (other.transform == jugador)
        {
            jugadorEnZona = false;
        }
    }

    private IEnumerator ProcesarDeteccion()
    {
        enBusqueda = true;

        int probabilidadBase = Random.Range(50, 150);
        int probabilidadTotal = probabilidadBase + miedo;

        if (probabilidadTotal >= 130 && probabilidadTotal <= 200)
        {
            if (Random.Range(0, 10) >= 8)
            {
                persiguiendoJugador = true;
                StartCoroutine(FinalizarPersecucion());
            }
        }
        else if (probabilidadTotal > 200)
        {
            if (Random.Range(0, 11) > 5)
            {
                persiguiendoJugador = true;
                StartCoroutine(FinalizarPersecucion());
            }
        }

        yield return new WaitForSeconds(tiempoRecuperacionBusqueda);
        enBusqueda = false;
    }

    private IEnumerator FinalizarPersecucion()
    {
        yield return new WaitForSeconds(tiempoPersecucion);
        if (!jugadorEnZona) // Solo terminamos la persecución si el jugador no está en la zona
        {
            persiguiendoJugador = false;
            CambiarAPatrulla();
        }
    }

    private void ActualizarTiempoCambio()
    {
        tiempoCambio -= Time.deltaTime;
        if (tiempoCambio <= 0f)
        {
            CambiarComportamiento();
            tiempoCambio = tiempoEntreCambio;
        }
    }

    private void CambiarComportamiento()
    {
        if (persiguiendoJugador && jugadorEnZona)
        {
            PerseguirJugador();
        }
        else
        {
            IrAlSiguienteWaypoint();
        }
    }

    private void CambiarAPatrulla()
    {
        persiguiendoJugador = false;
        IrAlSiguienteWaypoint();
    }

    private void PerseguirJugador()
    {
        agente.SetDestination(jugador.position);
    }

    private void IrAlSiguienteWaypoint()
    {
        if (waypoints == null || waypoints.Length == 0)
        {
            Debug.LogWarning("No hay waypoints asignados en " + gameObject.name);
            return;
        }

        if (waypoints[siguienteWaypoint] == null)
        {
            Debug.LogWarning("Waypoint " + siguienteWaypoint + " es null en " + gameObject.name);
            return;
        }

        agente.SetDestination(waypoints[siguienteWaypoint].position);
        siguienteWaypoint = (siguienteWaypoint + 1) % waypoints.Length;
    }

    private void OnDrawGizmosSelected()
    {
        // Dibujar zona de detección en el editor
        Gizmos.color = Color.yellow;
       // Gizmos.DrawWireSphere(transform.position, detectionZone?.radius ?? 10f);
    }
}