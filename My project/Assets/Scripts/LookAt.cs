using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    [SerializeField] private Transform target;

    void Update()
    {
        // Calcular la direcci�n hacia el objetivo
        Vector3 direction = target.position - transform.position;
        // Crear la rotaci�n que mira en esa direcci�n
        Quaternion rotation = Quaternion.LookRotation(direction);
        // Aplicar la rotaci�n y agregar un giro adicional de 180 grados en el eje Y
        transform.rotation = rotation * Quaternion.Euler(0, 180, 0);
    }
}
    