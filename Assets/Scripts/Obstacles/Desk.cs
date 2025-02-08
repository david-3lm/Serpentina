using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Desk : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private LayerMask playerLayer;

    public float scaleMultiplier = 1.3f; // Tamaño máximo al que se expande
    public float duration = 0.1f; // Duración total de la animación

    private Vector3 originalScale;
    private bool isPulsing = false; 

    private GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        radius = 1f;
        originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, radius, playerLayer);

        if (targetsInViewRadius.Length > 0)
        {
            targetsInViewRadius[0].gameObject.GetComponent<SpriteRenderer>().enabled = false;
            player = targetsInViewRadius[0].gameObject;
            Debug.Log("Desk Collided");
            StartCoroutine(AnimScale());
        }
        else if (player)
            player.GetComponent<SpriteRenderer>().enabled = true;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    IEnumerator AnimScale()
    {
        isPulsing = true;
        float elapsedTime = 0f;

        // Fase 1: Aumentar tamaño
        while (elapsedTime < duration / 2)
        {
            transform.localScale = Vector3.Lerp(originalScale, originalScale * scaleMultiplier, elapsedTime / (duration / 2));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = originalScale * scaleMultiplier;

        // Resetear tiempo para la segunda fase
        elapsedTime = 0f;

        // Fase 2: Reducir tamaño
        while (elapsedTime < duration / 2)
        {
            transform.localScale = Vector3.Lerp(originalScale * scaleMultiplier, originalScale, elapsedTime / (duration / 2));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = originalScale;

        isPulsing = false;
    }
}
