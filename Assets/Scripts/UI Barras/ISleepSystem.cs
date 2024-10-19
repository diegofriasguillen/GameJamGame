using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISleepSystem
{
    void reduccion();
    void reduccion(int r);
    void TickActualizarStats();
    void Aumento(int a);
    void SumMiedo(int a);
    void Despertar();
    void Dormir();

}
