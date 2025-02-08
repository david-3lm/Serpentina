using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Desk : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private LayerMask playerLayer;

    public float scaleMultiplier = 1.3f; // Tamaño máximo al que se expande
    public float duration = 0.1f; // Duración total de la animación

    private Vector3 originalScale;
    private bool isPulsing = false; 

    private GameObject player;
    private Light playerLight;
    private Coroutine coroutine;

    private bool playerIn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        radius = 1f;
        originalScale = transform.localScale;
        playerLight = GameObject.FindGameObjectWithTag("Spot").GetComponent<Light>();
        playerIn = false;
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, radius, playerLayer);

        if (targetsInViewRadius.Length > 0)
        {
            if (playerIn) return;
            playerIn = true;
            targetsInViewRadius[0].gameObject.GetComponent<SpriteRenderer>().enabled = false;
            player = targetsInViewRadius[0].gameObject;
            if (coroutine != null)
                StopCoroutine(coroutine);
            coroutine = StartCoroutine(ChangeLightRadius(11f, 13f, 0.5f));
            StartCoroutine(AnimScale());
        }
        else if (player)
        {
            if (!playerIn) return;
            playerIn = false;
            player.GetComponent<SpriteRenderer>().enabled = true;
            if (coroutine != null)
                StopCoroutine(coroutine);
            coroutine = StartCoroutine(ChangeLightRadius(20f, 22f, 0.5f));
        }
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

        Quaternion originalRotation = transform.rotation; 
        Quaternion targetRotation = originalRotation * Quaternion.Euler(0, 0, 10);
        Quaternion targetRotation2 = originalRotation * Quaternion.Euler(0, 0, -10);

        while (elapsedTime < duration / 2)
        {
            float t = elapsedTime / (duration / 2);
            transform.localScale = Vector3.Lerp(originalScale, originalScale * scaleMultiplier, t);
            transform.rotation = Quaternion.Lerp(originalRotation, targetRotation, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = originalScale * scaleMultiplier;
        transform.rotation = targetRotation;

        elapsedTime = 0f;

        while (elapsedTime < duration / 2)
        {
            float t = elapsedTime / (duration / 2);
            transform.localScale = Vector3.Lerp(originalScale * scaleMultiplier, originalScale, t);
            transform.rotation = Quaternion.Lerp(targetRotation, targetRotation2, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = originalScale;
        transform.rotation = originalRotation;

        isPulsing = false;
    }

    private IEnumerator ChangeLightRadius(float targetInnerAngle, float targetOuterAngle, float duration)
    {

        float elapsedTime = 0f;
        float startInnerAngle = playerLight.innerSpotAngle;
        float startOuterAngle = playerLight.spotAngle;

        while (elapsedTime < duration)
        {
            playerLight.innerSpotAngle = Mathf.Lerp(startInnerAngle, targetInnerAngle, elapsedTime / duration);
            playerLight.spotAngle = Mathf.Lerp(startOuterAngle, targetOuterAngle, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        playerLight.innerSpotAngle = targetInnerAngle;
        playerLight.spotAngle = targetOuterAngle;
    }
}
