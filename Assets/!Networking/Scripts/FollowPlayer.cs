using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player_Movement))]
public class FollowPlayer : MonoBehaviour
{
    public Player_Movement movementHandler;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake(){
        movementHandler = GetComponent<Player_Movement>();
    }



    // Update is called once per frame
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

        Physics.Raycast(transform.position, fwd, out hit1, 999);
        if(hit1.transform?.gameObject?.tag == "Player"){
            movementHandler.MoveForward();
        }

        Physics.Raycast(transform.position, bkd, out hit2, 999);
        if(hit2.transform?.gameObject?.tag == "Player"){
            movementHandler.MoveBackward();
        }

        Physics.Raycast(transform.position, rgt, out hit3, 999);
        if(hit3.transform?.gameObject?.tag == "Player"){
            movementHandler.MoveRight();
        }

        Physics.Raycast(transform.position, lft, out hit4, 999);
        if(hit4.transform?.gameObject?.tag == "Player"){
            movementHandler.MoveLeft();
        }
    }
}
