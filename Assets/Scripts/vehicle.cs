using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AprendaUnity;

public class vehicle : MonoBehaviour{

    private mapProcedural _map;

    public int speed, sizeVehicle;

    // public int posLeft, posRight;

    // Start is called before the first frame update
    void Start(){
        _map = FindObjectOfType(typeof(mapProcedural)) as mapProcedural;
        speed *= -1;
    }

    // Update is called once per frame
    void Update(){
        transform.Translate(speed * Time.deltaTime, 0, 0);

        switch(transform.rotation.eulerAngles.y){

            case 0:

                if(transform.position.x <= _map.posLeft - sizeVehicle){
                    transform.position = new Vector3(_map.posRight, transform.position.y, transform.position.z);
                }

            break;

            case 180:

                if(transform.position.x >= _map.posRight + sizeVehicle){
                    transform.position = new Vector3(_map.posLeft, transform.position.y, transform.position.z);
                }

            break;
        }
    }
}