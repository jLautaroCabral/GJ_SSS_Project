using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.PlayerLoop;
using Random = UnityEngine.Random;

public class Individuo : MonoBehaviour
{
    public string Nombre;
    public string Apellido;
    public int Dinero = 10;
    public int Productos = 10;
    public bool Empleado;

    public bool buscandoEmpleo;
    public bool comprando;
    
    [SerializeField]
    private int pilaDeTareas = 0;
    
    public Alma _alma;
    private Empresa _lugarTrabajoActual;
    private Empresa empresaTarget = null;

    [SerializeField]
    private NavMeshAgent _agent;

    private Vector3 startPoint;
    private bool yendoATrabajar = false;
    private bool haTrabajadoUnaVezEnElDia = false;
    private bool TareaEnProgreso = false;
    private bool yendoATarea = false;

    private Vector3[] patrolPoints;

    private void Awake()
    {
        _alma = this.gameObject.GetComponent<Alma>();
    }

    private void Start()
    {
        patrolPoints = GenerarPatrolPoints();
        startPoint = this.transform.position;
    }

    public void ActualizarEstado()
    {
        StopCoroutine(nameof(patrol));
        haTrabajadoUnaVezEnElDia = false;
        _alma.ActualizarEstado();

        consumirProducto();
        
        if (!yendoATarea && !TareaEnProgreso)
        {
            CalcularSiguienteTarea();
        }
        
    }

  
    
    private void CalcularSiguienteTarea()
    {
        Notificar("Calculando");

        CalcularSiBuscarTrabajo();
        
        if (Empleado && !haTrabajadoUnaVezEnElDia)
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
    
    // -----------------------------------------------------------------------------
    #region Trabajo
    public void IrAlTrabajo()
    {
        yendoATarea = true;
        yendoATrabajar = true;
        Notificar("Yendo a trabajar");
        
        _agent.SetDestination(_lugarTrabajoActual.entrada.position);
    }
    
    public void BuscarTrabajo()
    {
        yendoATarea = true;
        Notificar("Buscando trabajo");
        GameObject[] empresasDisponibles = EmpresaManager.sharedInstance.GetEmpresas();
        empresaTarget = empresasDisponibles[Random.Range(0, empresasDisponibles.Length)].GetComponent<Empresa>();
        
        _agent.SetDestination(empresaTarget.entrada.position);
    }
    
    
    public void Trabajar(Empresa empresa)
    {
        Notificar("Trabajando");
        _lugarTrabajoActual.ProducirProducto(this.gameObject.GetComponent<Individuo>());
        haTrabajadoUnaVezEnElDia = true;
        yendoATrabajar = false;
        yendoATarea = false;
        
        Deambular();
    }

    public void IntentarSerContratado()
    {
        bool resultado = empresaTarget.ContratarEmpleado(this.gameObject.GetComponent<Individuo>());
        if (resultado)
        {
            _lugarTrabajoActual = empresaTarget;
            _alma._intentosConseguirEmpleo = 0;
            buscandoEmpleo = false;
        }
        else
        {
            Notificar("No me contrataron :(");
            _alma._intentosConseguirEmpleo++;
        }
        
        yendoATarea = false;
        Deambular();
    }
    
    
    #endregion
    // -----------------------------------------------------------------------------
    #region Comprar
    public void IrAComprar(int cantidad)
    {
        Notificar("Yendo a comprar");
        comprando = true;
        yendoATarea = true;

        GameObject[] empresasDisponibles = EmpresaManager.sharedInstance.GetEmpresas();
        int suppCont = 0;
        bool noEncontreEmpresaParaComprar = false;
        while (true)
        {
            suppCont++;
            empresaTarget = empresasDisponibles[Random.Range(0, empresasDisponibles.Length)]
                .GetComponent<Empresa>();
            if (empresaTarget != _lugarTrabajoActual)
            {
                break;
            }

            if (suppCont > 13)
            {
                noEncontreEmpresaParaComprar = true;
                break;
            }
        }

        if (noEncontreEmpresaParaComprar)
        {
            return;
        }
        
        _agent.SetDestination(empresaTarget.entrada.position);
    }
    
    public void ComprarProducto(Empresa empresa)
    {
        
        bool resultado = empresaTarget.VenderProducto(this.gameObject.GetComponent<Individuo>());
        if (resultado)
        {
            Notificar("He comprado mi producto!");
        }
        else
        {
            Notificar("No pude comprar mi producto!");
        }
        
        comprando = false;
        yendoATarea = false;
        
        CalcularSiguienteTarea();
    }
    #endregion
    // -----------------------------------------------------------------------------
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Empresa>() == empresaTarget)
        {
            if (buscandoEmpleo)
            {
                RealizarTarea(() => IntentarSerContratado());
            }
            if (comprando)
            {
                RealizarTarea((x) => ComprarProducto(x), empresaTarget);
            }
        }

        if (other.gameObject.GetComponent<Empresa>() == _lugarTrabajoActual)
        {
            if (yendoATrabajar)
            {
                RealizarTarea(() => Trabajar(_lugarTrabajoActual));
            }
        }
    }
    
    public void Notificar(string msg)
    {
        Debug.Log(Nombre + ": " + msg);
    } 
    public void ReclamarAlEstado()
    {
        Notificar("Reclamando al estado");
    }
    
    public void RealizarTarea(Action func)
    {
        TareaEnProgreso = true;
        func();
        TareaEnProgreso = false;
    }
    public void RealizarTarea(Action<int> func, int cantidad)
    {
        pilaDeTareas++;
        func(cantidad);
        pilaDeTareas--;
    }
    public void RealizarTarea(Action<Empresa> func, Empresa empresa)
    {
        pilaDeTareas++;
        func(empresa);
        pilaDeTareas--;
    }
    
    public void Deambular()
    {
        Notificar("Deambulando");
        _agent.SetDestination(startPoint);
        patrullar();
    }

    public void patrullar()
    {
        StartCoroutine(nameof(patrol));
    }

    IEnumerator patrol()
    {
        int counter = 0;
        while (counter < patrolPoints.Length)
        {
            _agent.SetDestination(patrolPoints[counter]);
            counter++;
            yield return new WaitForSeconds(3);
        }
    }
    
    


    Vector3[] GenerarPatrolPoints()
    {
        Vector3[] points = new Vector3[4];

        for (int i = 0; i < points.Length; i++)
        {
            points[i] = new Vector3(
                startPoint.x + Random.Range(-6, 6),
                startPoint.y,
                startPoint.z + Random.Range(-6, 6));
        }

        return points;
    }

    public void consumirProducto()
    {
        if (Productos == 0)
        {
            IrAComprar(3);
        }
        else
        {
            Productos--;
        }
    }
}
