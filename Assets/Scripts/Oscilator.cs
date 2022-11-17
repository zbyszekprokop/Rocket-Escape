using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscilator : MonoBehaviour
{
    Vector3 startingPos;
    [SerializeField]Vector3 movementVector;
    [SerializeField][Range(0,1)]float movementFactor;
    [SerializeField]float period=4f;
    void Start()
    {
        startingPos = transform.position;
    }
    void Update()
    {
        if(period <= Mathf.Epsilon) // epsilon - najmniejsza liczba do jakiej jest dostęp, lepiej porównywać do tego niż do 0
        {
            return;                 // zatrzymuje błąd NaN, by nie dzielic przez zero
        }
        float cycles = Time.time / period; // stale rośnie
        const float tau = Mathf.PI*2; //stała jednostka tau czyt. 6.2
        float sinusoida = Mathf.Sin(cycles*tau);// od -1 do 1
        movementFactor = (sinusoida +1) /2f; // obliczenia na od 0 do 1
        Vector3 offset = movementVector*movementFactor;
        transform.position = startingPos+offset;
    }
}
