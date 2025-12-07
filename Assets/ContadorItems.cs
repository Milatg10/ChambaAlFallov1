using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Necesario para modificar el texto

public class ContadorItems : MonoBehaviour
{
    [Header("Configuración")]
    public TextMeshProUGUI textoUI;   // Arrastra aquí el texto de la pantalla
    public int totalAEncontrar = 3;   // Cuántos hay en total (3)
    
    public int cantidadActual = 0;   // Empezamos en 0

    private bool NPC = false;

    void Start()
    {
        ActualizarMarcador(); // Para que ponga "0 / 3" al empezar
    }

    // Esta función la llamaremos cuando toques un objeto
    public void SumarObjeto()
    {   
        if (NPC) return;
        else
        {
            cantidadActual++; // Sumamos 1
        }
        
        // Que no se pase del tope
        if (cantidadActual > totalAEncontrar) 
            cantidadActual = totalAEncontrar;

        ActualizarMarcador();

        // ¿Ganaste?
        if (cantidadActual == totalAEncontrar)
        {
            Debug.Log("¡TODOS ENCONTRADOS!");
            // Aquí puedes activar una puerta, un sonido, etc.
        }

        if(cantidadActual == 1)
        {
            NPC = true;
        }
    }

    void ActualizarMarcador()
    {
        textoUI.text = cantidadActual + " / " + totalAEncontrar;
    }
}