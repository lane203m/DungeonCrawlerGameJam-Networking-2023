using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Input_Handler))]
[RequireComponent(typeof(GameMenuController))]

public class TeleportToNewLevel : MonoBehaviour
{

    public GameObject[] levels;
    public List<GameObject> enemySpawners;
    public int playerCurrentLevel = 0;

    public int playerLives = 5;
    Input_Handler inputHandler;
    GameMenuController gameMenuController;
    FlashDeath flashStateController;

    public AudioSource[] AudioSources;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake(){
        inputHandler = GetComponent<Input_Handler>();
        gameMenuController = GetComponent<GameMenuController>();
        AudioSources = GetComponents<AudioSource>();
        flashStateController = GetComponent<FlashDeath>();
        foreach (GameObject spanwer in enemySpawners)
        {
            spanwer.GetComponent<SpawnEnemy>().triggerSpawn = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerLives < 0){
            flashStateController.FadeToBlack();
            StartCoroutine(failScreen());
        }

        if(playerCurrentLevel == levels.Length){
            flashStateController.FadeToBlack();
            StartCoroutine(winScreen());
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Enemy" && collision.gameObject.GetComponent<EnemyDestroyer>().canKill){
            playerLives -= 1;

            AudioSources[1].Play();
            flashStateController.StartFlashDeath();

            if(playerLives > -1 && playerCurrentLevel != levels.Length-1){
                var currentEnemies = GameObject.FindGameObjectsWithTag("Enemy");                
                foreach (GameObject enemy in currentEnemies)
                {
                    Destroy(enemy);
                }

                var vec = levels[playerCurrentLevel].transform.eulerAngles;
                vec.x = Mathf.Round(vec.x / 90) * 90;
                vec.y = Mathf.Round(vec.y / 90) * 90;
                vec.z = Mathf.Round(vec.z / 90) * 90;

                var newPos = levels[playerCurrentLevel].transform.position;

                inputHandler.movementHandler.targetGrid = newPos;
                inputHandler.movementHandler.targetRotation = vec;
                inputHandler.movementQueue.Clear();

                gameObject.transform.position = newPos;
                
                foreach (GameObject spanwer in enemySpawners)
                {
                    spanwer.GetComponent<SpawnEnemy>().triggerSpawn = true;
                }
            }
        }
        
        if(collision.gameObject.tag == "Escape"){
            playerCurrentLevel++;

            AudioSources[2].Play();
            flashStateController.StartFlashLife();

            if(playerCurrentLevel != levels.Length){
                var vec = levels[playerCurrentLevel].transform.eulerAngles;
                vec.x = Mathf.Round(vec.x / 90) * 90;
                vec.y = Mathf.Round(vec.y / 90) * 90;
                vec.z = Mathf.Round(vec.z / 90) * 90;

                var newPos = levels[playerCurrentLevel].transform.position;

                inputHandler.movementHandler.targetGrid = newPos;
                inputHandler.movementHandler.targetRotation = vec;
                inputHandler.movementQueue.Clear();

                gameObject.transform.position = newPos;
            }
        }
    }

    private IEnumerator failScreen(){
        yield return new WaitForSeconds(1f);
        gameMenuController.FailScreen();        
    }
    private IEnumerator winScreen(){
        yield return new WaitForSeconds(1f);
        gameMenuController.WinScreen();        
    }
}
