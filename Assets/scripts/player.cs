using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour{

    public float speed = 10;
    public float gravity = 10;
    public float maxVelocityChange = 10;
    public float jumpheight = 2;
    public int point = 0;
    public int health;//interger of health the player has

    private bool grounded;
    private bool dead;
    private Transform playerTransform;
    private Rigidbody _rigidbody;//make our player move


    // Start is called before the first frame update
    void Start() {

        //player transformation
        playerTransform = GetComponent<Transform>();

        //get the rigid body
        _rigidbody = GetComponent<Rigidbody>();

        //alter the gravity of the rigid body
        _rigidbody.useGravity = false;

        //allow rotation of the rigid body
        _rigidbody.freezeRotation = true;
    }

    // Update is called once per frame
    void FixedUpdate(){
        //spinning the cube
        //Time.deltaTime this is time taken to update one frame "smoothen movement"
        playerTransform.Rotate(0,Input.GetAxis("Horizontal"),0);

        //how fast to move
        //new vector3(x,y,forward/back)
        Vector3 targetVelocity = new Vector3(0,0,Input.GetAxis("Vertical"));
        //make sure target velocity is relative to the world position
        targetVelocity = playerTransform.TransformDirection(targetVelocity);
        //add some speed to it
        targetVelocity = targetVelocity * speed;


        //make sure we aint going beyound ground
          //add boundaries to the body
        //get current velocity
        Vector3 velocity = _rigidbody.velocity;
        Vector3 velocityChange = targetVelocity - velocity;

        //lets clamp => basically in math grabs the current value and the min value and clamps in bten those
        // velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
        velocityChange.y = 0;

        _rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);

        if(Input.GetButton("Jump") && grounded == true){
            _rigidbody.velocity = new Vector3(velocity.x,calculateJump(),velocity.z);
        }
        //make body fall
        _rigidbody.AddForce(new Vector3(0,-gravity * _rigidbody.mass));
        grounded = false;




    }
    void Update(){
      if(health < 1){
        //load level
        Application.LoadLevel("subway surf scene");
      }

    }


    float calculateJump(){
        float jump = Mathf.Sqrt(2*jumpheight*gravity);
        return jump;
    }
 //add gravity
    void OnCollisionStay(){
        grounded = true;
    }

    //create coins and collect
    void OnTriggerEnter(Collider coin){//collider coin=> this to catch what it is colliding withe for the ontriggerenter to work
        if(coin.tag == "Coin"){
            point += 5;
            Destroy(coin.gameObject);
            print (point);

        }

    }

    }
