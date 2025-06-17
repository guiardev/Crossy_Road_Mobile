using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace AprendaUnity {

    public enum typeItem{
        COIN, TOMATE
    }

    public enum GameState{
        GAMEPLAY, GAMEOVER, COMPLETELEVEL, MENU
    }

    public class gameController : MonoBehaviour{

        private Camera cam;
        private int coinCollectibles, tomateCollectibles, timeCurrent;
        private bool isFollowCam = true;

        public Mesh[] characters;
        public GameState currentState;
        public Transform playerTransform;

        // public int idStageCurrent;

        [Header("Settings level")]
        public int timeMax;
        public int bonusTimeSeg;
        public int sizeBlock;
        public int addTimeTomate;

        //[Space(10)]
        [Header("Settings Camera")]
        public float speedCam;
        public int margem;

        [Header("Limit level")]
        public Transform limitLeft;
        public Transform limitRight;
        public Transform limitCenter;

        [Header("HUD")]
        public GameObject hubGamePlay; 
        public GameObject hubGameOver;
        public GameObject hubStageComplete;
        [Space(10)]
        public TMP_Text coinTMP;
        public TMP_Text timeTMP;
        public TMP_Text stageCurrentTMP;

        [Header("HUD stage Complete")]
        public TMP_Text coinTotalTMP;
        public TMP_Text levelStageCompletoTMP;
        public TMP_Text bonusTimeTMP;
        public TMP_Text coinCollectiblesTMP;
        public TMP_Text timeCurrentTMP;

        public GameObject touchComandos;

        public GameObject btnPlay;
        public GameObject btnBack;

        [Header("FX")]
        public AudioSource fx;
        public AudioClip fxCollect;
        public AudioClip fxHit;
        public AudioClip[] fxJump;
        public AudioClip fxWater;

        #region  Functions Unity
        // Start is called before the first frame update
        void Start(){

            Application.targetFrameRate = 60;

            hubGameOver.SetActive(false);
            hubStageComplete.SetActive(false);
            hubGamePlay.SetActive(true);
            touchComandos.SetActive(true);

            cam = Camera.main;
            timeCurrent = timeMax;
            StartCoroutine("timeLevel");

            coinTotalTMP.text = PlayerPrefs.GetInt("coinCollectibles").ToString("N0");

            stageCurrentTMP.text = "Fase <color=#FFFF00>" + PlayerPrefs.GetInt("idStageCurrent").ToString() + "</color>";

            setSkinPlayer();
        }

        // Update is called once per frame
        void Update(){
            
        }

        private void LateUpdate(){

            if (currentState != GameState.GAMEPLAY) {
                return;
            };

            CameraController();

            limitLeft.position = new Vector3(limitLeft.position.x, limitLeft.position.y, playerTransform.position.z);
            limitRight.position = new Vector3(limitRight.position.x, limitRight.position.y, playerTransform.position.z);
            limitCenter.position = new Vector3(playerTransform.position.x, limitCenter.position.y, limitCenter.position.z);
        }

        #endregion

        #region My Functions

        public void collectItem(typeItem item, int value){

            switch(item){

                case typeItem.COIN:
                    coinCollectibles += value;
                    UpdateHud();
                break;

                case typeItem.TOMATE:

                    tomateCollectibles += value;
                    timeCurrent += addTimeTomate;

                    if(timeCurrent > timeMax){
                        timeCurrent = timeMax;
                    }

                    UpdateHud();
                break;
            }

            PlayFX(fxCollect);
        }

        void UpdateHud(){
            coinTMP.text = coinCollectibles.ToString("N0");
            timeTMP.text = timeCurrent.ToString();
        }

        IEnumerator timeLevel(){
            timeTMP.text = timeCurrent.ToString();
            yield return new WaitForSeconds(1);
            timeCurrent -= 1;
            StartCoroutine("timeLevel");
        }

        void CameraController(){

            if (isFollowCam == false) { return; }

            float posX = Mathf.Clamp(playerTransform.position.x, limitLeft.position.x + margem, limitRight.position.x - margem);
            Vector3 destinyCamera = new Vector3(posX, playerTransform.position.y, playerTransform.position.z);
            cam.transform.position = Vector3.Lerp(cam.transform.position, destinyCamera, speedCam * Time.deltaTime);
        }

        public void ChangeGameState(GameState newState){
            currentState = newState;

            switch(currentState){

                case GameState.COMPLETELEVEL:

                    StageComplete();
                    touchComandos.SetActive(false);
                break;

                case GameState.GAMEOVER:
                    isFollowCam = false;
                    //Debug.Log("GameOver");
                    StopCoroutine("timeLevel");
                    hubGameOver.SetActive(true);
                    touchComandos.SetActive(false);
                break;
            }
        }

        public void PlayFX(AudioClip clip){
            fx.PlayOneShot(clip);
        }

        public void loadingScene(string nameScane){
            SceneManager.LoadScene(nameScane);
        }

        void StageComplete(){

            int idStageCurrent = PlayerPrefs.GetInt("idStageCurrent");

            isFollowCam = false;
            //Debug.Log("fim do level");
            StopCoroutine("timeLevel");

            // Debug.Log("bonus " + bonus);

            //coinTotalTMP.text = coinCollectibles.ToString();
            timeCurrentTMP.text = timeCurrent.ToString();
            coinCollectiblesTMP.text = coinCollectibles.ToString();

            levelStageCompletoTMP.text = "Level <color=#ffff00>" + PlayerPrefs.GetInt("idStageCurrent").ToString() + "</color> Completo";

            // int bonus = Mathf.RoundToInt(timeCurrent / bonusTimeSeg);
            // bonusTimeTMP.text = "Bonus de Tempo: <color=#00FFFF>" + bonus.ToString() + "</color>";
            // coinCollectibles *= bonus;

            // PlayerPrefs.SetInt("coinCollectibles", PlayerPrefs.GetInt("coinCollectibles") + coinCollectibles);

            hubGamePlay.SetActive(false);
            hubStageComplete.SetActive(true);

            StartCoroutine("generateBonusTime");

            PlayerPrefs.SetInt("idStageCurrent", idStageCurrent += 1);
        }

        IEnumerator generateBonusTime(){ // criando uma contagem recreciva

            yield return new WaitForSeconds(1);

            int temp = 0, bonusTime = 0;
            for (int i = timeCurrent; i >= 0; i--){
                yield return new WaitForSeconds(0.05f);
                timeCurrentTMP.text = i.ToString();
                temp += 1;
                if(temp == 10){
                    temp = 0;
                    bonusTime += 1;
                    bonusTimeTMP.text = "Bonus de Tempo: <color=#00FFFF>" + bonusTime.ToString() + "</color>";
                }
            }

            //int bonus = Mathf.RoundToInt(timeCurrent / bonusTimeSeg);
            // bonusTimeTMP.text = "Bonus de Tempo: <color=#00FFFF>" + bonus.ToString() + "</color>";

            coinCollectibles *= bonusTime;
            coinCollectiblesTMP.text = coinCollectibles.ToString();

            int tempCoin = PlayerPrefs.GetInt("coinCollectibles");
            for (int i = coinCollectibles; coinCollectibles >= 0; coinCollectibles--){
                tempCoin += 1;
                coinTotalTMP.text = tempCoin.ToString("N0");
                yield return new WaitForSeconds(0.05f);
            }

            PlayerPrefs.SetInt("coinCollectibles", PlayerPrefs.GetInt("coinCollectibles") + coinCollectibles);

            btnPlay.SetActive(true);
            btnBack.SetActive(true);
        }

        void setSkinPlayer(){
            playerTransform.GetComponentInChildren<MeshFilter>().mesh = characters[PlayerPrefs.GetInt("idCharacterCurrent")];
        }

        #endregion
    }
}