﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public bool canTripleShot = false;
    public bool isSpeedBoostActive = false;

    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private float _fireRate = 0.25f;
    private float _canFire = 0.0f;

    [SerializeField]
    private float _speed = 5.0f;

    private int _lives = 3;

    private void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        // transform.name
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

            if (canTripleShot == true) {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
                //left
                // Instantiate(_laserPrefab, transform.position + new Vector3(0.56f, 0.06f, 0), Quaternion.identity);
                //right
                // Instantiate(_laserPrefab, transform.position + new Vector3(-0.55f, 0.06f, 0), Quaternion.identity);
                // Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);
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
        // decrement 
        _lives--;

        // if life is less than 1, boom.
        if (_lives < 1) 
        {
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

}