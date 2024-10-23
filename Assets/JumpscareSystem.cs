using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class JumpscareSystem : MonoBehaviour
{
    [Header("Componentes")]
    [SerializeField] private Light pointLight;        // Referencia a la luz que parpadeará
    [SerializeField] private AudioSource scareSound;  // Sonido del susto
    [SerializeField] private GameObject monster;      // El monstruo que aparecerá
    [SerializeField] private Transform pointA;        // Punto inicial del monstruo
    [SerializeField] private Transform pointB;        // Punto final del monstruo

    [Header("Configuración")]
    [SerializeField] private float scareDuration = 1.5f;     // Duración total del susto
    [SerializeField] private float flashSpeed = 0.05f;       // Velocidad del parpadeo
    [SerializeField] private float monsterSpeed = 10f;       // Velocidad del monstruo
  //  public Parpadeo parpadeo;

    private NavMeshAgent monsterAgent;
    public BoxCollider activador;
    private bool isScareActive = false;
    private void Awake()
    {
       

    }
    private void Start()
    {

        // Aseguramos que el monstruo esté desactivado al inicio
        monster.SetActive(false);

        // Configuramos el NavMeshAgent del monstruo
        monsterAgent = monster.GetComponent<NavMeshAgent>();
        activador = gameObject.GetComponent<BoxCollider>();
        if (monsterAgent != null)
        {
            monsterAgent.speed = monsterSpeed;
            monsterAgent.acceleration = monsterSpeed * 2;
        }     

    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificamos si es el jugador y el susto no está activo
        if (other.CompareTag("Player") && !isScareActive)
        {
            activador.enabled = false;
            monster.gameObject.SetActive(true);
            ActivateJumpscare();
        }
    }

    private void ActivateJumpscare()
    {
       
        isScareActive = true;
        StartCoroutine(FlashLightCoroutine());
        StartCoroutine(MonsterMovementCoroutine());

        // Reproducir el sonido
        if (scareSound != null)
        {
            scareSound.Play();
        }

    }

    private IEnumerator FlashLightCoroutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < scareDuration)
        {
            // Alternamos el estado de la luz
            pointLight.enabled = !pointLight.enabled;

            yield return new WaitForSeconds(flashSpeed);
            elapsedTime += flashSpeed;
        }

        // Aseguramos que la luz quede apagada al final
        pointLight.enabled = false;
    }

    private IEnumerator MonsterMovementCoroutine()
    {
        // Posicionamos y activamos el monstruo
        monster.transform.position = pointA.position;
        monster.SetActive(true);

        if (monsterAgent != null)
        {
            // Movemos el monstruo usando NavMesh
            monsterAgent.SetDestination(pointB.position);

            // Esperamos hasta que llegue al destino o pase el tiempo del susto
            float elapsedTime = 0f;
            while (elapsedTime < scareDuration)
            {
                if (!monsterAgent.pathPending && monsterAgent.remainingDistance < 0.1f)
                    break;

                yield return null;
                elapsedTime += Time.deltaTime;
            }
        }
        else
        {
            // Movimiento directo si no hay NavMeshAgent
            float elapsedTime = 0f;
            Vector3 startPos = pointA.position;
            Vector3 endPos = pointB.position;

            while (elapsedTime < scareDuration)
            {
                float t = elapsedTime / scareDuration;
                monster.transform.position = Vector3.Lerp(startPos, endPos, t);
                monster.transform.LookAt(endPos);

                yield return null;
                elapsedTime += Time.deltaTime;
            }
        }

        // Desactivamos el monstruo y reseteamos el sistema
        monster.SetActive(false);
        isScareActive = false;

    }
}