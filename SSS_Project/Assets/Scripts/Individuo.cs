using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Individuo : MonoBehaviour
{
    public string Nombre;
    public string Apellido;
    public int Dinero = 10;
    public int Productos = 10;
    public bool Empleado;

    public bool buscandoEmpleo;
    [SerializeField]
    private bool TareaEnProgreso = false;
    
    private Alma _alma;
    private Empresa _lugarTrabajoActual;
    private Empresa empresaTarget;

    private void Awake()
    {
        _alma = this.gameObject.GetComponent<Alma>();
    }

    public void consumirProducto()
    {
        if (Productos != 0)
        {
            Productos--;
        }
        else
        {
            bool seCompro = IrAComprar(3);
            if (!seCompro)
            {
                IrAComprar();
            }
        }
    }
    
    public void ActualizarEstado()
    {
        
        _alma.ActualizarEstado();
        consumirProducto();
        
        if (!TareaEnProgreso)
        {
            CalcularSiguienteTarea();
        }
    }

    private void CalcularSiguienteTarea()
    {
        CalcularSiBuscarTrabajo();
        
        if (Empleado && !TareaEnProgreso)
        {
            IrAlTrabajo();
        }
        
    }

    public void CalcularSiBuscarTrabajo()
    {
        if (!Empleado && buscandoEmpleo)
        {
            BuscarTrabajo();
        }
    }
    
    public void Trabajar(Empresa empresa)
    {
        _lugarTrabajoActual.ProducirProducto(this.gameObject.GetComponent<Individuo>());
        TareaEnProgreso = false;
    }

    public void IrAlTrabajo()
    {
        TareaEnProgreso = true;

        Trabajar(_lugarTrabajoActual);
    }
    
    public void BuscarTrabajo()
    {
        TareaEnProgreso = true;
        Notificar("Buscando trabajo");

        GameObject[] empresasDisponibles = EmpresaManager.sharedInstance.GetEmpresas();

        empresaTarget = empresasDisponibles[Random.Range(0, empresasDisponibles.Length - 1)].GetComponent<Empresa>();
        
        Invoke(nameof(IntentarSerContratado), 10f);
    }

    public void IntentarSerContratado()
    {
        // Debug.Log("Encontré una empresa!");
        bool resultado = empresaTarget.ContratarEmpleado(this.gameObject.GetComponent<Individuo>());
        if (resultado)
        {
            _lugarTrabajoActual = empresaTarget;
            empresaTarget = null;
            TareaEnProgreso = false;
            _alma._intentosConseguirEmpleo = 0;
        }
        else
        {
            Notificar("No me contrataron :(");
            _alma._intentosConseguirEmpleo++;
            BuscarTrabajo();
        }
        
    }
    
    public bool IrAComprar(int cantidad = 1)
    {
        TareaEnProgreso = true;

        GameObject[] empresasDisponibles = EmpresaManager.sharedInstance.GetEmpresas();

        empresaTarget = empresasDisponibles[Random.Range(0, empresasDisponibles.Length - 1)].GetComponent<Empresa>();
        
        return ComprarProducto(empresaTarget, cantidad);
    }
    
    public bool ComprarProducto(Empresa empresa, int cantidad)
    {
        //Debug.Log("Comprando producto");
        
        bool resultado = empresaTarget.VenderProducto(this.gameObject.GetComponent<Individuo>(), cantidad);
        if (resultado)
        {
            Notificar("He comprado mi producto!");
            TareaEnProgreso = false;
        }
        else
        {
            Notificar("No pude comprar mi producto!");
        }
        
        return resultado;
    }
    
    public void Notificar(string msg)
    {
        Debug.Log(Nombre + ": " + msg);
    } 
    public void ReclamarAlEstado()
    {
        TareaEnProgreso = true;
        Notificar("Reclamando al estado");
    }

}
