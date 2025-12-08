using UnityEngine;
using UnityEngine.EventSystems;

public class Boton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RectTransform rectTransform;
    public float normalHeight = 50f;
    public float hoverHeight = 70f;
    public float speed = 10f;

    private bool hovering = false;

    private void Update()
    {
        float targetHeight = hovering ? hoverHeight : normalHeight;

        Vector2 size = rectTransform.sizeDelta;
        size.y = Mathf.Lerp(size.y, targetHeight, Time.deltaTime * speed);
        rectTransform.sizeDelta = size;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovering = false;
    }
}
