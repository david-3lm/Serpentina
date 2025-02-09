using System;
using UnityEngine;

public class GatoInteractuar : MonoBehaviour
{
    public float visionRadius = 5f;
    public LayerMask playerLayer;
    private bool puedeRecoger = false;
    private GameObject objetoCercano;
    public Sprite mesaSinObjeto;
    public Animator animator;
    public bool hasFish;
    public GameObject fish;
    

    void Update()
    {
        DetectarObjetosEnRango();
        if (puedeRecoger && Input.GetKeyDown(KeyCode.Space))
            RecogerObjeto();
    }

    void DetectarObjetosEnRango()
    {
        if (Vector3.Distance(this.gameObject.transform.position, fish.transform.position) < 5f)
        {
            puedeRecoger = true;
            Debug.Log("Distancia: ");
        }
        else
        {
            puedeRecoger = false;
            objetoCercano = null;
        }
    }

    void RecogerObjeto()
    {
        fish.GetComponent<SpriteRenderer>().sprite = mesaSinObjeto;
        animator.SetBool("hasFish", true);
        hasFish = true;
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, visionRadius * 2);
    }
}