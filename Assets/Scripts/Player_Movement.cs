using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float speed = 10f;
    public float rotation = 500f;

    /*movementQueue = new {
        Vector3 targetGrid;
        Vector3 prevTargetGrid;
        Vector3 targetRotation; 
    }*/

    public Vector3 targetGrid;
    public Vector3 prevTargetGrid;
    public Vector3 targetRotation;
    public AudioSource[] AudioSources;
    public bool makeWalkSounds = false;

    public bool atRest {
        get {
            if ((Vector3.Distance(transform.position, targetGrid) < 0.05f) &&
                (Vector3.Distance(transform.eulerAngles, targetRotation) < 0.05f)) 
                return true;
            else    
                return false;
        }
    }
    
    //ArrayList commands = {};

    public void RotateLeft(){
        if(makeWalkSounds) AudioSources[4].Play();
        if(atRest) {targetRotation -= Vector3.up * 90f;}
    }
    public void RotateRight(){
        if(makeWalkSounds) AudioSources[4].Play();
        if(atRest) {targetRotation += Vector3.up * 90f;}
    }

    public void MoveForward() {
        if(makeWalkSounds) AudioSources[3].Play();
        if(atRest) {targetGrid += transform.forward * 5;}
    }
    public void MoveBackward() {
        if(makeWalkSounds) AudioSources[3].Play();
        if(atRest) {targetGrid -= transform.forward * 5;}
    }
    public void MoveLeft() {
        if(atRest) {
            if(makeWalkSounds) AudioSources[3].Play();
            targetGrid -= transform.right * 5;
        }
    }
    public void MoveRight() {
        if(atRest){ 
            if(makeWalkSounds) AudioSources[3].Play();
            targetGrid += transform.right * 5;
        }
    }
    
    private void FixedUpdate(){
        MovePlayer();
    }

    void MovePlayer(){
        prevTargetGrid = targetGrid;

        Vector3 target = targetGrid;

        if(targetRotation.y > 270f && targetRotation.y < 361f) targetRotation.y = 0f;
        if(targetRotation.y < 0f) targetRotation.y = 270f;
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(targetRotation), Time.deltaTime * rotation);
    }

    // Start is called before the first frame update
    void Start()
    {
        targetGrid = transform.position;
        targetRotation = transform.eulerAngles;
    }

    void Awake(){
        AudioSources = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if(atRest){

        }*/
    }
}
