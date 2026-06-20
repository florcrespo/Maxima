using UnityEngine;

public class PersecucionReina : MonoBehaviour
{
    [Header("Configuración")]
    public Transform objetivo; // Acá vamos a arrastrar a Máxima
    public float velocidad = 3.5f;
    public float distanciaMinima = 1f; // Distancia que mantiene con Máxima

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (objetivo == null) return;

        // Si Máxima murió, la Reina deja de moverse
        ControlMaxima control = objetivo.GetComponent<ControlMaxima>();
        if (control != null && !control.enabled)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 direccion = (objetivo.position - transform.position).normalized;
        direccion.y = 0;

        float distancia = Vector2.Distance(transform.position, objetivo.position);

        if (distancia > distanciaMinima)
        {
            rb.linearVelocity = new Vector2(direccion.x * velocidad, 0);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }

        if (direccion.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (direccion.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }
}