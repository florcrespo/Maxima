using UnityEngine;
using System.Collections;
using Unity.Cinemachine;

public class PowerUpZuecos : MonoBehaviour
{
    public GameObject maximaNormal;
    public GameObject maximaConZuecos;
    public CinemachineCamera camara;

    public float tiempoCambio = 0.5f;
    public float duracionZuecos = 5f;

    private SpriteRenderer spriteRenderer;
    private Collider2D colliderZuecos;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        colliderZuecos = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == maximaNormal)
        {
            StartCoroutine(ActivarZuecos());
        }
    }

    private IEnumerator ActivarZuecos()
    {
        if (spriteRenderer != null) spriteRenderer.enabled = false;
        if (colliderZuecos != null) colliderZuecos.enabled = false;

        yield return new WaitForSeconds(tiempoCambio);

        Vector3 posicionActual = maximaNormal.transform.position;

        maximaNormal.SetActive(false);
        maximaConZuecos.transform.position = posicionActual;
        maximaConZuecos.SetActive(true);

        if (camara != null)
        {
            camara.Target.TrackingTarget = maximaConZuecos.transform;
        }

        yield return new WaitForSeconds(duracionZuecos);

        posicionActual = maximaConZuecos.transform.position;

        maximaConZuecos.SetActive(false);
        maximaNormal.transform.position = posicionActual;
        maximaNormal.SetActive(true);

        if (camara != null)
        {
            camara.Target.TrackingTarget = maximaNormal.transform;
        }

        Destroy(gameObject);
    }
}