using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoverImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform imageTransform;
    private Vector3 originalScale;

    void Start()
    {
        imageTransform = GetComponent<RectTransform>();
        originalScale = imageTransform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        imageTransform.localScale = new Vector3(1.2f, 1.2f, 1); // Aumenta el tamaño
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        imageTransform.localScale = originalScale; // Vuelve al tamaño original
    }
}