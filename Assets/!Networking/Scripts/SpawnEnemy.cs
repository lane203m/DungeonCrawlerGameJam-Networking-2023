using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemy;
    public bool triggerSpawn = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(triggerSpawn){
            triggerSpawn = false;
            
            StartCoroutine(startSpawn());


        }


    }


    private IEnumerator startSpawn(){
        yield return new WaitForSeconds(0.02f);

        var vec = gameObject.transform.eulerAngles;
        vec.x = Mathf.Round(vec.x / 90) * 90;
        vec.y = Mathf.Round(vec.y / 90) * 90;
        vec.z = Mathf.Round(vec.z / 90) * 90;
        
        var newPos = gameObject.transform.position;
        
        var newEnemy = Instantiate(enemy, newPos,  Quaternion.Euler(vec));
    }
}


