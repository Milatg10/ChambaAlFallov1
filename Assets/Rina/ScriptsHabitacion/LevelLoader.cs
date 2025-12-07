using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;              // Necesario para que entienda qué es un "Slider"
using UnityEngine.SceneManagement; // Necesario para manejar escenas

public class LevelLoader : MonoBehaviour
{
    public GameObject pantallaDeCarga; // El panel negro
    public Slider barraDeCarga;        // La barra (slider)

    public void CargarNivel(string nombreEscena)
    {
        StartCoroutine(CargarAsincronamente(nombreEscena));
    }

    IEnumerator CargarAsincronamente(string nombreEscena)
    {
        // 1. ENCENDER LA PANTALLA (Aquí es donde se activa sola)
        pantallaDeCarga.SetActive(true);

        // 2. Cargar la escena en segundo plano
        AsyncOperation operacion = SceneManager.LoadSceneAsync(nombreEscena);
        
        // Evita que cambie de golpe al terminar
        operacion.allowSceneActivation = false;

        while (!operacion.isDone)
        {
            // Mover la barra de carga
            float progreso = Mathf.Clamp01(operacion.progress / 0.9f);
            barraDeCarga.value = progreso;

            // Si ya cargó (llegó al 90%), esperamos un poquito para que se vea bonito
            if (operacion.progress >= 0.9f)
            {
                // Pausa falsa de 1 segundo
                yield return new WaitForSeconds(1f); 
                operacion.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
