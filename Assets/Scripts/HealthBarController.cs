using UnityEngine;
using UnityEngine.UI;  // Para poder usar la UI y manipular el Slider y la Imagen.

public class HealthBarController : MonoBehaviour
{
    public Slider healthBar;           // La referencia al Slider de la UI.
    public GameObject enemy;           // Referencia al enemigo.
    public float maxHealth = 100f;     // La cantidad máxima de vida.
    public float decreaseRate = 10f;   // La velocidad a la que disminuye la vida.
    public float recoveryRate = 5f;    // La velocidad de recuperación de la vida.
    public Image fadeImage;            // La referencia a la imagen que se mostrará progresivamente.

    private float currentHealth;       // Vida actual.
    private bool isDecreasing = false; // Flag para saber si estamos disminuyendo vida.
    private bool isDead = false;       // Flag para saber si la vida ha llegado a 0.
    private bool isEnemyVisible = false; // Para alternar visibilidad del enemigo.

    void Start()
    {
        // Inicializamos la vida actual al valor máximo.
        currentHealth = maxHealth;
        healthBar.value = currentHealth;

        // Inicializamos la imagen con transparencia total (invisible).
        SetImageAlpha(0f);  // La imagen comienza completamente invisible.
    }

    void Update()
    {
        // Detecta cuando se presiona el clic derecho del mouse solo si no está muerto.
        if (Input.GetMouseButtonDown(1) && !isDead)  // 1 es el botón derecho del mouse
        {
            isDecreasing = true;  // Inicia la disminución de vida.
            ToggleEnemyVisibility();  // Alterna la visibilidad del enemigo.
        }

        // Detecta cuando se suelta el clic derecho del mouse solo si no está muerto.
        if (Input.GetMouseButtonUp(1) && !isDead)  // 1 es el botón derecho del mouse
        {
            isDecreasing = false;  // Detiene la disminución de vida y comienza la recuperación.
            ToggleEnemyVisibility();  // Alterna la visibilidad del enemigo.
        }

        // Lógica para disminuir o recuperar la vida según el estado actual.
        if (isDecreasing && !isDead)
        {
            DecreaseHealth();
        }
        else if (!isDecreasing && !isDead)
        {
            RecoverHealth();
        }

        // Actualizamos el valor del Slider en la UI.
        healthBar.value = currentHealth;

        // Actualiza la transparencia de la imagen en función de la salud.
        UpdateImageTransparency();
    }

    void DecreaseHealth()
    {
        // Disminuimos la vida de manera constante hasta un mínimo de 0.
        if (currentHealth > 0)
        {
            currentHealth -= decreaseRate * Time.deltaTime;
        }
        else
        {
            currentHealth = 0;
            isDead = true;  // Marcamos que la vida ha llegado a 0.
        }
    }

    void RecoverHealth()
    {
        // Recuperamos la vida de manera progresiva hasta el máximo solo si no estamos "muertos".
        if (currentHealth < maxHealth)
        {
            currentHealth += recoveryRate * Time.deltaTime;
        }
        else
        {
            currentHealth = maxHealth;
        }
    }

    void ToggleEnemyVisibility()
    {
        // Alterna el estado de visibilidad del enemigo solo si la vida no está en 0.
        if (!isDead)
        {
            isEnemyVisible = !isEnemyVisible;
            SetEnemyVisibility(isEnemyVisible);
        }
    }

    void SetEnemyVisibility(bool visible)
    {
        // Si el enemigo tiene un componente Renderer (3D) o SpriteRenderer (2D), usamos esto para hacer visible/invisible.
        if (enemy.TryGetComponent<Renderer>(out Renderer enemyRenderer))
        {
            enemyRenderer.enabled = visible;
        }
        else if (enemy.TryGetComponent<SpriteRenderer>(out SpriteRenderer enemySpriteRenderer))
        {
            enemySpriteRenderer.enabled = visible;
        }
    }

    // Método para actualizar la transparencia de la imagen en función de la salud.
    void UpdateImageTransparency()
    {
        // Calculamos la opacidad en función de la vida actual.
        float alpha = 1f - (currentHealth / maxHealth);  // A menor vida, mayor transparencia.

        // Establecemos la nueva transparencia de la imagen.
        SetImageAlpha(alpha);
    }

    // Método para ajustar la opacidad (alfa) de la imagen.
    void SetImageAlpha(float alpha)
    {
        Color color = fadeImage.color;  // Obtenemos el color actual de la imagen.
        color.a = Mathf.Clamp01(alpha); // Ajustamos el valor alfa (transparencia), asegurando que esté entre 0 y 1.
        fadeImage.color = color;        // Asignamos el nuevo color a la imagen.
    }
}
