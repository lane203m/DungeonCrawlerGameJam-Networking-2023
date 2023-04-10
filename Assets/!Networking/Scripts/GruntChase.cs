using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class GruntChase : MonoBehaviour
{




    private UnityEngine.AI.NavMeshAgent Enemy;

    public GameObject Player;

    public float EnemySpeed;

    public bool pursuePlayer;
    public AudioSource[] AudioSources;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake(){
        Enemy = GetComponent<UnityEngine.AI.NavMeshAgent>();  
        Player = GameObject.Find("PlayerCapsule");
        AudioSources = GetComponents<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {    
        if(Player){
            
            if(!pursuePlayer){
                RaycastHit hit1;
                RaycastHit hit2;
                RaycastHit hit3;
                RaycastHit hit4;

                Vector3 fwd = transform.TransformDirection(Vector3.forward);
                Vector3 bkd = transform.TransformDirection(-Vector3.forward);
                Vector3 rgt = transform.TransformDirection(Vector3.right);
                Vector3 lft = transform.TransformDirection(-Vector3.right);
                
                float distance = Vector3.Distance(transform.position, Player.transform.position);

                Physics.Raycast(transform.position, fwd, out hit1, 999);
                if(hit1.transform?.gameObject?.tag == "Player" ){
                    pursuePlayer = true;
                }

                Physics.Raycast(transform.position, bkd, out hit2, 999);
                if(hit2.transform?.gameObject?.tag == "Player"  ){
                    pursuePlayer = true;
                }

                Physics.Raycast(transform.position, rgt, out hit3, 999);
                if(hit3.transform?.gameObject?.tag == "Player"  ){
                    pursuePlayer = true;
                }

                Physics.Raycast(transform.position, lft, out hit4, 999);
                if(hit4.transform?.gameObject?.tag == "Player"  ){
                    pursuePlayer = true;
                }

                if(pursuePlayer){
                    AudioSources[0].Play();
                }
            }

        
            if(pursuePlayer){
                Vector3 dirToPlayer = transform.position - Player.transform.position;
                Vector3 newPos = transform.position - dirToPlayer;

                Enemy.SetDestination(newPos);
                
            }
        }
    }
}
