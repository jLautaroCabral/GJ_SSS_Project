using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParcelaManager : MonoBehaviour
{
    public static ParcelaManager sharedInstance;
    
    public Transform[] spawnPointCasas;
    public GameObject[] ParcelasEscena;
    public List<Parcela> Parcelas = new List<Parcela>();
    
    // Start is called before the first frame update
    void Start()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        
        RegistrarParcelas();
    }

    void RegistrarParcelas()
    {
        ParcelasEscena = GameObject.FindGameObjectsWithTag("Parcela");
        foreach (GameObject gameObj in ParcelasEscena)
        {
            Parcelas.Add(gameObj.GetComponent<Parcela>());
        }
    }

    public void CrearEmpresaRandom()
    {
        Parcela parcela;
        do
        {
            parcela = Parcelas[Random.Range(0, Parcelas.Count)];
            
        } while (parcela.yaUsada);
        
        parcela.CrearEmpresa();
    }
    
    
}
