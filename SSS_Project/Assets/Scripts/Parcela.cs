using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;

public class Parcela : MonoBehaviour
{
    public GameObject[] posiblesEmpresas;

     Vector3 empresaSpawn;
     GameObject empresa;
     private Vector3 startPosition;
     float subirValor = 8;
     bool debeSubir = true;
    
     void SubirEmpresa()
    {
        StartCoroutine(nameof(subir));
    }

     void pararCo()
    {
        StopCoroutine(nameof(subir));
    }

     private void Start()
     {
         empresaSpawn = new Vector3(
             this.transform.position.x,
             this.transform.position.y - 8,
             this.transform.position.z
             );
     }

     IEnumerator subir()
    {
        while (debeSubir)
        {
            Debug.Log("Corr");
            empresa.transform.position = new Vector3(
                    empresa.transform.position.x,
                    empresa.transform.position.y + 1,
                    empresa.transform.position.z
                );
            if (empresa.transform.position.y >= empresaSpawn.y + subirValor)
            {
                debeSubir = false;
            }
            yield return new WaitForSeconds(0.01f);
        }
        pararCo();
    }
    
    
    public void CrearEmpresa()
    {
        empresa = posiblesEmpresas[Random.Range(0, posiblesEmpresas.Length)];
        Instantiate(empresa, empresaSpawn, Quaternion.identity);
        SubirEmpresa();
    }
    
}
