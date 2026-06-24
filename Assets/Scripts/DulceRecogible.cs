using UnityEngine;

public class DulceRecogible : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            InventarioManager.instancia.AgregarDulce();

            Destroy(gameObject);
        }
    }
}