using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameOver = true;
    [SerializeField]
    private GameObject _playerPrefab;

    private UIManager _uiManager;

    private void Start() 
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    private void Update() {
        if (gameOver && Input.GetKeyDown(KeyCode.Space)) //start the game
        {
            Instantiate(_playerPrefab, Vector3.zero, Quaternion.identity);
            gameOver = false;

            _uiManager.HideTitle();
        }
    }
}
