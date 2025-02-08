using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = Unity.Mathematics.Random;

public class EyeScript : MonoBehaviour
{

    [SerializeField] private float cooldown;
    private float count;
    private int i = 0;
    [SerializeField] private List<Sprite> eyeSprites;
    private Image sr;

    private void Start()
    {
        sr = GetComponent<Image>();
        count = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        count -= Time.deltaTime;
        if (count < 0)
        {
            if (i == 0) count = cooldown / 2;
            else count = cooldown;
            sr.sprite = eyeSprites[i];
            if (i < eyeSprites.Count - 1) i++;
            else i = 0;
        }
    }
}
