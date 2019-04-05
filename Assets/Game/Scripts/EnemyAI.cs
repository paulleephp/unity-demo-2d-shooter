using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    //variable for your speed
    private float _speed = 3.0f;

    [SerializeField]
    private GameObject _enemyExplosionPrefab;
    private UIManager _uiManager;
    // private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _clip;

    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //move down
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        //when off the screen on the bottom
        // when X is -7, move back to 7

        //respawn back on top with a anew x position between bounds of the screen
        // Y should be in between - 8 and 8
        
        if (transform.position.y < -7.0f) {
            float randomX = Random.Range(-7.0f, 7.0f);
            transform.position = new Vector3(randomX, 7.0f, 0);
        }
    }

    // on collsion with the player,
    // destroy self, decrement player life and destroy if life < 1
    /**
        laser will damage the enemy

        the enemy will have to check for two different objects
    */
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with " + other.name);

        if (other.tag == "Player") 
        {
            //access the player
            Player player = other.GetComponent<Player>();

            // decrement player life
            player.Damage();

            // animate & destroy
            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);
            Destroy(this.gameObject);

        } else if (other.tag == "Laser") {
            //destroy parent if it exists
            if (other.transform.parent != null) 
            {
                Destroy(other.transform.parent.gameObject);
            }

            // destroy laser and kill self
            Destroy(other.gameObject);

            // animate & destroy
            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);

            if (_uiManager != null)
            {
                _uiManager.UpdateScore();
            }
            
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);

            Destroy(this.gameObject);
        }
    }
}
