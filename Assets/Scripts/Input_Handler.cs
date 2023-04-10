using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player_Movement))]
public class Input_Handler : MonoBehaviour
{
    public KeyCode forward = KeyCode.W;
    public KeyCode left = KeyCode.A;
    public KeyCode backward = KeyCode.S;
    public KeyCode right = KeyCode.D;
    public KeyCode rotateLeft = KeyCode.Q;
    public KeyCode rotateRight = KeyCode.E;

    public Player_Movement movementHandler;

    public Queue<KeyCode> movementQueue;

    private void Awake(){
        movementHandler = GetComponent<Player_Movement>();
        movementQueue = new Queue<KeyCode>();
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.DrawRay(collision.contacts[0].point, collision.contacts[0].normal, Color.green, 2, false);
    }

    void Update()
    {
        RaycastHit hit1;
        RaycastHit hit2;
        RaycastHit hit3;
        RaycastHit hit4;

        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Vector3 bkd = transform.TransformDirection(-Vector3.forward);
        Vector3 rgt = transform.TransformDirection(Vector3.right);
        Vector3 lft = transform.TransformDirection(-Vector3.right);

        if(movementQueue.Count>0 && movementHandler.atRest){

            var nextStep = movementQueue.Dequeue();

            if(nextStep == KeyCode.W){
                Physics.Raycast(transform.position, fwd, out hit1, 6);
                if(hit1.transform?.gameObject?.tag != "GameWall"){
                    movementHandler.MoveForward();
                }
            }
            if(nextStep == KeyCode.A){
                Physics.Raycast(transform.position, lft, out hit2, 6);
                if(hit2.transform?.gameObject?.tag != "GameWall"){
                    movementHandler.MoveLeft();
                }
            }
            if(nextStep == KeyCode.S){
                Physics.Raycast(transform.position, bkd, out hit3, 6);
                if(hit3.transform?.gameObject?.tag != "GameWall"){
                    movementHandler.MoveBackward();
                }
            }
            if(nextStep == KeyCode.D){
                Physics.Raycast(transform.position, rgt, out hit4, 6);
                if(hit4.transform?.gameObject?.tag != "GameWall"){
                    movementHandler.MoveRight();
                }
            }
            if(nextStep == KeyCode.Q){
                movementHandler.RotateLeft();
            }
            if(nextStep == KeyCode.E){
                movementHandler.RotateRight();
            }
        }

        if(Input.GetKeyDown(forward) && movementQueue.Count < 3){
            movementQueue.Enqueue(forward);
        }
        if(Input.GetKeyDown(left) && movementQueue.Count < 3){
            movementQueue.Enqueue(left);
        }
        if(Input.GetKeyDown(backward) && movementQueue.Count < 3){
            movementQueue.Enqueue(backward);
        }
        if(Input.GetKeyDown(right) && movementQueue.Count < 3){
            movementQueue.Enqueue(right);
        }
        if(Input.GetKeyDown(rotateLeft) && movementQueue.Count < 3){
            movementQueue.Enqueue(rotateLeft);
        }
        if(Input.GetKeyDown(rotateRight) && movementQueue.Count < 3){
            movementQueue.Enqueue(rotateRight);
        }
    }
}
