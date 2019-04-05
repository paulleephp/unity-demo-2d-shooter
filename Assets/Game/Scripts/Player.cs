using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public bool canTripleShot = false;
    public bool isSpeedBoostActive = false;
    public bool isShieldActive = false;
    public int lives = 3;

    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private GameObject _shieldGameObject;
    [SerializeField]
    private GameObject[] _engines;

    [SerializeField]
    private float _fireRate = 0.25f;
    private float _canFire = 0.0f;

    [SerializeField]
    private float _speed = 5.0f;

    private UIManager _uiManager;
    private GameManager _gameManager;
    private SpawnManager _spawnManager;
    private AudioSource _audioSource;

    private int hitCount = 0;

    private void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uiManager != null)
        {
            _uiManager.UpdateLives(lives);
        }

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        //start spawning
        _spawnManager.StartSpawning();

        _audioSource = GetComponent<AudioSource>();

        // reset the hit count
        hitCount = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        Movement();

        // if space key pressed
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0)) 
        {
            Shoot();
        }
            
    }

    private void Shoot()
    {
        if (Time.time > _canFire) {
            _audioSource.Play();

            if (canTripleShot == true) {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            } else {
                // spawn laser at player position
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);
            }
            
            _canFire = Time.time + _fireRate;
        }
    }

    private void Movement() {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //speed boost
        if (isSpeedBoostActive) {
            transform.Translate(Vector3.right * horizontalInput * _speed * 1.5f * Time.deltaTime);
            transform.Translate(Vector3.up * verticalInput * _speed * 1.5f * Time.deltaTime);
        } else {
            transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
            transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);
        }

        if (transform.position.y > 0) {
            transform.position = new Vector3(transform.position.x, 0, 0);
        } else if (transform.position.y < -4.2f) {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }

        if (transform.position.x > 9.5f) {
            transform.position = new Vector3(-9.5f, transform.position.y, 0);
        } else if (transform.position.x < -9.5f) {
            transform.position = new Vector3(9.5f, transform.position.y, 0);
        }
    }

    public void Damage() 
    {
        // if the shield is active, take away the shield.
        if (isShieldActive) {
            isShieldActive = false;
            _shieldGameObject.SetActive(false);
            return;
        }

        hitCount++;

        if (hitCount == 1) {
            // turn left engine_failure on
            _engines[0].SetActive(true);
        } else if (hitCount == 2) {
            // turn right engine_failure on
            _engines[1].SetActive(true);
        }

        // decrement 
        lives--;
        _uiManager.UpdateLives(lives);

        // if life is less than 1, boom.
        if (lives < 1) 
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

            _gameManager.gameOver = true;
            _uiManager.ShowTitle();
            
            Destroy(this.gameObject);
        }
    }

    public void TripleShotPowerupOn() 
    {
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    public IEnumerator TripleShotPowerDownRoutine() 
    {
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false;
    }

    public void SpeedBoostPowerupOn() 
    {
        isSpeedBoostActive = true;
        StartCoroutine(SpeedBoostDownRoutine());
    }

    public IEnumerator SpeedBoostDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        isSpeedBoostActive = false;
    }   

    public void ShieldPowerupOn() 
    {
        isShieldActive = true;
        _shieldGameObject.SetActive(true);
    }   

}