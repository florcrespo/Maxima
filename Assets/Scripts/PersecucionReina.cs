using UnityEngine;

public class PersecucionReina : MonoBehaviour
{
    [Header("Configuración")]
    public Transform objetivo;
    public float velocidad = 3.5f;
    public float distanciaMinima = 1f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool comiendo = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (objetivo == null) return;

        if (comiendo)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

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
            rb.linearVelocity = new Vector2(direccion.x * velocidad, 0);
        else
            rb.linearVelocity = Vector2.zero;

        if (direccion.x > 0)
            spriteRenderer.flipX = false;
        else if (direccion.x < 0)
            spriteRenderer.flipX = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("La Reina tocó: " + other.gameObject.name);

        if (other.CompareTag("Asado"))
        {
            Debug.Log("La Reina encontró el asado");

            Destroy(other.gameObject);

            StartCoroutine(ComerAsado());
        }
    }

    private System.Collections.IEnumerator ComerAsado()
    {
        comiendo = true;

        yield return new WaitForSeconds(4f);

        comiendo = false;
    }
}