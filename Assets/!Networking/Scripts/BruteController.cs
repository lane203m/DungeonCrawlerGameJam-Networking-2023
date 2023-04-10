using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player_Movement))]
[RequireComponent(typeof(EnemyDestroyer))]
public class BruteController : MonoBehaviour
{
    public GameObject Player;
    public bool sawPlayer;
    public bool correctingPos;
    public Queue<KeyCode> movementQueue;
    public Player_Movement movementHandler;
    public EnemyDestroyer enemyDestroyer;
    private Renderer renderer;
    List<GameObject> childParts;
    public AudioSource[] AudioSources;
    public int turnOffGlow = 1;
    public bool waitingForAttack = false;

    public bool canPlaySound = true;

    // Start is called before the first frame update
    void Start()
    {

    }
    private void Awake(){
        Player = GameObject.Find("PlayerCapsule");
        movementHandler = GetComponent<Player_Movement>();
        enemyDestroyer = GetComponent<EnemyDestroyer>();

        movementQueue = new Queue<KeyCode>();
        renderer = this.gameObject.GetComponent<Renderer>();

        AudioSources = GetComponents<AudioSource>();

        childParts = enemyDestroyer.FindAllChildren(gameObject.transform);
        childParts = enemyDestroyer.GetChildObjectsWithTag("KillablePiece", childParts);

        for(int i = 0; i<childParts.Count; i++){
            var renderer = childParts[i].GetComponent<Renderer>();
        
            renderer.material.SetInt("_TurnOff", turnOffGlow);
        }
    }
    // Update is called once per frame
    void Update()
    {    
        if(waitingForAttack && turnOffGlow == 1){
            turnOffGlow = 0;
            for(int i = 0; i<childParts.Count; i++){
                var renderer = childParts[i].GetComponent<Renderer>();
            
                renderer.material.SetInt("_TurnOff", turnOffGlow);
            }
        }

        if(Player){
            if(sawPlayer && canPlaySound){
                AudioSources[0].Play();
                canPlaySound = false;
            }
            RaycastHit hit1;
            RaycastHit hit2;
            RaycastHit hit3;
            RaycastHit hit4;

            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            Vector3 bkd = transform.TransformDirection(-Vector3.forward);

            Physics.Raycast(transform.position, fwd, out hit1, 6);
            Physics.Raycast(transform.position, bkd, out hit2, 10);
            Physics.Raycast(transform.position, fwd, out hit3, 10);
            Physics.Raycast(transform.position, bkd, out hit4, 6);

            if(!waitingForAttack && hit3.transform?.gameObject?.tag == Player?.tag || hit2.transform?.gameObject?.tag == Player?.tag){
                waitingForAttack = true;
                StartCoroutine( WaitForAttack() );
            }

            if(hit1.transform?.gameObject?.tag == "GameWall" && movementHandler.atRest && !correctingPos){
                correctingPos = true;
                movementQueue.Clear();
                
                movementQueue.Enqueue(KeyCode.Q);

                sawPlayer=false;
                canPlaySound = true;
            } else if (sawPlayer && movementHandler.atRest && !correctingPos) {
                movementQueue.Enqueue(KeyCode.W);
            }
            
            if(movementQueue?.Count == 0){
                correctingPos = false;
            }

            if(movementQueue?.Count>0 && movementHandler.atRest && !correctingPos){
                var nextStep = movementQueue.Dequeue();

                if(nextStep == KeyCode.W){
                    Physics.Raycast(transform.position, fwd, out hit1, 6);
                    if(hit1.transform?.gameObject?.tag != "GameWall"){
                        movementHandler.MoveForward();
                    }
                }
                if(nextStep == KeyCode.Q){
                    movementHandler.RotateLeft();
                }
                if(nextStep == KeyCode.E){
                    movementHandler.RotateRight();
                }
            } else if(movementQueue?.Count>0 && movementHandler.atRest && correctingPos){
                var nextStep = movementQueue.Dequeue();

                if(nextStep == KeyCode.Q){
                    movementHandler.RotateLeft();
                }
                if(nextStep == KeyCode.E){
                    movementHandler.RotateRight();
                }
            }
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if(!waitingForAttack && collision.gameObject.tag == "ScannerHitbox"){
            waitingForAttack = true;
            StartCoroutine( WaitForAttack() );
        }
    }

    private IEnumerator WaitForAttack()
    {
        if(waitingForAttack){
            yield return new WaitForSeconds( 0.25f );
            sawPlayer = true;
            waitingForAttack = false;
        }
    }
}
