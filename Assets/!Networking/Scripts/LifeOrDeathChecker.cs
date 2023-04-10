using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeOrDeathChecker : MonoBehaviour
{
    private TeleportToNewLevel teleportToNewLevel;
    public GameObject player;
    public GameObject livesUI;


    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Awake()
    {
        if(player) teleportToNewLevel = player.GetComponent<TeleportToNewLevel>();
    }
    // Update is called once per frame
    void Update()
    {
        if(player){
            var lifeToShow = 0;
            if (teleportToNewLevel.playerLives > 0){
                lifeToShow = teleportToNewLevel.playerLives;
            }
            livesUI.GetComponent<TMPro.TextMeshProUGUI>().text = "x" + lifeToShow;
        }
    }
}
