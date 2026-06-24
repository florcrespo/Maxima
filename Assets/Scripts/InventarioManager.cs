using UnityEngine;

public class InventarioManager : MonoBehaviour
{
    public static InventarioManager instancia;

    [Header("Iconos")]
    public GameObject iconoAsado;
    public GameObject iconoDulce;

    [Header("Prefabs")]
    public GameObject prefabAsadoLanzado;
    public GameObject prefabDulceLanzado;

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

        AudioClip clip = Resources.Load<AudioClip>("lanzar objeto");
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, posicionMaxima);
        }

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

    public void UsarDulce(Vector3 posicionMaxima)
    {
        if (!TieneDulce()) return;

        AudioClip clip = Resources.Load<AudioClip>("lanzar objeto");
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, posicionMaxima);
        }

        iconoDulce.SetActive(false);

        Instantiate(
            prefabDulceLanzado,
            posicionMaxima,
            Quaternion.identity
        );
    }
}