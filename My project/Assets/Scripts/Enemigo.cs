using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

  
public class Enemigo : MonoBehaviour

{
    public Transform target; 
    public float speed = 15;

    //void OnTriggerEntered(Collider other)
    //{
        
  //  }

    // Update is called once per frame
    void Update()
    {
       if (target != null){
            Vector3 direction = target.position - transform.position;
            direction.y = 0; 
            transform.position += direction.normalized * speed * Time.deltaTime;
        } 
    }
}
