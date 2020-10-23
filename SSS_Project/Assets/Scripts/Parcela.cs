using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using Random = UnityEngine.Random;

public class Parcela : MonoBehaviour
{
    public GameObject[] posiblesEmpresas;

     Vector3 empresaSpawn;
     GameObject empresa;
     private Vector3 startPosition;
     public bool yaUsada = false;


     private void Awake()
     {
         empresa = posiblesEmpresas[Random.Range(0, posiblesEmpresas.Length)];
         empresaSpawn = new Vector3(
             this.transform.position.x,
             this.transform.position.y - 0,
             this.transform.position.z
         );
     }

     private void Start()
     {
         
     }
     
    
    
    public void CrearEmpresa()
    {
        yaUsada = true;
        Instantiate(empresa, empresaSpawn, Quaternion.identity);
    }
    
    /*

    private void FixedUpdate()
    {
        if (debeSubir)
        {
            empresa.transform.Translate(Vector3.up);
            if (empresa.transform.position.y <= empresaSpawn.y + subirValor)
            {
                Debug.Log("Lol");
                debeSubir = false;
            }
        }
    }
    */
}
