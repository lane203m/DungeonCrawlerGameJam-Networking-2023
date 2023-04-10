using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanLauncher : MonoBehaviour
{
    public GameObject m_Projectile;    
    public GameObject m_SpawnPoint; 
    public float reloadTime = 2f;
    public float reloadLeeway = 0.5f;

    public float currentReloadTime = 2f;
    public bool isLoaded = true;
    public bool queueFire = false;
    public AudioSource[] AudioSources;

    public bool playReload = false;

    private void Awake(){
        AudioSources = GetComponents<AudioSource>();
    }

    private void Update()
    {
        if ((Input.GetKeyDown("space") && isLoaded) || queueFire && isLoaded)
        {
            queueFire = false;
            AudioSources[0].Play();
            StartCoroutine(LaunchScanner());
        }


        if( Input.GetKeyDown("space") && currentReloadTime > reloadTime - reloadLeeway){
            queueFire = true;
        }
    }

    private IEnumerator LaunchScanner(){
        playReload = true;
        currentReloadTime = 0.0f;
        isLoaded = false;

        var vec = m_SpawnPoint.transform.eulerAngles;
        vec.x = Mathf.Round(vec.x / 90) * 90;
        vec.y = Mathf.Round(vec.y / 90) * 90;
        vec.z = Mathf.Round(vec.z / 90) * 90;
        
        var newPos = m_SpawnPoint.transform.position;
        newPos.y = newPos.y + 2.5f;
        
        Instantiate(m_Projectile, newPos,  Quaternion.Euler(vec));
        
        while(currentReloadTime < reloadTime){
            currentReloadTime += Time.deltaTime;
            
            
            yield return new WaitForEndOfFrame();
            if(currentReloadTime > reloadTime - reloadLeeway && playReload){
                playReload = false;
                AudioSources[1].Play();
            }
        }
        playReload = true;
        currentReloadTime = 2f;
        isLoaded = true;
        
    }

}
