using UnityEngine;

public class DesbloqueoCamino : MonoBehaviour
{
    [Header("Configuración")]
    public ContadorItems scriptContador; 
    public int objetosNecesarios = 2;    

    // TU BOOLEANO DE CONTROL (Privado, porque solo lo usa este script)
    private bool paredAbierta = false; 

    void Update()
    {
        // 1. EL FILTRO: Si ya está abierta, no leemos nada más. ¡Adiós!
        if (paredAbierta == true) return;

        // Protección de seguridad
        if (scriptContador == null) return;

        // 2. LA COMPROBACIÓN
        if (scriptContador.cantidadActual >= objetosNecesarios)
        {
            AbrirPared();
        }
    }

    void AbrirPared()
    {
        Debug.Log("¡Condición cumplida! Abriendo camino...");

        // 3. CAMBIAMOS EL BOOLEANO: "Ya he cumplido mi misión"
        paredAbierta = true; 

        // 4. ACCIÓN: Desactivamos la pared
        gameObject.SetActive(false);
    }
}