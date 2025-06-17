using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temp : MonoBehaviour{

    public GameObject prefab;
    public Transform posStart;
    public float distance;
    public int block;

    // Start is called before the first frame update
    void Start(){
        
        float car = (block * 20) / distance;
        //Debug.Log(posStart.position.x + (block * 20));
        Vector3 posIni = new Vector3(posStart.position.x + distance, posStart.position.y, posStart.position.z);

        for (int i = 0; i < car; i++){

            Instantiate(prefab, posIni, transform.localRotation);

            posIni += new Vector3(distance, 0, 0);
        }
    }

    // Update is called once per frame
    void Update(){
        
    }
}
