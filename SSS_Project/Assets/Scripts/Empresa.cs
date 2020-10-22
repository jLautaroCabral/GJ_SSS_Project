using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Empresa : MonoBehaviour
{
    public int Dinero = 2000;
    public int Productos = 200;
    
    public bool puedoContratar = true;
    public int cantMaxEmpleados = 5;
    
    public List<Individuo> empleados = new List<Individuo>();
    
    public void ActualizarEstado()
    {
        // Debug.Log("Mis reservas son $" + Dinero);
    }

    public bool ContratarEmpleado(Individuo ind)
    {
        if (puedoContratar)
        {
            empleados.Add(ind);
            ind.Empleado = true;
            return true;
        }

        return false;
    }
}
