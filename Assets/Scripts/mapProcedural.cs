using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AprendaUnity{

    public class mapProcedural : MonoBehaviour{

        private gameController _gameController;
        private GameObject blockTemp, blockTempFree;
        private int sizeBlock, currentLine;

        public GameObject[] block;
        public GameObject blockEndLevel;
        public GameObject[] decoration, collectibles;
        public Transform blockStage, decorateStage;
        public Material materialEndStage;
        public int[] occupiesBlock;
        public int qtdBloco, qtdBlocoLimit, lineLevel, linesStart, percDecorate, percDecorateLimit , percCollerctibles;

        public bool[] canDecorate, canElevation;

        [Header("Configuration Cars")]
        [SerializeField] private float qtdCars;
        public GameObject carPrefab;

        public spanws[] _spanws;
        public int posLeft;
        public int posRight;
        public float distanceCars;
        
        // Start is called before the first frame update
        void Start(){

            _gameController = FindObjectOfType(typeof(gameController)) as gameController;

            sizeBlock = _gameController.sizeBlock;

            RuleProgressionLevel(PlayerPrefs.GetInt("idLevelCurrent"));

            qtdCars = Mathf.RoundToInt(((qtdBloco + qtdBlocoLimit) * 2 + 1) * sizeBlock / distanceCars);
            // Debug.Log("qtdCars " + qtdCars);

            posRight = (qtdBloco + qtdBlocoLimit) * sizeBlock;
            posLeft = posRight * -1;

            // for (int i = 0; i < qtdCars; i++){
            //     Debug.Log("Spawn Car");
            // }
            
            generateMap();
        }

        // Update is called once per frame
        void Update(){
            
        }

        #region My Functions

            void generateMap(){

                for (int i = 0; i < linesStart; i++){
                   generateStartLevel(i, 0);
                }

                currentLine = 1;

                for (int i = currentLine; i <= lineLevel; i++){
                    int idBlockLine = Random.Range(0, block.Length);
                    generateLine(currentLine, idBlockLine);
                    currentLine += occupiesBlock[idBlockLine];
                }

                for (int i = 0; i <= linesStart; i++){
                    int idBlockLine = 999;
                    generateLine(currentLine, idBlockLine);
                    currentLine += 1;
                }

                _gameController.limitLeft.position -= new Vector3((qtdBloco + 1) * sizeBlock, 0, 0);
                _gameController.limitRight.position += new Vector3((qtdBloco + 1) * sizeBlock, 0, 0);
                _gameController.limitCenter.position -= new Vector3(0, 0, linesStart * sizeBlock);
            }

            async void generateStartLevel(int i, int idBlock){

                blockTemp = Instantiate(block[idBlock], transform.position - new Vector3(0,0,i * sizeBlock), transform.localRotation, blockStage);

                if (canDecorate[idBlock] == true && i > 0){
                    setDecorate(blockTemp.transform, false);
                }

                // Gerar bloco para a esquerda
                int bl = 0;

                for (int lX = 1; lX <= qtdBloco; lX++){

                    blockTemp = Instantiate(block[idBlock], transform.position - new Vector3(lX * sizeBlock, 0, i * sizeBlock), transform.localRotation, blockStage);
                    bl = lX;

                    if (canDecorate[idBlock] == true){
                        setDecorate(blockTemp.transform, false);
                    }
                }

                // Gerar bloco limite a esquerda
                for (int lX = 1; lX <= qtdBlocoLimit; lX++){

                    bl++;
                    blockTemp = Instantiate(block[idBlock], transform.position - new Vector3(bl * sizeBlock, Random.Range(-5, 5), i * sizeBlock), transform.localRotation, blockStage);
                    blockTemp.GetComponentInChildren<Renderer>().material = materialEndStage;

                    if (canDecorate[idBlock] == true){
                        setDecorate(blockTemp.transform, true);
                    }
                }

                // Gerar bloco para a direita
                for (int lZ = 1; lZ <= qtdBloco; lZ++){
                    blockTemp = Instantiate(block[idBlock], transform.position + new Vector3(lZ * sizeBlock, 0, i * sizeBlock * -1), transform.localRotation, blockStage);

                    if (canDecorate[idBlock] == true){
                        setDecorate(blockTemp.transform, false);
                    }

                    bl = lZ;
                }

                // Gerar bloco limite a direita
                for (int lZ = 1; lZ <= qtdBlocoLimit; lZ++){

                    bl++;
                    blockTemp = Instantiate(block[idBlock], transform.position + new Vector3(bl * sizeBlock,  Random.Range(-5, 5), i * sizeBlock * -1), transform.localRotation, blockStage);
                    blockTemp.GetComponentInChildren<Renderer>().material = materialEndStage;

                    if (canDecorate[idBlock] == true){
                        setDecorate(blockTemp.transform, true);
                    }
                }
            }

            void generateLine(int i, int idBlock){

                float posY = 0;
                GameObject blockInstanciar = null;

                if (idBlock == 999){
                    blockEndLevel.transform.position = new Vector3(transform.position.x, transform.position.y + 20, transform.position.z);
                    blockInstanciar = blockEndLevel;
                }else{
                    blockInstanciar = block[idBlock];
                }

                // bloco central
                blockTemp = Instantiate(blockInstanciar, transform.position + new Vector3(0,0,i * sizeBlock), transform.localRotation, blockStage);

                if (idBlock != 999){
                    
                    if (canDecorate[idBlock] == true && i > 1){
                        setDecorate(blockTemp.transform, false);
                    }else if(canDecorate[idBlock] == false) {
                        setCollectibles(blockTemp.transform);
                    }
                }

                // Debug.Log("gerei linha");

                // Gerar bloco para a esquerda
                int bl = 0;

                for (int lX = 1; lX <= qtdBloco; lX++){

                    blockTemp = Instantiate(blockInstanciar, transform.position - new Vector3(lX * sizeBlock, 0, i * sizeBlock * -1), transform.localRotation, blockStage);
                    bl = lX;

                    if (idBlock != 999){
                    
                        if (canDecorate[idBlock] == true){
                            setDecorate(blockTemp.transform, false);
                        }else if(canDecorate[idBlock] == false) {
                            setCollectibles(blockTemp.transform);
                        }
                    }

                }

                // Gerar bloco limite a esquerda
                for (int lX = 1; lX <= qtdBlocoLimit; lX++){

                    bl++;

                    if (idBlock != 999 && canElevation[idBlock] == true){
                        posY = Random.Range(-5, 5);
                    }

                    blockTemp = Instantiate(blockInstanciar, transform.position - new Vector3(bl * sizeBlock, posY, i * sizeBlock * -1), transform.localRotation, blockStage);
                    blockTemp.GetComponentInChildren<Renderer>().material = materialEndStage;

                    if (idBlock != 999 && canDecorate[idBlock] == true){
                        setDecorate(blockTemp.transform, true);
                    }
                }

                // Gerar bloco para a direita
                for (int lZ = 1; lZ <= qtdBloco; lZ++){

                    blockTemp = Instantiate(blockInstanciar, transform.position + new Vector3(lZ * sizeBlock, 0, i * sizeBlock), transform.localRotation, blockStage);

                    if (idBlock != 999){
                    
                        if (canDecorate[idBlock] == true && i > 1){
                            setDecorate(blockTemp.transform, false);
                        }else if(canDecorate[idBlock] == false) {
                            setCollectibles(blockTemp.transform);
                        }
                    }

                    if (idBlock != 999 && canDecorate[idBlock] == true){
                        setDecorate(blockTemp.transform, false);
                    }

                    bl = lZ;
                }

                // Gerar bloco limite a direita
                for (int lZ = 1; lZ <= qtdBlocoLimit; lZ++){

                    if (idBlock != 999 && canElevation[idBlock] == true){
                        posY = Random.Range(-5, 5);
                    }

                    bl++;
                    blockTemp = Instantiate(blockInstanciar, transform.position + new Vector3(bl * sizeBlock,  posY, i * sizeBlock), transform.localRotation, blockStage);
                    blockTemp.GetComponentInChildren<Renderer>().material = materialEndStage;

                    if (idBlock != 999 && canDecorate[idBlock] == true){
                        setDecorate(blockTemp.transform, true);
                    }
                }

                // Debug.Log("canDecorate[idBlock]: " + canDecorate[idBlock]);
                // Debug.Log("idBlock: " + idBlock);
                
                if(idBlock != 999 && _spanws[idBlock].IsSpawn == true){
                    setCars(blockTemp, idBlock);
                }
            }

            void setCars(GameObject blockRef, int idBlock){

                bool isLeft = false;

                if(_spanws[idBlock].isReverse == true){

                    if(Random.Range(0, 100) < 50){
                        isLeft = false;
                    }else{
                        isLeft = true;
                    }
                }

                if(_spanws[idBlock].isTrem == true){
                    qtdCars = 1;
                }else{
                    qtdCars = Mathf.RoundToInt(((qtdBloco + qtdBlocoLimit) * 2 + 1) * sizeBlock / distanceCars);
                }

                Vector3 posIni = Vector3.zero;
                
                int speedLine = Random.Range(_spanws[idBlock].minSpeed, _spanws[idBlock].maxSpeed);

                switch (isLeft){

                    case true: // move da direita para esquerda

                        posIni = new Vector3(posLeft + distanceCars, blockRef.transform.position.y + 20, blockRef.transform.position.z);

                        if(_spanws[idBlock].isTrem == true){
                            posIni += new Vector3(0, 0, sizeBlock);
                        }

                        for (int i = 0; i < qtdCars; i++){

                            int idCar = Random.Range(0, _spanws[idBlock].prefabs.Length);

                            carPrefab = _spanws[idBlock].prefabs[idCar];

                            GameObject tempCar = Instantiate(carPrefab, posIni, transform.localRotation);
                            tempCar.GetComponent<vehicle>().speed = speedLine;
                            tempCar.transform.rotation = Quaternion.Euler(0, 180, 0);

                            posIni += new Vector3(distanceCars, 0, 0);
                        }

                    break;

                    case false: // move da esquerda para direita

                        posIni = new Vector3(posRight - distanceCars, blockRef.transform.position.y + 20, blockRef.transform.position.z);

                        if(_spanws[idBlock].isTrem == true){
                            posIni += new Vector3(0, 0, sizeBlock);
                        }

                        for (int i = 0; i < qtdCars; i++){

                            int idCar = Random.Range(0, _spanws[idBlock].prefabs.Length);

                            carPrefab = _spanws[idBlock].prefabs[idCar];

                            GameObject tempCar = Instantiate(carPrefab, posIni, transform.localRotation);
                            tempCar.GetComponent<vehicle>().speed = speedLine;
                            tempCar.transform.rotation = Quaternion.Euler(0, 180, 0);

                            posIni -= new Vector3(distanceCars, 0, 0);
                        }

                        if(_spanws[idBlock].isDupla == true){

                            posIni = new Vector3(posLeft + distanceCars, blockRef.transform.position.y + 20, blockRef.transform.position.z + sizeBlock);

                            speedLine = Random.Range(_spanws[idBlock].minSpeed, _spanws[idBlock].maxSpeed);

                            for (int i = 0; i < qtdCars; i++){

                                int idCar = Random.Range(0, _spanws[idBlock].prefabs.Length);
                                carPrefab = _spanws[idBlock].prefabs[idCar];

                                GameObject tempCar = Instantiate(carPrefab, posIni, transform.localRotation);
                                tempCar.GetComponent<vehicle>().speed = speedLine;

                                posIni += new Vector3(distanceCars, 0, 0);
                            }
                        }

                    break;
                }
                
            }

            void setDecorate(Transform t, bool isLimit){

                if (isLimit == false){

                    if (Rand() <= percDecorate){

                        GameObject temp = Instantiate(decoration[Random.Range(0, decoration.Length)]);
                        temp.transform.position = new Vector3(t.position.x, t.position.y + 20, t.position.z);
                        temp.transform.parent = decorateStage;

                    }else { // se nao tiver uma decoracao, ve a possibilidade de inserir um coletavel

                        setCollectibles(t);
                    }

                }else { // se for um bloco de limite da fase

                    if (Rand() <= percDecorateLimit){
                        GameObject temp = Instantiate(decoration[Random.Range(0, decoration.Length)]);
                        temp.transform.position = new Vector3(t.position.x, t.position.y + 20, t.position.z);
                        temp.transform.parent = decorateStage;
                    }
                }

            }

            void setCollectibles(Transform t){

                if(Rand() <= percCollerctibles){
                    GameObject temp = Instantiate(collectibles[Random.Range(0, collectibles.Length)]);
                    temp.transform.position = new Vector3(t.position.x, t.position.y + 20, t.position.z);
                    temp.transform.parent = decorateStage;
                }
            }

            int Rand(){
                int r = Random.Range(0, 100);
                return r;
            }

            void RuleProgressionLevel(int idStage){

                if (idStage > 10){
                    qtdBloco = 25;
                    lineLevel = 20;
                }else if(idStage > 5){
                    qtdBloco = 20;
                    lineLevel = 15;
                }else if(idStage > 3){
                    qtdBloco = 15;
                    lineLevel = 10;
                }else if(idStage <= 3){
                    qtdBloco = 10;
                    lineLevel = 5;
                }
            }

        #endregion

    }
}