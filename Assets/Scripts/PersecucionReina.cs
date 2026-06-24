using UnityEngine;

public class PersecucionReina : MonoBehaviour
{
    [Header("Configuración")]
    public Transform objetivo;
    public float velocidad = 3.5f;
    public float distanciaMinima = 1f;

    [Header("Animaciones")]
    public GameObject reinaComiendo;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool comiendo = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
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
        if (other.CompareTag("Asado"))
        {
            Destroy(other.gameObject);
            StartCoroutine(ComerAsado());
        }
       if (other.CompareTag("DulceLanzado"))
        {
            StartCoroutine(TrabarseConDulce(other.gameObject));
        }
    }

    private System.Collections.IEnumerator ComerAsado()
    {
        comiendo = true;

        // Reproducimos el sonido "bocado" al asimilar el asado
        AudioClip clipBocado = Resources.Load<AudioClip>("bocado");
        if (clipBocado != null)
        {
            AudioSource.PlayClipAtPoint(clipBocado, transform.position);
        }

        // Aparece la animación de comer donde está la Reina
        if (reinaComiendo != null)
        {
            reinaComiendo.transform.position = transform.position;
            reinaComiendo.SetActive(true);
        }

        // Oculta visualmente la Reina en bici
        if (spriteRenderer != null)
            spriteRenderer.enabled = false;

        if (animator != null)
            animator.enabled = false;

        yield return new WaitForSeconds(4f);

        // Oculta la animación de comer
        if (reinaComiendo != null)
            reinaComiendo.SetActive(false);

        // Vuelve la Reina en bici
        if (spriteRenderer != null)
            spriteRenderer.enabled = true;

        if (animator != null)
            animator.enabled = true;

        comiendo = false;
    }
    
    private System.Collections.IEnumerator TrabarseConDulce(GameObject dulce)
    {
        comiendo = true;
        yield return new WaitForSeconds(3f);
        comiendo = false;
        if (dulce != null) Destroy(dulce);
    }
}