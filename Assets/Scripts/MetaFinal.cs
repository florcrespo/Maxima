using UnityEngine;
using Unity.Cinemachine;

public class MetaFinal : MonoBehaviour
{
    public GameObject maxima;
    public GameObject maximaConZuecos;
    public GameObject reinaPerseguidora;
    public CinemachineCamera camara;
    public GameObject textoNivelCompletado;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == maxima || other.gameObject == maximaConZuecos)
        {
            GameObject maximaActual = other.gameObject;

            ControlMaxima control = maximaActual.GetComponent<ControlMaxima>();
            if (control != null)
            {
                control.estaEnBicicleta = false;
                control.enabled = false;
            }

            Rigidbody2D rb = maximaActual.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }

            Animator animator = maximaActual.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetBool("enBicicleta", false);
                animator.SetBool("isJumping", false);
                animator.SetFloat("velocidad", 0f);
                animator.speed = 1f;
            }

            if (reinaPerseguidora != null)
            {
                reinaPerseguidora.SetActive(false);
            }

            if (textoNivelCompletado != null)
            {
                textoNivelCompletado.SetActive(true);
            }

            if (camara != null)
            {
                camara.Target.TrackingTarget = null;
            }
        }
    }
}