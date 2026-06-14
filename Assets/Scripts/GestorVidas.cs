using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para reiniciar el juego

public class GestorVidas : MonoBehaviour
{
    public int vidas = 3;
    public Vector3 puntoInicio; // Posición donde Máxima reaparece

    public void PerderVida(GameObject maxima)
    {
        vidas--;

        if (vidas > 0)
        {
            // REAPARECER DONDE MURIÓ:
            // Simplemente reiniciamos la velocidad y hacemos el efecto.
            // No cambiamos la posición (transform.position), así que se queda donde está.
            maxima.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            StartCoroutine(EfectoParpadeo(maxima.GetComponent<SpriteRenderer>()));
        }
        else
        {
            // REINICIO TOTAL (Solo al perder la última vida):
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void PerderBici(GameObject maxima)
    {
        ControlMaxima control = maxima.GetComponent<ControlMaxima>();
        control.estaEnBicicleta = false; // Máxima deja la bici
                                         // Opcional: podrías poner acá un efecto de sonido de "choque"
        Debug.Log("¡Perdiste la bici!");
    }

    System.Collections.IEnumerator EfectoParpadeo(SpriteRenderer sr)
    {
        ControlMaxima control = sr.GetComponent<ControlMaxima>();
        control.esInvencible = true; // Máxima se vuelve invencible

        for (int i = 0; i < 5; i++)
        {
            sr.enabled = false;
            yield return new WaitForSeconds(0.2f);
            sr.enabled = true;
            yield return new WaitForSeconds(0.2f);
        }

        control.esInvencible = false; // Vuelve a ser vulnerable
    }
}