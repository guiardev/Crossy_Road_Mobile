using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AprendaUnity{

    public class PlayerController : MonoBehaviour{

        private gameController _gameController;
        private Animator animator;
        private Vector3 preDestiny, destiny;
        private bool isJumping;

        [Header("Settings Player")]
        public LayerMask whatIsObstacles;
        public LayerMask whatIsGround;
        public float speedJump;
        public int sizeBlock;

        #region functions unity

        // Start is called before the first frame update
        void Start(){
            _gameController = FindObjectOfType(typeof(gameController)) as gameController;
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update(){

            if(_gameController.currentState != GameState.GAMEPLAY){
                return;
            }

            InputController();
            MoverPlayer();
        }

        private void OnCollisionEnter(Collision col){

            if(_gameController.currentState != GameState.GAMEPLAY){ return; }

            switch (col.gameObject.tag){

                case "Collectible":
                    col.gameObject.SendMessage("Collect", SendMessageOptions.DontRequireReceiver);
                break;

                case "Danger":
                    //Debug.Log("OnTriggerEnter");
                    Die();
                break;
            }
        }

        // private void OnTriggerEnter(Collision col){
        //     switch (col.gameObject.tag){

        //         case "Collectible":
        //             col.gameObject.SendMessage("Collect", SendMessageOptions.DontRequireReceiver);
        //         break;

        //         case "Danger":
        //             Debug.Log("OnTriggerEnter");
        //             Die();
        //         break;
        //     }
        // }

        #endregion

        #region my functions

        void InputController(){ // fazer o player olhar para o lado que ele se mover

            if(isJumping == true){
                return;
            }

            if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)){
                preDestiny = transform.position + new Vector3(0, 0, sizeBlock);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                PreJump();
            }else if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)){
                preDestiny = transform.position - new Vector3(0, 0, sizeBlock);
                transform.rotation = Quaternion.Euler(0, 180, 0);
                PreJump();
            }else if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)){
                preDestiny = transform.position - new Vector3(sizeBlock, 0, 0);
                transform.rotation = Quaternion.Euler(0, -90, 0);
                PreJump();
            }else if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)){
                preDestiny = transform.position + new Vector3(sizeBlock, 0, 0);
                transform.rotation = Quaternion.Euler(0, 90, 0);
                PreJump();
            }
        }

        public void PreJump(){

            RaycastHit hit;
            Physics.Raycast(transform.position + new Vector3(0, 5, 0), transform.forward, out hit, sizeBlock, whatIsObstacles);

            if(hit.collider == null){
                //destiny = preDestiny;
                isJumping = true;
                animator.SetTrigger("jump");
            }
        }

        void Jump(){
            destiny = preDestiny;
            _gameController.PlayFX(_gameController.fxJump[Random.Range(0, _gameController.fxJump.Length)]);
            //_gameController.PlayFX(_gameController.fxJump[1]);
        }

        void OnJumpComplete(){
            isJumping = false;

            RaycastHit hit;
            Physics.Raycast(transform.position + new Vector3(0, 5, 0), Vector3.down, out hit, sizeBlock, whatIsGround);

            if(hit.collider != null){
                Debug.Log(hit.collider.gameObject.name);

                switch(hit.collider.gameObject.tag){
                    
                    case "End":
                        Debug.Log("fim do level");
                        _gameController.ChangeGameState(GameState.COMPLETELEVEL);
                    break;

                    case "Trunk": 

                    break;

                    case "Water": 

                    break;
                }
            }
        }

        void MoverPlayer(){
            transform.position = Vector3.MoveTowards(transform.position, destiny, speedJump * Time.deltaTime);
        }

        void Die(){
            _gameController.ChangeGameState(GameState.GAMEOVER);
            _gameController.PlayFX(_gameController.fxHit);
            animator.SetTrigger("die");
        }

        public void TouchComando(string tecla){

            if(isJumping == true){
                return;
            }

            switch (tecla){

                case "W":
                    preDestiny = transform.position + new Vector3(0, 0, sizeBlock);
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    PreJump();
                break;

                case "A":
                    preDestiny = transform.position - new Vector3(sizeBlock, 0, 0);
                    transform.rotation = Quaternion.Euler(0, -90, 0);
                    PreJump();
                break;

                case "D":
                    preDestiny = transform.position + new Vector3(sizeBlock, 0, 0);
                    transform.rotation = Quaternion.Euler(0, 90, 0);
                    PreJump();
                break;

                case "S":
                    preDestiny = transform.position - new Vector3(0, 0, sizeBlock);
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    PreJump();
                break;
            }
        }

        #endregion

    }
}