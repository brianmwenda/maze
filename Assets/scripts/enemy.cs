using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public enum State{
        LOOKFOR,
        GOTO,
        ATTACK
    }

    public State CurState;
    public float Speed = 6.5f;
    public float gotodistance = 5;
    public float attackdistance = 2;
    public float attackTimer = 2;
    public Transform Target;
    public string PlayerTag = "Player";//this is the player not enemy
    public float CurTime;

    private player playerScript;

    // Start is called before the first frame update
    IEnumerator Start(){
      //this is our Start
      Target = GameObject.FindGameObjectWithTag(PlayerTag).transform;

      CurTime =  attackTimer;

      if(Target != null){
        playerScript = Target.GetComponent<player>();
      }
        while(true){

          //this is our update

            switch(CurState){
                case State.LOOKFOR:
                  Lookfor();
                  break;
                case State.GOTO:
                  Goto();
                  break;
                case State.ATTACK:
                  Attack();
                  break;

            }
            yield return 0;
        }
    }

   void Lookfor(){
     print("we are in look for");

     if(Vector3.Distance(Target.position, transform.position) < gotodistance){
       CurState = State.GOTO;
     }
   }

   void Goto(){
     print("we are in got to");

     //make enemy look at the player
     transform.LookAt(Target);

     //Raycast
     Vector3 fwd = transform.TransformDirection(Vector3.forward);//our object`s forward direction
     RaycastHit buddy;//specify what our ray cast wil be
     if(Physics.Raycast(transform.position, fwd, out buddy)){//if we are hitting something in line of sight then
       if(buddy.transform.tag != PlayerTag){//if what we are hitting isnt player tag then
         CurState = State.LOOKFOR;//break to look for the player state
         return;
       }
     }

     if (Vector3.Distance(Target.position, transform.position) > attackdistance){
       transform.position = Vector3.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);
     }
     else{
       CurState = State.ATTACK;
     }
   }

   void Attack(){
     print("we are in attack");

     //make enemy look at the player
     transform.LookAt(Target);

     CurTime = CurTime - Time.deltaTime;

     if(CurTime < 0){
       playerScript.health--;
       CurTime = attackTimer;

     }
     else if (Vector3.Distance(Target.position, transform.position) > attackdistance){
       CurState = State.GOTO;
     }

   }
}
