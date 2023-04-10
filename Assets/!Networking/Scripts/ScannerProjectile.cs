using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScannerProjectile : MonoBehaviour
{
     public float m_Speed = 100f;   
     public float m_Lifespan = 10f;
     private float timer = 0f;

     private Rigidbody m_Rigidbody;
 
     void Awake()
     {
         m_Rigidbody = GetComponent<Rigidbody>();
     }
 
     void Start()
     {
         m_Rigidbody.AddForce(m_Rigidbody.transform.forward * m_Speed);
         Destroy(gameObject, m_Lifespan);
     }

     void Update()
     {
        //if(timer => m_Lifespan){
        //    m_Rigidbody.rigidbody.velocity = Vector3.zero;
        //    rigidbody.angularVelocity = Vector3.zero;
        //}

     }
}
