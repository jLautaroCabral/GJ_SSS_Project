using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Patrullar : MonoBehaviour
{
    public Transform[] puntoPatrullaje;
    private int puntodestino = 0;
    private int puntorandom;
    NavMeshAgent agente;
    Vector3 destino;


    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        agente.autoBraking = false;
        IraSiguientePunto();
    }

    // Update is called once per frame
    void Update()
    {
        if (agente.remainingDistance < 1f)
        {
            IraSiguientePunto();
        }
    }

    public void IraSiguientePunto()
    {
        puntorandom = Random.Range(0, puntoPatrullaje.Length);
        if (puntoPatrullaje.Length ==0)
        {
            return;
        }
        agente.destination = puntoPatrullaje[puntodestino].position;
        puntodestino = (puntorandom) % puntoPatrullaje.Length;
    }
        
}
