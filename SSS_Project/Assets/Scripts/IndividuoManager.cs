using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividuoManager : MonoBehaviour
{
    public static IndividuoManager sharedInstance;
    
    public GameObject[] IndividuosEnEscena;
    public List<Individuo> Individuos = new List<Individuo>();
    
    // Start is called before the first frame update
    void Start()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        
        RegistrarIndividuos();
    }

    public void ActualizarEstadoDeIndividuos()
    {
        foreach (Individuo i in Individuos)
        {
            i.ActualizarEstado();
        }
    } 
    
    void RegistrarIndividuos()
    {
        IndividuosEnEscena = GameObject.FindGameObjectsWithTag("Individuo");
        foreach (GameObject gameObj in IndividuosEnEscena)
        {
            Individuos.Add(gameObj.GetComponent<Individuo>());
        }
    }
    
    public int GetSumaDineroIndividuos()
    {
        int total = 0;
        foreach (Individuo individuo in Individuos)
        {
            total += individuo.Dinero;
        }
        return total;
    }
    
    public int GetSumaProductosIndividuos()
    {
        int total = 0;
        foreach (Individuo individuo in Individuos)
        {
            total += individuo.Productos;
        }
        return total;
    }
    
    
}
