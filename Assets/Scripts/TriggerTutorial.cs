using UnityEngine;

public class TriggerTutorial : MonoBehaviour
{
    [Header("Placa de este objeto")]
    public Sprite fotoDelCartel; 

    // Al NO ser static, cada objeto controla su cartel por separado
    private bool yaSeMostro = false; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !yaSeMostro)
        {
            if (fotoDelCartel != null && GestorVidas.instancia != null)
            {
                yaSeMostro = true;
                GestorVidas.instancia.MostrarTutorial(fotoDelCartel);
            }
        }
    }
}