using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Necesidad : MonoBehaviour
{
    private Individuo _individuo { get; set; }
    public GameObject[] gesto;
    public Transform spawnPointGesto;
    
    private int _intentosConseguirEmpleo = 0;
    private int _limiteIntentos;


    [SerializeField]
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
        
        _felicidadTotal += (_individuo.Productos != 0) ? _individuo.Productos : -1;
        _felicidadTotal += (_individuo.Empleado)? 2 : -3;
        
        
        _felicidadTotal += (_individuo.Dinero != 0)? 
                                (_individuo.Dinero < 60 ? 5 : (_individuo.Dinero > 500? 15 : 10) )
                            :
                                (_individuo.Empleado)? 0 : -7;
        
        _felicidadTotal += (_intentosConseguirEmpleo == 0)?
                                0 
                            : 
                                (_intentosConseguirEmpleo < _limiteIntentos)? -_intentosConseguirEmpleo :-_limiteIntentos-5;
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
