using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoManager : MonoBehaviour
{
    public static EstadoManager sharedInstance;
    
    private Estado _estado;
    
    // Start is called before the first frame update
    void Start()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        
        _estado = GameObject.FindWithTag("Estado").GetComponent<Estado>();
    }

    public void ActualizarEstadoDelEstado()
    {
        _estado.ActualizarEstado();
    }

    public int GetReservaEstatal()             
    {
        return _estado.Dinero;
    }
}
