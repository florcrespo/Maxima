using UnityEngine;

public class InventarioManager : MonoBehaviour
{
    public static InventarioManager instancia;

    public GameObject iconoAsado;

    private void Awake()
    {
        instancia = this;
    }

    public void AgregarAsado()
    {
        iconoAsado.SetActive(true);
    }
}