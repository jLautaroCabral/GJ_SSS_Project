using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmpresaManager : MonoBehaviour
{
    public static EmpresaManager sharedInstance;
    
    private GameObject[] EmpresasEnEscena;
    public List<Empresa> Empresas = new List<Empresa>();
    // Start is called before the first frame update
    void Start()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        
        RegistrarEmpresas();
    }
    
    public void ActualizarEstadoDeEmpresas()
    {
        foreach (Empresa i in Empresas)
        {
            i.ActualizarEstado();
        }
    } 
    
    void RegistrarEmpresas()
    {
        EmpresasEnEscena = GameObject.FindGameObjectsWithTag("Empresa");
        foreach (GameObject gameObj in EmpresasEnEscena)
        {
            Empresas.Add(gameObj.GetComponent<Empresa>());
        }
    }
    
    public int GetSumaDineroEmpresas()
    {
        int total = 0;
        foreach (Empresa empresa in Empresas)
        {
            total += empresa.Dinero;
        }
        return total;
    }
    
    public int GetSumaProductosEmpresas()
    {
        int total = 0;
        foreach (Empresa empresa in Empresas)
        {
            total += empresa.Productos;
        }
        return total;
    }
    
    public GameObject[] GetEmpresas()
    {
        return EmpresasEnEscena;
    }
    
}
