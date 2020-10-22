using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Gesto : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    public float time = 0;
    
    private void Awake()
    {
        _spriteRenderer = this.transform.GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        StartCoroutine(nameof(SubirYDesaparecer));
        
    }

    IEnumerator SubirYDesaparecer()
    {
        while (true)
        {
            if (_spriteRenderer.color.a > 0.1f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z);
                _spriteRenderer.color = new Color(255, 255, 255, _spriteRenderer.color.a - 0.01f);
            }
            else
            {
                Destroy(this.gameObject);
            }
            yield return new WaitForSeconds(time);
        }
    }
}
