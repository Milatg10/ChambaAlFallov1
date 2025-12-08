using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugVida : MonoBehaviour
{
    public int vida = 3;
    public float daño = 10f; // Cuánto quita al tocar

    [Header("Efectos de Sonido")]
    public AudioClip sonidoGolpe;    // Sonido "Plop" al darle
    public AudioClip sonidoMuerte;   // Sonido "Explosión/Desinflado" al morir
    [Range(0f, 1f)] public float volumenMuerte = 1f;  // Volumen de la muerte

    private AudioSource audioBug; // El altavoz del bug
    private Animator miAnimator;
    private bool estaMuerto = false;
    private Color colorOriginal; // Aquí guardaremos si es verde, amarillo, etc.

    void Start()
    {
        miAnimator = GetComponent<Animator>();
        colorOriginal = GetComponent<SpriteRenderer>().color;
        // Añadimos altavoz al bug automáticamente si no tiene
        audioBug = GetComponent<AudioSource>();
        if (audioBug == null) audioBug = gameObject.AddComponent<AudioSource>();
    }

    // Para recibir daño de las balas (Trigger)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (estaMuerto) return;

        if (collision.CompareTag("Weapon"))
        {
            RecibirDañoPublico();
            // Destruimos la bala para que no atraviese
            Destroy(collision.gameObject);
        }
    }

    // Para probar con clic
    private void OnMouseDown()
    {
        if (estaMuerto) return;
        RecibirDañoPublico();
    }

    public void RecibirDañoPublico()
    {
        if (estaMuerto) return;
        vida--;
        GetComponent<SpriteRenderer>().color = Color.red;
        Invoke("RestaurarColor", 0.1f);

        // --- SONIDO DE GOLPE ---
        if (audioBug != null && sonidoGolpe != null)
        {
            audioBug.PlayOneShot(sonidoGolpe);
        }

        if (vida <= 0)
        {
            estaMuerto = true;          
            audioBug.PlayOneShot(sonidoMuerte, volumenMuerte);
            if (GameManagerMila.instance != null)
                {
                    GameManagerMila.instance.RegistrarMuerteBug();
                }
            StartCoroutine(MorirConEstilo());
        }
    }

    void RestaurarColor()
    {
        if(!estaMuerto) GetComponent<SpriteRenderer>().color = colorOriginal;
    }

    IEnumerator MorirConEstilo()
    {
        miAnimator.Play("Bug_die");

        if (TryGetComponent<BugMovimiento>(out BugMovimiento movimiento))
        {
            movimiento.enabled = false;
        }
        if (TryGetComponent<BugDisparoAbanico>(out BugDisparoAbanico escopeta))
        {
            escopeta.enabled = false; // ¡Apagado! Ya no dispara más.
        }
        if( TryGetComponent<BugDisparo>(out BugDisparo disparo))
        {
            disparo.enabled = false; // ¡Apagado! Ya no dispara más.
        }
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false; // Para que no estorbe

        yield return new WaitForSeconds(0.8f);
        Destroy(gameObject);
    }
}