using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Individuo : MonoBehaviour
{
    public int Dinero = 10;
    public int Productos = 10;
    public bool Empleado;
    
    [SerializeField]
    private bool TareaEnProgreso = false;
    
    private Alma _alma;
    private Empresa _lugarTrabajoActual;
    private Empresa empresaTarget;

    private void Awake()
    {
        _alma = this.gameObject.GetComponent<Alma>();
    }

    public void ActualizarEstado()
    {
        _alma.ActualizarEstado();
        if (!TareaEnProgreso)
        {
            CalcularSiguienteTarea();
        }
    }

    private void CalcularSiguienteTarea()
    {
        if (!Empleado)
        {
            BuscarTrabajo();
        }

        if (Dinero >= 15)
        {
            IrAComprar();
        }
            
    }

    public void IrAComprar()
    {
        TareaEnProgreso = true;
        Debug.Log("Yendo a comprar");

        GameObject[] empresasDisponibles = EmpresaManager.sharedInstance.GetEmpresas();

        empresaTarget = empresasDisponibles[Random.Range(0, empresasDisponibles.Length - 1)].GetComponent<Empresa>();
        
        ComprarProducto(empresaTarget);
    }
    
    public void ComprarProducto(Empresa empresa)
    {
        TareaEnProgreso = true;
        Debug.Log("Yendo a comprar un producto");
    }

    public void Trabajar(Empresa empresa)
    {
        TareaEnProgreso = true;
        Debug.Log("Yendo a comprar un producto");
    }

    public void BuscarTrabajo()
    {
        TareaEnProgreso = true;
        Debug.Log("Buscando trabajo");

        GameObject[] empresasDisponibles = EmpresaManager.sharedInstance.GetEmpresas();

        empresaTarget = empresasDisponibles[Random.Range(0, empresasDisponibles.Length - 1)].GetComponent<Empresa>();
        
        Invoke(nameof(IntentarSerContratado), 10f);
    }

    public void IntentarSerContratado()
    {
        Debug.Log("Encontré una empresa!");
        bool resultado = empresaTarget.ContratarEmpleado(this.gameObject.GetComponent<Individuo>());
        if (resultado)
        {
            Debug.Log("Me contrataron!!!");
            _lugarTrabajoActual = empresaTarget;
            empresaTarget = null;
            TareaEnProgreso = false;
            _alma._intentosConseguirEmpleo = 0;
        }
        else
        {
            Debug.Log("No me contrataron :(");
            _alma._intentosConseguirEmpleo++;
            BuscarTrabajo();
        }
        
    }
    public void ReclamarAlEstado()
    {
        TareaEnProgreso = true;
        Debug.Log("Reclamando al estado");
    }
}
