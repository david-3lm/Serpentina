using System;
using TMPro;
using UnityEngine;

using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    
    [SerializeField] private Canvas canvas;
    
    TextMeshProUGUI text;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Mantiene el objeto en todas las escenas
        }
        else
        {
            Destroy(gameObject); // Elimina duplicados si ya existe una instancia
        }
    }

    private void Start()
    {
        text = canvas.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void BroadcastMsg(string msg)
    {
        if(!canvas) return;
        
        text.text = msg;
    }
}

