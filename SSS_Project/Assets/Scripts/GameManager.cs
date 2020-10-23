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
    
    private int _segundos = 0;
    
    private void Start()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        startTimer();
    }
    
    public void ActualizarGameState()
    {
        textoDineroTotalEnIndividuos.text = "" + IndividuoManager.sharedInstance.GetSumaDineroIndividuos();
        textoDineroTotalEnEmpresas.text = "" + EmpresaManager.sharedInstance.GetSumaDineroEmpresas();
        textoDineroEstatal.text = "" + EstadoManager.sharedInstance.GetReservaEstatal();

        textTotalProductosEnPempresas.text = "" + EmpresaManager.sharedInstance.GetSumaProductosEmpresas();
        textTotalProductosEnIndividuos.text = "" + IndividuoManager.sharedInstance.GetSumaProductosIndividuos();
        
        IndividuoManager.sharedInstance.ActualizarEstadoDeIndividuos();
        EmpresaManager.sharedInstance.ActualizarEstadoDeEmpresas();
        EstadoManager.sharedInstance.ActualizarEstadoDelEstado();
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
