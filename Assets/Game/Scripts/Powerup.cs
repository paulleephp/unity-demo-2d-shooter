using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int powerupId; // 0 = triple shot 1 = speed, 2 = shields

    [SerializeField]
    private AudioClip _clip;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -7) {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with " + other.name);

        if (other.tag == "Player") {
            //access the player
            Player player = other.GetComponent<Player>();

            if ( player != null) 
            {
                if (powerupId == 0) {
                    //enable triple shot
                    player.TripleShotPowerupOn();
                } else if (powerupId == 1) {
                    //enable speed boost here
                    player.SpeedBoostPowerupOn();
                } else if (powerupId == 2) {
                    //enable shields
                    player.ShieldPowerupOn();
                }
            }

            StartCoroutine(player.TripleShotPowerDownRoutine());

            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);

            //destory ourself
            Destroy(this.gameObject);
        }
    }
}
