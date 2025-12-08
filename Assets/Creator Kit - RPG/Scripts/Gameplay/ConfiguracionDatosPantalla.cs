using UnityEngine;

public class ConfiguracionDatosPantalla : MonoBehaviour
{
    public MundoData mundoData;
    public DificultadData dificultad;

    void Awake()
    {
        Configurar();
    }

    void Configurar()
    {
        switch (dificultad.dificultadActual)
        {
            case 1:  // Perfecto
                mundoData.tiempoInicio = 18 * 60;   // 18:00
                mundoData.tiempoLimite = 24 * 60;   // 00:00
                mundoData.objetosMaximos = 3;
                break;

            case 2:  // Complicado
                mundoData.tiempoInicio = 19 * 60;   // 19:00
                mundoData.tiempoLimite = 24 * 60;   // 00:00
                mundoData.objetosMaximos = 3;
                break;

            case 3:  // Malo
                mundoData.tiempoInicio = 20 * 60;   // 21:00
                mundoData.tiempoLimite = 24 * 60;   // 00:00
                mundoData.objetosMaximos = 3;
                break;
        }

        // Reset global por seguridad:
        mundoData.objetosRecogidos = 0;
        mundoData.vidaActual = mundoData.vidaMaxima;
    }
}
