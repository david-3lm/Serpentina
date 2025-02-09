using System;
using UnityEngine;
using UnityEngine.UI;

public class OnHoverButton : MonoBehaviour
{
    private Image image;
    private Vector3 ogSize;

    [SerializeField] private Button button;

    void Start()
    {
        image = GetComponent<Image>(); // Obtiene la imagen del objeto
        ogSize = transform.localScale; // Guarda el color original
    }

    private void OnMouseOver()
    {
        Debug.Log("Mouse over");
        transform.localScale = ogSize * 1.2f;
    }
}