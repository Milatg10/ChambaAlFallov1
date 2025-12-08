using UnityEngine;
using TMPro;

public class ContadorItems : MonoBehaviour
{
    [Header("Configuración")]
    public TextMeshProUGUI textoUI;
    public int totalAEncontrar = 3;
    
    public ContadorData datosContador; // Tu variable global
    private bool NPC = false;
    public int cantidadActual 
    {
        get { return datosContador != null ? datosContador.cantidadObjetos : 0; }
    }
    // ----------------------------

    private int ultimoValorConocido = -1; 

    void Start()
    {
        if(datosContador != null) 
            ultimoValorConocido = datosContador.cantidadObjetos;
            
        ActualizarMarcador();
    }

    void Update()
    {
        // Si el dato global cambia (porque lo tocó un compañero), actualizamos la UI
        if (datosContador != null && datosContador.cantidadObjetos != ultimoValorConocido)
        {
            ultimoValorConocido = datosContador.cantidadObjetos;
            VerificarEventos(); 
            ActualizarMarcador();
        }
    }

    public void SumarObjeto()
    {   
        if (NPC) return;

        if (datosContador != null)
        {
            datosContador.cantidadObjetos++;
            
            if (datosContador.cantidadObjetos > totalAEncontrar) 
                datosContador.cantidadObjetos = totalAEncontrar;
        }
    }

    void VerificarEventos()
    {
        if(datosContador.cantidadObjetos >= 1) NPC = true;

        if (datosContador.cantidadObjetos == totalAEncontrar)
        {
            Debug.Log("¡TODOS ENCONTRADOS!");
        }
    }

    void ActualizarMarcador()
    {
        if (datosContador != null)
            textoUI.text = datosContador.cantidadObjetos + " / " + totalAEncontrar;
    }
}