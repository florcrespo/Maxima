using UnityEngine;

public class InventarioManager : MonoBehaviour
{
    public static InventarioManager instancia;

    public GameObject iconoAsado;
    public GameObject prefabAsadoLanzado;

    private void Awake()
    {
        instancia = this;
    }

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
}