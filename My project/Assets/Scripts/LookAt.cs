using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    [SerializeField] private Transform target;

    void Update()
    {
        // Calcular la dirección hacia el objetivo
        Vector3 direction = target.position - transform.position;
        // Crear la rotación que mira en esa dirección
        Quaternion rotation = Quaternion.LookRotation(direction);
        // Aplicar la rotación y agregar un giro adicional de 180 grados en el eje Y
        transform.rotation = rotation * Quaternion.Euler(0, 180, 0);
    }
}
    