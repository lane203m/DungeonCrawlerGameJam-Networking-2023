using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroyer : MonoBehaviour
{
    public bool setToKill = false;
    public bool isDying = false;

    public bool isDead = false;
    public bool canKill = true;

    public float deathTime = 2f;

    public float dissolvePercent = -0.07f;

    public GameObject explosionObject;

    public bool overrideAll = false;
    public AudioSource[] AudioSources;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void Awake(){
        AudioSources = GetComponents<AudioSource>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(!overrideAll){
            if(collision.gameObject.tag == "ScannerHitbox"){
                canKill = false;
                setToKill = true;
            }
        }
    }



    private void Update()
    {
        if(!overrideAll){
            if (setToKill && !isDying)
            {
                isDying = true;
                StartCoroutine(KillEntity());
                dissolvePercent = 0.01f;
            }

            var enemyParts = FindAllChildren(gameObject.transform);
            enemyParts = GetChildObjectsWithTag("KillablePiece", enemyParts);
            if(isDying){
                for(int i = 0; i<enemyParts.Count; i++){
                    var renderer = enemyParts[i].GetComponent<Renderer>();
                
                    var rigidBody = enemyParts[i].GetComponent<Rigidbody>();
                    rigidBody.isKinematic = false;
                

                    renderer.material.SetFloat("_DissolveAmmount", dissolvePercent);
                }
            }
        }
    }

    public List<GameObject> FindAllChildren(Transform transform)
    {
        List<GameObject> allChildren = new List<GameObject>(); 
        int len = transform.childCount;
 
        for (int i = 0; i < len; i++)
        {
            allChildren.Add(transform.GetChild(i).gameObject);

            if (transform.GetChild(i).transform.childCount > 0)
                allChildren.AddRange(FindAllChildren(transform.GetChild(i).transform));
        }

        return allChildren;
    }
 
    public List<GameObject> GetChildObjectsWithTag(string _tag, List<GameObject> allChildren)
    {
        List<GameObject> childrenWithTag = new List<GameObject>(); 

        foreach (GameObject child in allChildren)
        {
            if (child.tag == _tag)
                childrenWithTag.Add(child);
        }

        return childrenWithTag;
    }

    private IEnumerator KillEntity(){
        float time = 0.0f;
        
        var explosion = Instantiate(explosionObject, gameObject.transform.position,  gameObject.transform.rotation);
        explosion.transform.parent = gameObject.transform;
        AudioSources[1].Play();
        while(time < deathTime)
        {
            time += Time.deltaTime;
            if(dissolvePercent < 1) {
                dissolvePercent = time/deathTime;
            }
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
    }
}
