using UnityEngine;

public class InventarioManager : MonoBehaviour
{
    public static InventarioManager instancia;

    [Header("Iconos")]
    public GameObject iconoAsado;
    public GameObject iconoDulce;

    [Header("Prefabs")]
    public GameObject prefabAsadoLanzado;

    private void Awake()
    {
        instancia = this;
    }

    // -------------------
    // ASADO
    // -------------------

    public void AgregarAsado()
    {
        iconoAsado.SetActive(true);
    }

    public bool TieneAsado()
    {
        return iconoAsado.activeSelf;
    }

    public void UsarAsado(Vector3 posicionMaxima)
    {
        if (!TieneAsado()) return;

        iconoAsado.SetActive(false);

        Instantiate(
            prefabAsadoLanzado,
            posicionMaxima,
            Quaternion.identity
        );
    }

    // -------------------
    // DULCE DE LECHE
    // -------------------

    public void AgregarDulce()
    {
        iconoDulce.SetActive(true);
    }

    public bool TieneDulce()
    {
        return iconoDulce.activeSelf;
    }
}