using UnityEngine;

public class LanzadorBombas : MonoBehaviour
{
    [Header("Configuración")]
    public GameObject prefabBomba;
    public Transform puntoLanzamiento;
    public float tiempoEntreBombas = 10f;

    [Header("Lanzamiento")]
    public float fuerzaHorizontal = 8f;
    public float fuerzaVertical = 5f;

    private float temporizador;

    void Update()
    {
        temporizador += Time.deltaTime;

        if (temporizador >= tiempoEntreBombas)
        {
            LanzarBomba();
            temporizador = 0f;
        }
    }

    void LanzarBomba()
    {
        GameObject bomba = Instantiate(prefabBomba, puntoLanzamiento.position, Quaternion.identity);

        Rigidbody2D rb = bomba.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.linearVelocity = new Vector2(fuerzaHorizontal, fuerzaVertical);
        }
    }
}