using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSystemCamera : MonoBehaviour
{
    public Transform cameraTransform;      // La cámara que quieres mover
    public Transform targetPosition;       // El punto al que la cámara debe ir
    public float speed = 2f;               // Velocidad del zoom
    public int sceneToLoad = 1;            // Índice de la escena a cargar

    public void Jugar()
    {
        StartCoroutine(MoverCamara());
    }

    public void Salir()
    {
        Debug.Log("SALIENDO DEL JUEGO...");
        Application.Quit();
    }

    private IEnumerator MoverCamara()
    {
        Vector3 startPos = cameraTransform.position;
        Quaternion startRot = cameraTransform.rotation;

        Vector3 endPos = targetPosition.position;
        Quaternion endRot = targetPosition.rotation;

        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * speed;

            cameraTransform.position = Vector3.Lerp(startPos, endPos, t);
            cameraTransform.rotation = Quaternion.Slerp(startRot, endRot, t);

            yield return null;
        }

        // ⬇️ Cuando termina el zoom, cambiamos de escena
        SceneManager.LoadScene(sceneToLoad);
    }
}
