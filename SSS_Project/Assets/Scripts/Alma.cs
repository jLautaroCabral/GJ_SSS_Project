using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alma : MonoBehaviour
{
    private Individuo _individuo { get; set; }
    public GameObject[] gesto;
    public Transform spawnPointGesto;
    
    public int _intentosConseguirEmpleo = 0;
    public int _limiteIntentos;
    
    private int felicidadBase = 10;
    [SerializeField]
    private int _felicidadTotal;

    private void Awake()
    {
        _individuo = this.gameObject.GetComponent<Individuo>();
    }

    private void CalcularFelicidad()
    {
        _felicidadTotal = felicidadBase;

        if (_individuo.Productos != 0 && _individuo.Productos > 10)
        {
            _felicidadTotal += 2;
        }

        if (!_individuo.Empleado && _individuo.Dinero < 20)
        {
            _felicidadTotal -= 15;
        }

        if (!_individuo.Empleado)
        {
            _felicidadTotal -= 3;
        }
        else
        {
            _felicidadTotal += 2;
        }

        if (_individuo.Dinero > 500)
        {
            _felicidadTotal += 15;
        }
        else if(_individuo.Dinero < 300 && _individuo.Dinero > 50)
        {
            _felicidadTotal += 5;
        }

        if (_intentosConseguirEmpleo > 0)
        {
            if (_intentosConseguirEmpleo > _limiteIntentos)
            {
                _felicidadTotal -= _intentosConseguirEmpleo - 10;
            }
            else
            {
                _felicidadTotal -= _intentosConseguirEmpleo - 1;
            }
        }
    }
    
    
    public void ActualizarEstado()
    {

        CalcularFelicidad();
        MostrarGestoSegunFelicidad(_felicidadTotal);
    }
    public void MostrarGestoSegunFelicidad(int felicidad)
    {
        if (felicidad < 10)
        {
            InstanciarGesto(0);
        } 
        else if (felicidad < 25)
        {
            InstanciarGesto(1);    
        }
        else
        {
            InstanciarGesto(2);
        }
    }
    public void InstanciarGesto(int indx)
    {
        Vector3 position = new Vector3(spawnPointGesto.position.x, spawnPointGesto.position.y, spawnPointGesto.position.z);
        Instantiate(gesto[indx], position, Quaternion.identity);
    }
}
