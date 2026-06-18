using UnityEngine;
using UnityEngine.SceneManagement;

public class GestorVidas : MonoBehaviour
{
    public int vidas = 3;
    public Vector3 puntoInicio;
    public GameObject mensajeNivelCompletado;
    public GameObject reinaPerseguidora;

    public bool nivelCompletado = false;

    void Start()
    {
        if (mensajeNivelCompletado != null)
        {
            mensajeNivelCompletado.SetActive(false);
        }
    }

    public void NivelCompletado(GameObject maxima)
    {
        nivelCompletado = true;

        if (mensajeNivelCompletado != null)
        {
            mensajeNivelCompletado.SetActive(true);
        }

        if (reinaPerseguidora != null)
        {
            reinaPerseguidora.SetActive(false);
        }

        ControlMaxima control = maxima.GetComponent<ControlMaxima>();
        Animator anim = maxima.GetComponent<Animator>();
        Rigidbody2D rb = maxima.GetComponent<Rigidbody2D>();

        if (control != null)
        {
            control.estaEnBicicleta = false;
            control.enabled = false;
        }

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.simulated = false;
        }

        if (anim != null)
        {
            anim.speed = 1f;

            anim.SetFloat("velocidad", 0f);
            anim.SetBool("enBicicleta", false);
            anim.SetBool("isJumping", false);

            // Fuerza el cambio inmediato al idle
            anim.CrossFade("maxima_idle", 0f, 0);
        }
    }

    public void PerderVida(GameObject maxima)
    {
        vidas--;

        if (vidas > 0)
        {
            maxima.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            StartCoroutine(EfectoParpadeo(maxima.GetComponent<SpriteRenderer>()));
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void PerderBici(GameObject maxima)
    {
        maxima.GetComponent<ControlMaxima>().estaEnBicicleta = false;
    }

    System.Collections.IEnumerator EfectoParpadeo(SpriteRenderer sr)
    {
        ControlMaxima control = sr.GetComponent<ControlMaxima>();
        control.esInvencible = true;

        for (int i = 0; i < 5; i++)
        {
            sr.enabled = false;
            yield return new WaitForSeconds(0.2f);
            sr.enabled = true;
            yield return new WaitForSeconds(0.2f);
        }

        control.esInvencible = false;
    }
}