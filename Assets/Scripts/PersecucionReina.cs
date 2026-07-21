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

        if (reinaComiendo != null)
            reinaComiendo.SetActive(false);
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
        if (other.CompareTag("AsadoLanzado"))
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

        rb.linearVelocity = Vector2.zero;

        AudioClip clipBocado = Resources.Load<AudioClip>("bocado");
        if (clipBocado != null)
            AudioSource.PlayClipAtPoint(clipBocado, transform.position);

        if (reinaComiendo != null)
        {
            SpriteRenderer srComiendo = reinaComiendo.GetComponent<SpriteRenderer>();
            if (srComiendo != null)
                srComiendo.flipX = spriteRenderer.flipX;

            reinaComiendo.SetActive(true);
        }

        if (spriteRenderer != null)
            spriteRenderer.enabled = false;

        if (animator != null)
            animator.enabled = false;

        yield return new WaitForSeconds(3f);

        if (reinaComiendo != null)
            reinaComiendo.SetActive(false);

        if (spriteRenderer != null)
            spriteRenderer.enabled = true;

        if (animator != null)
            animator.enabled = true;

        comiendo = false;
    }

    private System.Collections.IEnumerator TrabarseConDulce(GameObject dulce)
    {
        comiendo = true;

        rb.linearVelocity = Vector2.zero;

        yield return new WaitForSeconds(3f);

        comiendo = false;

        if (dulce != null)
            Destroy(dulce);
    }
}