using UnityEngine;

public class PosicionarJugador : MonoBehaviour
{
    void Start()
    {
        // Al empezar la escena, preguntamos: ¿Vengo de un puzzle?
        if (EstadoJuego.hayPosicionGuardada)
        {
            // ¡Sí! Me muevo a la posición que me dijeron
            transform.position = EstadoJuego.posicionAlVolver;

            // Apagamos el aviso para que si reinicias el juego normal, empieces en el inicio
            EstadoJuego.hayPosicionGuardada = false;
        }
    }
}