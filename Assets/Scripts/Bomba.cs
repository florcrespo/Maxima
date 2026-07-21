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

        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            exploto = true;

            // Crear la explosión
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            // Destruir la bomba
            Destroy(gameObject);
        }
    }
}