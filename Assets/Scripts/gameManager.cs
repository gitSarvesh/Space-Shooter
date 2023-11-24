using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    [SerializeField]
    private bool isGameOver;
    public bool isCoopMode;
    private spawnManager _spawnManager;
    private player player_1;
    private player player_2;
    [SerializeField]
    private GameObject pauseMenuPanel;
    
    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<spawnManager>();
        player_1 = GameObject.Find("Player_1").GetComponent<player>();
        if(isCoopMode == true){
            player_2 = GameObject.Find("Player_2").GetComponent<player>();
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void GameOver()
    {
        isGameOver = true;
    }
    void Update()
    {
        if(player_1 == null || player_2 == null){
            GameOver();
        }
        if(isGameOver == true){
            if(Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape)){
            SceneManager.LoadScene(0);
        }

        if(Input.GetKey(KeyCode.P)){
            pauseMenuPanel.SetActive(true);
            Time.timeScale = 0;
        }
        
    }

    public void ResumeGame(){
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
