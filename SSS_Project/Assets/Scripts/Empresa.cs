using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Empresa : MonoBehaviour
{
    public int Nombre;
    public int Dinero = 200;
    public int Productos = 10;
    public int plazoDeConfiguracion = 4;
    private int contadorDesabastecimiento = 0;
    private int contadorVentaDiara = 0;
    private int contadorActualizacion = 0;
    private int dineroPlazoAnterior = 0;

    public int valorProducto = 15;
    public int sueldo = 20;
    public int cantidadProduccion = 3;
    
    public bool puedoContratar = false;
    public int cantMaxEmpleados = 5;
    
    public List<Individuo> empleados = new List<Individuo>();
    
    public void ActualizarEstado()
    {
        contadorActualizacion++;
        ActualizarConfiguracion();
    }

    public void ActualizarConfiguracion()
    {
        if (contadorActualizacion >= plazoDeConfiguracion)
        {
            dineroPlazoAnterior = Dinero;
        }
        CalcularSiEsConvenienteDespedir();
        CalcularSiEsConvenienteContratar();
    }

    public void CalcularSiEsConvenienteContratar()
    {
        if (Productos == 0 && empleados.Count < cantMaxEmpleados)
        {
            contadorDesabastecimiento++;
        }
        if (contadorDesabastecimiento >= 3)
        {
            cantMaxEmpleados++;
            puedoContratar = true;
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
            }
        }
    }

    public void DespedirEmpledo()
    {
        
    }
    public bool ContratarEmpleado(Individuo ind)
    {
        if (puedoContratar)
        {
            empleados.Add(ind);
            ind.Empleado = true;
            
            puedoContratar = false;
            return true;
        }
        return false;
    }

    public bool VenderProducto(Individuo ind, int cantidad = 1)
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
        
    }
}
