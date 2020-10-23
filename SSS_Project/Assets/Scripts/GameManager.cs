using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager sharedInstance;
    
    public int tiempoActualizacion = 10;
    public Text textoTiempoActualizacion;
    public Text textoDineroTotalEnIndividuos;
    public Text textoDineroTotalEnEmpresas;
    public Text textoDineroEstatal;

    public Text textTotalProductosEnPempresas;
    public Text textTotalProductosEnIndividuos;

    public GameObject panelGanaste;
    public GameObject panelPerdiste;
    
    private int _segundos = 0;
    

    private void Start()
    {
        ParcelaManager.sharedInstance.CrearEmpresaRandom();
        ParcelaManager.sharedInstance.CrearEmpresaRandom();
        ParcelaManager.sharedInstance.CrearEmpresaRandom();
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        startTimer();
    }

    public void RevisarSiPerdiste()
    {
        List<Individuo> indiv = IndividuoManager.sharedInstance.Individuos;
        int felicidadTotal = 0;
        int contador;
        foreach (var i in indiv)
        {
            felicidadTotal += i._alma._felicidadTotal;
        }

        contador = felicidadTotal / indiv.Count;

        if (contador < 8)
        {
            Perdiste();
        }
    }
    
    public void ActualizarGameState()
    {
        int dineroTotalInd = IndividuoManager.sharedInstance.GetSumaDineroIndividuos();
        int dineroTotalEmp = EmpresaManager.sharedInstance.GetSumaDineroEmpresas();
        textoDineroTotalEnIndividuos.text = "" + dineroTotalInd;
        textoDineroTotalEnEmpresas.text = "" + dineroTotalEmp;
        textoDineroEstatal.text = "" + EstadoManager.sharedInstance.GetReservaEstatal();

        textTotalProductosEnPempresas.text = "" + EmpresaManager.sharedInstance.GetSumaProductosEmpresas();
        textTotalProductosEnIndividuos.text = "" + IndividuoManager.sharedInstance.GetSumaProductosIndividuos();
        
        IndividuoManager.sharedInstance.ActualizarEstadoDeIndividuos();
        EmpresaManager.sharedInstance.ActualizarEstadoDeEmpresas();
        EstadoManager.sharedInstance.ActualizarEstadoDelEstado();

        if (dineroTotalEmp > 10000 && dineroTotalInd > 2000)
        {
            Ganaste();
        }

        RevisarSiPerdiste();
    }

    public void Ganaste()
    {
        panelGanaste.SetActive(true);
    }

    public void Perdiste()
    {
        panelPerdiste.SetActive(true);
    }

    #region Timer

    public void startTimer()
    {
        Invoke(nameof(updateTimer), 1f);
    }

    public void restartTimer()
    {
        _segundos = -1;
        startTimer();
    }
    public void updateTimer()
    {
        _segundos++;
        EscribirTiempo();
        if (_segundos < tiempoActualizacion)
        {
            Invoke(nameof(updateTimer), 1f);
        }
        else
        {
            ActualizarGameState();
            restartTimer();
        }
    }
    
    public void EscribirTiempo()
    {
        textoTiempoActualizacion.text = "" + _segundos;
    }

    #endregion
    
}
