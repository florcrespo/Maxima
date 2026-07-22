using UnityEngine;

public class Bomba : MonoBehaviour
{
    [Header("Explosión")]
    public GameObject explosionPrefab;

    private bool exploto = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (exploto)
            return;

        // Si toca a Máxima, explota inmediatamente
        if (collision.gameObject.CompareTag("Player"))
        {
            exploto = true;

            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject);
            return;
        }

        // Si toca el piso, también explota
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            exploto = true;

            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}