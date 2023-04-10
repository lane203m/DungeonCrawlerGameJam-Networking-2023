using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagDeployer : MonoBehaviour
{
    public GameObject m_Projectile;    
    public GameObject m_SpawnPoint; 
    public float reloadTime = 4f;
    public bool isLoaded = true;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(FlagDeployerSub());
        }
    }

    private IEnumerator FlagDeployerSub(){
        isLoaded = false;
        var vec = m_SpawnPoint.transform.eulerAngles;
        vec.x = Mathf.Round(vec.x / 90) * 90;
        vec.y = Mathf.Round(vec.y / 90) * 90;
        vec.z = Mathf.Round(vec.z / 90) * 90;
        
        var newPos = m_SpawnPoint.transform.position;
        newPos.y = newPos.y + 2.5f;
        
        Instantiate(m_Projectile, newPos,  Quaternion.Euler(vec));
        
        yield return new WaitForSeconds(reloadTime);
        isLoaded = true;
    }

}
