using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScannerCollisionDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
   {
      if(collision.gameObject.tag == "GridBox"){
        Destroy(gameObject.transform.parent.gameObject);
      }

   }
}
