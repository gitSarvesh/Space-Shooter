using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUp : MonoBehaviour
{
    [SerializeField]
    private float speed = 3f;

    [SerializeField]
    private int powerUpID;

    [SerializeField]
    private AudioClip audioClip;


    void Start()
    {

    }


    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if(transform.position.y <= -5)
        {
            Destroy(this.gameObject);
        }   
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            player _player = other.GetComponent<player>();
            AudioSource.PlayClipAtPoint(audioClip, transform.position);
            if(_player != null){
            switch(powerUpID)
            {
                case 0:
                    _player.TripleShotActive();
                    break;
                case 1:
                    _player.SpeedBoostActive();
                    break;
                case 2:
                    _player.ShieldActive();
                    break;
            }
            Destroy(this.gameObject);
            }
        }
    }

    
}
