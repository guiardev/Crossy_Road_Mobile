using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AprendaUnity;

public class collectibles : MonoBehaviour{

    private gameController _gameController;

    public typeItem item;
    public int value;

    // Start is called before the first frame update
    void Start(){
        _gameController = FindObjectOfType(typeof(gameController)) as gameController;
    }

    // Update is called once per frame
    void Update(){
        
    }

    #region My Functions

    private void Collect(){
        //Debug.Log("Collect");
        _gameController.collectItem(item, value);
        Destroy(this.gameObject);
    }

    #endregion
}