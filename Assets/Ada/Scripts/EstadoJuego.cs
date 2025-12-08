using UnityEngine;

public class EstadoJuego : MonoBehaviour
{
    public static bool puzzle1Resuelto = false;
    public static bool puzzle2Resuelto = false;
    public static bool hayPosicionGuardada = false; // ¿Debemos recolocar al jugador?
    public static Vector3 posicionAlVolver;         // ¿A qué coordenadas?
}