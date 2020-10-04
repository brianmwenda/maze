using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dollplayer : MonoBehaviour
{
  public Collider MainCollider;
  public Collider[] AllCollider;

    // Start is called before the first frame update
    void Awake()
    {
        MainCollider = GetComponent<Collider>();
        AllCollider = GetComponentsInChildren<Collider>(true);
    }

    // Update is called once per frame
    public void DoRagdoll(bool isRagdoll)
    {
        foreach (var col in AllCollider){
          col.enabled = isRagdoll;
        }
        MainCollider.enabled = isRagdoll;
        GetComponent<Rigidbody>().useGravity = !isRagdoll;
        GetComponent<Animator>().enabled = !isRagdoll;
    }
}
