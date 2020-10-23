using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Empresa : MonoBehaviour
{
    public string Nombre;
    public int Dinero = 200;
    public int Productos = 10;
    public int plazoDeConfiguracion = 4;
    
    public int valorProducto = 15;
    public int sueldo = 20;
    public int cantidadProduccion = 3;

    public bool puedoContratar = false;
    public int cantMaxEmpleados = 4;
    
    public Transform entrada;
    
    private int contadorDesabastecimiento = 0;
    private int contContratacionEntreDesabastecimiento = 0;
    
    private int contadorActualizacion = 0;
    private int dineroPlazoAnterior = 0;

    public List<Individuo> empleados = new List<Individuo>();
    
    public void ActualizarEstado()
    {
        contadorActualizacion++;
        ActualizarConfiguracion();
        EvaluarSiInvertit();
    }

    public void EvaluarSiInvertit()
    {
        if (Dinero > 500 && contadorDesabastecimiento <= 1)
        {
            ParcelaManager.sharedInstance.CrearEmpresaRandom();
            Dinero -= 400;
        }
    }

    private void Start()
    {
        if (empleados.Count < cantMaxEmpleados)
        {
            puedoContratar = true;
        }
        else
        {
            puedoContratar = false;
        }
    }

    public void ActualizarConfiguracion()
    {
        if (contadorActualizacion > plazoDeConfiguracion)
        {
            dineroPlazoAnterior = Dinero;
            CalcularSiEsConvenienteDespedir();
            
            Notificar("Se ha completado un plazo!");
            contadorActualizacion = 0;
        }
        
        CalcularSiEsConvenienteContratar();
    }

    public void CalcularSiEsConvenienteContratar()
    {
        if (Productos <= 5 )
        {
            contadorDesabastecimiento++;
        }
        
        if (contadorDesabastecimiento >= 2 && Dinero > 200)
        {
            if (contContratacionEntreDesabastecimiento == 0)
            {
                cantMaxEmpleados++;
                puedoContratar = true;
                contContratacionEntreDesabastecimiento = 2;
            }
            else
            {
                contContratacionEntreDesabastecimiento--;
            }

            contadorDesabastecimiento = 0;
        }
        
        if (empleados.Count == 0)
        {
            puedoContratar = true;
        }
    }

    public void CalcularSiEsConvenienteDespedir()
    {
        if (dineroPlazoAnterior > Dinero)
        {
            if (Productos > 100)
            {
                DespedirEmpledo();
                cantMaxEmpleados--;
            }
        }
    }

    public void DespedirEmpledo()
    {
        Notificar("Despedí un empleado");
    }
    public bool ContratarEmpleado(Individuo ind)
    {
        if (puedoContratar)
        {
            empleados.Add(ind);
            ind.Empleado = true;
            
            if (empleados.Count < cantMaxEmpleados)
            {
                puedoContratar = true;
            }
            else
            {
                puedoContratar = false;
            }
            
            return true;
        }
        return false;
    }

    public bool VenderProducto(Individuo ind, int cantidad = 3)
    {
        int monto = cantidad * valorProducto;
        if (ind.Dinero > monto && cantidad <= Productos)
        {
            ind.Dinero -= monto;
            this.Dinero += monto;
            ind.Productos += cantidad;
            this.Productos -= cantidad;
            return true;
        }

        return false;
    }

    public void ProducirProducto(Individuo ind)
    {
        Dinero -= sueldo;
        ind.Dinero += sueldo;
        Productos += cantidadProduccion;
    }

    public void Notificar(string msg)
    {
        Debug.Log("Empresa " + Nombre + ": " + msg);
    }
}
