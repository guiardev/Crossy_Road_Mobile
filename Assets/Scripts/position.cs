using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class position : MonoBehaviour{

    public Vector3 positionFree;
    //public Vector3 positionPlayer;

    // Start is called before the first frame update
    void Start(){
       // positionPlayer = new Vector3(0, 20, 0);       
    }

    // Update is called once per frame
    void Update(){
        positionFree = transform.position;
        Debug.Log("position objeto: " + positionFree);
        //transform.position = positionPlayer;
    }
}
