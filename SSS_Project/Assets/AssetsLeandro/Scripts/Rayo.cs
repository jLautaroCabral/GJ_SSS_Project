using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rayo : MonoBehaviour
{
    public AudioSource fuentesonido;
     public AudioClip[] sonido;
    void Start()
    {
        
       


    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Individuo") && !fuentesonido.isPlaying)
            {
                fuentesonido.clip = sonido[0];
                fuentesonido.Play();
            }
            ;
        }
    }

     public void ReproducirSonido(int pista)
    {
        fuentesonido.clip = sonido[pista];
        fuentesonido.Play();

    }

}
