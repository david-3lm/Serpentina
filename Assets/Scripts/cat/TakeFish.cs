using System;
using UnityEngine;

public class GatoInteractuar : MonoBehaviour
{
    public float visionRadius = 5f;
    public LayerMask playerLayer;
    private bool puedeRecoger = false;
    private GameObject objetoCercano;
    public Sprite mesaSinObjeto;

    void Update()
    {
        DetectarObjetosEnRango();
        if (puedeRecoger && Input.GetKeyDown(KeyCode.Space))
            RecogerObjeto();
    }

    void DetectarObjetosEnRango()
    {
        Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, visionRadius, playerLayer);

        if (targetsInViewRadius.Length > 0)
        {
            puedeRecoger = true;
            objetoCercano = targetsInViewRadius[0].gameObject;
        }
        else
        {
            puedeRecoger = false;
            objetoCercano = null;
        }
    }

    void RecogerObjeto()
    {
        objetoCercano.GetComponent<SpriteRenderer>().sprite = mesaSinObjeto;  
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, visionRadius);
    }
}