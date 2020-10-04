using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coins : MonoBehaviour
{

    public float speed = 5;
    public int randomnumber;

    // Start is called before the first frame update
    void Start()
    {
        randomnumber =  Random.Range(1, (int)speed);//cast int to speed
        
    }

    // Update is called once per frame
    void Update()
    {  
        //spin coins
        gameObject.transform.Rotate(Vector3.up * (randomnumber * 1.0f));//change to float "trick"
        
    }
}
