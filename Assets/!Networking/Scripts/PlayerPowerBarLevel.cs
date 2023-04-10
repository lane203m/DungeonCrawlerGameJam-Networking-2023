using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerBarLevel : MonoBehaviour
{
    public ScanLauncher scanLauncher;
    public bool animationFinished = true;
    public float minWidth = 0.06f;
    public float barPercent = 1f;
    public float maxWidth = 0.93f;
    void Awake(){
        scanLauncher = GameObject.Find("MainCamera").GetComponent<ScanLauncher>();
    }
   
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        barPercent = scanLauncher.currentReloadTime / scanLauncher.reloadTime;

        Vector3 scale = transform.localScale;
        scale.x = -1;

        scale.x = barPercent * maxWidth;

        transform.localScale = scale;
    }

    private IEnumerator ReloadAnim(){
        float time = 0.0f;
        barPercent = 0f;
        animationFinished = false;

        while(time < scanLauncher.reloadTime)
        {
            time += Time.deltaTime;
            if(barPercent < 1) {
                barPercent = time / scanLauncher.reloadTime;
            }

            Vector3 scale = transform.localScale;
            scale.x = -1;

            scale.x = barPercent * maxWidth;

            transform.localScale = scale;


            yield return new WaitForEndOfFrame();
        }
        
        yield return new WaitForEndOfFrame();

        animationFinished = true;
    }
}
