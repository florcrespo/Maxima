using UnityEngine;

public class Molino : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ControlMaxima control = other.GetComponent<ControlMaxima>();
            
            if (control != null)
            {
                // Activamos la bandera de que está en el molino
                control.estaEnMolino = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ControlMaxima control = other.GetComponent<ControlMaxima>();
            
            if (control != null)
            {
                // Al salir, desactivamos la bandera para que recupere su velocidad normal
                control.estaEnMolino = false;
            }
        }
    }
}