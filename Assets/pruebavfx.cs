using UnityEngine;
using UnityEngine.VFX;

public class pruebavfx : MonoBehaviour
{
    
    VisualEffect effect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        effect = GetComponent<VisualEffect>();
        
        effect.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
