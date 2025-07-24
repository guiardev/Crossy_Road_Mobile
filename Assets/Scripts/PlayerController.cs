using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AprendaUnity{

    public class PlayerController : MonoBehaviour{

        private gameController _gameController;
        private Animator animator;
        private Vector3 preDestiny, destiny;
        private RaycastHit _hit;
        private bool isJumping;
        //public bool isJumping;

        [Header("Settings Player")]
        public LayerMask whatIsObstacles;
        public LayerMask whatIsGround;
        public float speedJump, cooldownTime = 1, lastMoveTime;
        public int sizeBlock;
        public bool canMove = true;

        #region functions unity

        // Start is called before the first frame update
        void Start(){
            _gameController = Object.FindFirstObjectByType(typeof(gameController)) as gameController;
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void FixedUpdate(){

            if(_gameController.currentState != GameState.GAMEPLAY){
                return;
            }

            // if(Time.fixedTime - lastMoveTime >= cooldownTime){
            // } 

            //InputController();
            MoverPlayer();
        }

        void OnTriggerEnter(Collider col){
            
            if (_gameController.currentState != GameState.GAMEPLAY) { return; }
            
            switch (col.gameObject.tag){

                case "Collectible":
                    col.gameObject.SendMessage("Collect", SendMessageOptions.DontRequireReceiver);
                break;

                case "Danger":
                    //Debug.Log("OnTriggerEnter Danger Die() --> col.gameObject.tag = " + col.gameObject.tag);
                    Die();
                break;
            }

        }

        #endregion

        #region my functions

        /*
        void InputController(){
                
            if (Input.GetKeyDown(KeyCode.W) && canMove){
                StartCoroutine(MovementCooldown());
                preDestiny = transform.position + new Vector3(0, 0, sizeBlock);
                transform.rotation = Quaternion.Euler(0, 0, 0); // fazer o player olhar para o lado que ele se mover
                PreJump();
            }else if (Input.GetKeyDown(KeyCode.S) && canMove){
                StartCoroutine(MovementCooldown());
                preDestiny = transform.position - new Vector3(0, 0, sizeBlock);
                transform.rotation = Quaternion.Euler(0, 180, 0);
                PreJump();
            }else if (Input.GetKeyDown(KeyCode.A) && canMove){
                StartCoroutine(MovementCooldown());
                preDestiny = transform.position - new Vector3(sizeBlock, 0, 0);
                transform.rotation = Quaternion.Euler(0, -90, 0);
                PreJump();
            }else if (Input.GetKeyDown(KeyCode.D) && canMove){
                StartCoroutine(MovementCooldown());
                preDestiny = transform.position + new Vector3(sizeBlock, 0, 0);
                transform.rotation = Quaternion.Euler(0, 90, 0);
                PreJump();
            }
        }
        */


        public void PreJump(){

            //RaycastHit hit;

            Physics.Raycast(transform.position + new Vector3(0, 5, 0), transform.forward, out _hit, sizeBlock, whatIsObstacles);

            if(_hit.collider == null){
                 //destiny = preDestiny;
                isJumping = true;
                animator.SetTrigger("jump");
            }

            if (isJumping == true){
                isJumping = false;
            }
                
        }

        IEnumerator MovementCooldown(){
            canMove = false;
            yield return new WaitForSeconds(cooldownTime);
            canMove = true;
        }

        void Jump(){
            destiny = preDestiny;
            _gameController.PlayFX(_gameController.fxJump[Random.Range(0, _gameController.fxJump.Length)]);
            //_gameController.PlayFX(_gameController.fxJump[1]);
        }

        void OnJumpComplete(){
            isJumping = false;

            //RaycastHit hit;
            Physics.Raycast(transform.position + new Vector3(0, 5, 0), Vector3.down, out _hit, sizeBlock, whatIsGround);

            if(_hit.collider != null){
                Debug.Log(_hit.collider.gameObject.name);

                switch(_hit.collider.gameObject.tag){
                    
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
            Debug.Log(" Die() _gameController");
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

                    if(canMove){
                        StartCoroutine(MovementCooldown());
                        preDestiny = transform.position + new Vector3(0, 0, sizeBlock);
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                        PreJump();
                    }

                break;

                case "A":

                    if(canMove){
                        StartCoroutine(MovementCooldown());
                        preDestiny = transform.position - new Vector3(sizeBlock, 0, 0);
                        transform.rotation = Quaternion.Euler(0, -90, 0);
                        PreJump();
                    }

                break;

                case "D":

                    if(canMove){
                        StartCoroutine(MovementCooldown());
                        preDestiny = transform.position + new Vector3(sizeBlock, 0, 0);
                        transform.rotation = Quaternion.Euler(0, 90, 0);
                        PreJump();
                    }

                break;

                case "S":

                    if(canMove){
                        StartCoroutine(MovementCooldown());
                        preDestiny = transform.position - new Vector3(0, 0, sizeBlock);
                        transform.rotation = Quaternion.Euler(0, 180, 0);
                        PreJump();                       
                    }

                break;
            }
            
        }

        #endregion

    }
}