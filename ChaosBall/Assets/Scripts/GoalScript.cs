using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    public bool isSolve = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject colliderWith = other.gameObject;
        if(colliderWith.tag == gameObject.tag)
        {
            isSolve = false;
            GetComponent<Light>().enabled = false;
            Destroy(colliderWith);
        }
    }
}
