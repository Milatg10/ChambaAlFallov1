using UnityEngine;

[CreateAssetMenu(fileName = "MundoData", menuName = "Datos/MundoData")]
public class MundoData : ScriptableObject
{
    [Header("VIDA")]
    public float vidaMaxima = 150f;
    public float vidaActual = 150f;

    [Header("OBJETOS A RECOGER")]
    public int objetosRecogidos = 0;
    public int objetosMaximos = 3;

    [Header("TIEMPO (en minutos)")]
    public int tiempoInicio = 18 * 60;   // 18:00
    public int tiempoLimite = 24 * 60;   // 24:00 = fin del día
}
