using UnityEngine;
public class DulceLanzado : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool yaCayo = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (yaCayo) return;
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            yaCayo = true;
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
        if (collision.gameObject.CompareTag("Reina"))
        {
            // la reina se traba — tu compa agrega esto en el script de la reina
            Destroy(gameObject);
        }
    }
}