using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EstadoManager : MonoBehaviour
{
    public static EstadoManager sharedInstance;
    
    private Estado _estado;
    public int impIndActual = 0;
    public int impEmpActual = 0;
    private int contadorImpuesto = 0;
    public GameObject casa;
    public GameObject[] spawnPoints;

    public Text textImpInd;
    public Text textImpEmp;
    public Text textDineroEstatal;
    
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
        contadorImpuesto++;
        if (contadorImpuesto == 2)
        {
            contadorImpuesto = 0;
            cobrarImpuestos();
        }
        actualizarDatosUI();
    }

    public int GetReservaEstatal()             
    {
        return _estado.Dinero;
    }

    public void cobrarImpuestos()
    {
        CobrarImpuestosEmpresas();
        CobrarImpuestosPersonas();
    }

    public void CounstruirCasa()
    {
        if (!(_estado.Dinero - 300 < 0))
        {
            _estado.Dinero -= 300;

            Vector3 point = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;

            Instantiate(
                casa,
                new Vector3(point.x + Random.Range(-5, 5), point.y, point.z +Random.Range(-5, 5)),
                Quaternion.identity
                );
        }
    }

    public void SubirImpuestosInd()
    {
        impIndActual++;
        actualizarDatosUI();
    }

    public void SubirImpuestoEmp()
    {
        impEmpActual++;
        actualizarDatosUI();
    }

    public void actualizarDatosUI()
    {
        textImpEmp.text = "$" + impEmpActual;
        textImpInd.text = "$" + impIndActual;
        textDineroEstatal.text = "$" + _estado.Dinero;
    }

    void CobrarImpuestosEmpresas()
    {
        List<Empresa> empresas = EmpresaManager.sharedInstance.Empresas;
        foreach (var e in empresas)
        {
            e.Dinero -= impEmpActual;
            _estado.Dinero += impEmpActual;
        }
    }

    void CobrarImpuestosPersonas()
    {
        List<Individuo> ind = IndividuoManager.sharedInstance.Individuos;
        foreach (var i in ind)
        {
            i.Dinero -= impIndActual;
            _estado.Dinero += impIndActual;
        }
    }
}
