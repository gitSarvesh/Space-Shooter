using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour
{
    [SerializeField]
    private float laserSpeed = 8f;

    public bool isEnemyLaser = false;
    private player player_1;
    private player player_2;

    void Start()
    {
        player_1 = GameObject.Find("Player_1")?.GetComponent<player>();
        player_2 = GameObject.Find("Player_2")?.GetComponent<player>();
    }

    void Update()
    {
        if (isEnemyLaser == false)
        {
            MoveUp();
        }
        else
        {
            MoveDown();
        }
    }

    void MoveUp()
    {
        transform.Translate(Vector3.up * (laserSpeed + 2) * Time.deltaTime);
        if (transform.position.y >= 8)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    void MoveDown()
    {
        transform.Translate(Vector3.down * (laserSpeed - 2) * Time.deltaTime);
        if (transform.position.y <= -8)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    public void AssignEnemyLaser()
    {
        isEnemyLaser = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player_1" && isEnemyLaser == true)
        {
            if (player_1 != null)
            {
                player_1.Damage();
            }
        }
        if (other.name == "Player_2" && isEnemyLaser == true)
        {
            if (player_2 != null)
            {
                player_2.Damage();
            }
        }
    }
}
