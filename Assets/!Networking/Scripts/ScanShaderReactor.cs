using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanShaderReactor : MonoBehaviour
{
    private Renderer renderer;


    private GameObject[] lights; 


    private GameObject[] objects;
    // Start is called before the first frame update
    void Start()
    {
        renderer = this.gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        objects = GameObject.FindGameObjectsWithTag("Scanner");

        

        Transform objPos = GetClosestScanner(objects);
        renderer = this.gameObject.GetComponent<Renderer>();
        if(objPos?.position != null){
            renderer.material.SetVector("_Position", objPos.position);
        }
        else{
            renderer.material.SetVector("_Position", new Vector3(999,999,999));
        }
        renderer.material.SetFloat("_Float", 5);
    }

    Transform GetClosestScanner (GameObject[] enemies)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        
        
        
        for(int i = 0; i < enemies.Length; i++)
        {
            Vector3 directionToTarget = enemies[i].transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if(dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = enemies[i].transform;
            }
        }
     
        return bestTarget;
    }
}
