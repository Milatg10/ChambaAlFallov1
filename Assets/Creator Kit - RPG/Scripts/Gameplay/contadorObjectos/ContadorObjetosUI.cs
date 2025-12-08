using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ContadorObjetosUI : MonoBehaviour
{
    public MundoData mundo;               // ScriptableObject
    public TextMeshProUGUI textoContador; // Texto "0/3"

    void Update()
    {
        if (mundo == null || textoContador == null) return;

        // Actualizar texto
        textoContador.text = mundo.objetosRecogidos + " / " + mundo.objetosMaximos;

        // Si alcanza el máximo -> cambiar escena
        if (mundo.objetosRecogidos >= mundo.objetosMaximos)
        {
            CambiarAScenaFinal();
        }
    }

    void CambiarAScenaFinal()
    {
        SceneManager.LoadScene("FINAL");
    }
}
