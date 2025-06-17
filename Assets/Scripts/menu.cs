using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class menu : MonoBehaviour{

    private int idCharacterCurrent;

    public Mesh[] characters;
    public TMP_Text priceTMP, btnText, coinTotalTMP;
    public GameObject lookPlayer;
    public Sprite[] imgBtn;
    public Image btnImg;

    public MeshFilter character, characterLeft, characterRight;
    public int[] pricePlayer;
    public bool[] playerOpen;

    #region Functions Unity

    // Start is called before the first frame update
    void Start(){

        if(PlayerPrefs.GetInt("idStageCurrent") == 0){
            PlayerPrefs.SetInt("idStageCurrent", 1);
        }

        //selectedPlayer(idCharacterCurrent);

        idCharacterCurrent = PlayerPrefs.GetInt("idCharacterCurrent");

        character.mesh = characters[idCharacterCurrent];
        int idTemp = idCharacterCurrent - 1;

        if(idTemp < 0){
            idTemp = characters.Length - 1;
        }

        characterLeft.mesh = characters[idTemp];

        idTemp = idCharacterCurrent + 1;

        if(idTemp >= characters.Length){
            idTemp = 0;
        }

        if(playerOpen[idCharacterCurrent] == true){
            lookPlayer.SetActive(false);
        }else{
            priceTMP.text = pricePlayer[idCharacterCurrent].ToString();
        }

        coinTotalTMP.text = PlayerPrefs.GetInt("coinCollectibles").ToString("N0");
    }   

    // Update is called once per frame
    void Update(){
        
    }

    #endregion

    #region My Funtions

    void selectedPlayer(int i){

        idCharacterCurrent += i;

        if(idCharacterCurrent < 0){
            idCharacterCurrent = characters.Length - 1;
        }else if(idCharacterCurrent >= characters.Length){
            idCharacterCurrent = 0;
        }

        //Debug.Log("idCharacterCurrent: " + idCharacterCurrent);
        
        if(playerOpen[idCharacterCurrent] == true){
            lookPlayer.SetActive(false);
            btnImg.sprite = imgBtn[1];
            btnText.text = "Jogar";
        }else {
            priceTMP.text = pricePlayer[idCharacterCurrent].ToString();
            lookPlayer.SetActive(true);
            btnImg.sprite = imgBtn[0];
            btnText.text = "Liberar";
        }

        character.mesh = characters[idCharacterCurrent];

        int idLeft = idCharacterCurrent - 1;
        if(idLeft < 0){
            idLeft = characters.Length - 1;
        }

        characterLeft.mesh = characters[idLeft];

        int idRight = idCharacterCurrent + 1;
        if(idRight > characters.Length - 1){
            idRight = 0;
        }

        characterRight.mesh = characters[idRight];

        // Debug.Log("idCharacterCurrent " + idCharacterCurrent);
        // Debug.Log("idLeft " + idLeft);
        // Debug.Log("idRight " + idRight);
    }

    public void btnAction(){

        if(playerOpen[idCharacterCurrent] == true){
            PlayerPrefs.SetInt("idCharacterCurrent", idCharacterCurrent);
            SceneManager.LoadScene("GamePlay");
        }else{
            buyPlayer();
        }
    }

    void buyPlayer(){

        int totalCoin = PlayerPrefs.GetInt("coinCollectibles");

        if(totalCoin >= pricePlayer[idCharacterCurrent]){
            totalCoin -= pricePlayer[idCharacterCurrent];
            PlayerPrefs.SetInt("coinCollectibles", totalCoin);
            coinTotalTMP.text = totalCoin.ToString("N0");
            playerOpen[idCharacterCurrent] = true;
            //selectedPlayer(idCharacterCurrent);

            lookPlayer.SetActive(false);
            btnImg.sprite = imgBtn[1];
            btnText.text = "Jogar";
        }else {
            Debug.Log("Not money ");
        }

    }

    #endregion

}