using System;
using UnityEngine;
using TMPro;

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
    public TextMeshProUGUI salidaTexto;
    
    void Start()
    {
        salidaTexto.gameObject.SetActive(false);
    }

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
            UIManager.Instance.BroadcastMsg("Pulsa espacio para recoger la sardina");
            puedeRecoger = true;
        }
        else
        {
            puedeRecoger = false;
            objetoCercano = null;
        }
    }

    void RecogerObjeto()
    {
        AudioManager.Instance.PlayClip(5);
        UIManager.Instance.BroadcastMsg("");
        fish.GetComponent<SpriteRenderer>().sprite = mesaSinObjeto;
        animator.SetBool("hasFish", true);
        hasFish = true;
        salidaTexto.gameObject.SetActive(true);
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, visionRadius * 2);
    }
}