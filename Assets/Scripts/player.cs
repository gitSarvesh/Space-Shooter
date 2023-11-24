using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField]
    private float speed = 4.5f;

    [SerializeField]
    private GameObject laserPrefab;

    [SerializeField]
    private float fireRate = 0.15f;

    [SerializeField]
    private float canFire = -1f;

    [SerializeField]
    private int lives = 3;

    private spawnManager _spawnManager;

    [SerializeField]
    private bool isTripleShotActive = false;

    [SerializeField]
    private GameObject tripleShot;

    private float speedMultiplyer = 2.0f;

    private bool isSpeedActive = false;

    private bool isShieldActive = false;

    [SerializeField]
    private GameObject shieldVisualizer;
    private Coroutine shieldCooldownCoroutine;

    [SerializeField]
    private int score;
    private int bestScore;

    private UIManager uiManager;

    [SerializeField]
    private GameObject leftEngine, rightEngine; 

    [SerializeField]
    private AudioClip laserSoundClip;

    private AudioSource audioSource;
    private gameManager _gameManager;
    public bool isPlayer1;
    public bool isPlayer2;


    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<gameManager>();
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<spawnManager>();
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        audioSource = GetComponent<AudioSource>();
        if(_spawnManager == null)
        {
            Debug.LogError("You have null in next elements");
        }
        if(uiManager == null)
        {
            Debug.LogError("UIManager is not present");
        }
        if(audioSource == null)
        {
            Debug.LogError("Audio Source on the player is not present");
        }
        else
        {
            audioSource.clip = laserSoundClip;
        }
        if(_gameManager.isCoopMode == false){
            transform.position = new Vector3(0, 0, 0);
        }
        bestScore = PlayerPrefs.GetInt("BestScore", 0);

    }

    void Update()
    {
        if(isPlayer1 == true){
            PlayerOneMovement();
            if(Input.GetKey(KeyCode.Space) && Time.time > canFire && isPlayer1 == true){
                FireMethodPlayerOne();
            }
        }
        if(isPlayer2 == true){
            PlayerTwoMovement();
            if(Input.GetKey(KeyCode.KeypadEnter) && Time.time > canFire && isPlayer2 == true){
                FireMethodPlayerTwo();
            }
        }
    }

    void PlayerOneMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        if(isSpeedActive == true){
            transform.Translate(direction * speed * speedMultiplyer * Time.deltaTime);
        }
        else{
            transform.Translate(direction * speed * Time.deltaTime);
        }
        

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if(transform.position.x >= 11)
        {
            transform.position = new Vector3(-11, transform.position.y, 0);
        }
        else if(transform.position.x <= -11)
        {
            transform.position = new Vector3(11, transform.position.y, 0);
        }
    }
    void PlayerTwoMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if(isSpeedActive == true){
            if(Input.GetKey(KeyCode.Keypad8)){
            transform.Translate(Vector3.up * speed * speedMultiplyer * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.Keypad6)){
            transform.Translate(Vector3.right * speed * speedMultiplyer * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.Keypad5)){
            transform.Translate(Vector3.down * speed * speedMultiplyer * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.Keypad4)){
            transform.Translate(Vector3.left * speed * speedMultiplyer * Time.deltaTime);
        }
        }
        else{
            if(Input.GetKey(KeyCode.Keypad8)){
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.Keypad6)){
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.Keypad5)){
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.Keypad4)){
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        }

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if(transform.position.x >= 11)
        {
            transform.position = new Vector3(-11, transform.position.y, 0);
        }
        else if(transform.position.x <= -11)
        {
            transform.position = new Vector3(11, transform.position.y, 0);
        }
    }

    public void Damage()
    {
        if(isShieldActive == true)
        {
            isShieldActive = false;
            shieldVisualizer.SetActive(false);
            return;
        }
        lives--;
        if(lives == 2)
        {
            leftEngine.SetActive(true);
        }
        else if(lives == 1)
        {
            rightEngine.SetActive(true);  
        }
        uiManager.UpdateLives(lives);
        if(lives < 1)
        {
            _gameManager.GameOver();
            _spawnManager.OnPlayerDeath();
            CheckForBestScore();
            this.gameObject.SetActive(false);
        }

    }

    void FireMethodPlayerOne()
    {
        canFire = Time.time + fireRate;
        if(isTripleShotActive == true)
        {
            Instantiate(tripleShot, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(laserPrefab, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
        }
        audioSource.Play();
    }
    void FireMethodPlayerTwo()
    {
        canFire = Time.time + fireRate;
        if(isTripleShotActive == true)
        {
            Instantiate(tripleShot, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }
        audioSource.Play();
    }


    public void TripleShotActive()
    {
        isTripleShotActive = true;
        StartCoroutine(TripleShotCoolDownRoutine());
    }

    IEnumerator TripleShotCoolDownRoutine()
    {
        while(isTripleShotActive == true)
        {
            yield return new WaitForSeconds(5.0f);
            isTripleShotActive = false;
        }
    }

    public void SpeedBoostActive()
    {
        isSpeedActive = true;
        StartCoroutine(SpeedBoostCoolDownRoutine());
    }
    
    IEnumerator SpeedBoostCoolDownRoutine()
    {
        while(isSpeedActive == true)
        {
            yield return new WaitForSeconds(5.0f);
            isSpeedActive = false;
        }
    }

    public void ShieldActive()
{
    isShieldActive = true;
    shieldVisualizer.SetActive(true);
    if (shieldCooldownCoroutine != null)
    {
        StopCoroutine(shieldCooldownCoroutine); // Stop the previous coroutine if running
    }
    shieldCooldownCoroutine = StartCoroutine(ShieldCoolDownRoutine());
}

IEnumerator ShieldCoolDownRoutine()
{
    yield return new WaitForSeconds(5.0f);
    isShieldActive = false;
    shieldVisualizer.SetActive(false);
}

    public void AddScore(int points)
    {
        score += points;
        uiManager.UpdateScore(score);
    }

    public void CheckForBestScore()
    {
        if (score > bestScore)
        {
            bestScore = score;
            uiManager.UpdateBestScore(bestScore);
            PlayerPrefs.SetInt("BestScore", bestScore);
        }
    }
}
