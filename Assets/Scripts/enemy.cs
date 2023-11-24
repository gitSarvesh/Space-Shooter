using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    [SerializeField]
    private float enemySpeed = 3f;

    private player player_1;
    private player player_2;

    private Animator animator;

    private AudioSource audioSource;

    [SerializeField]
    private GameObject laserPrefab;

    private float fireRate = 2.0f;
    private float canFire = -1f;

    private bool enemyHasHit = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player_1 = GameObject.Find("Player_1")?.GetComponent<player>();
        player_2 = GameObject.Find("Player_2")?.GetComponent<player>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        CalculateMovement();
        if (Time.time > canFire && enemyHasHit == false)
        {
            canFire = Time.time + fireRate;
            GameObject enemyLaser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
            laser[] lasers = enemyLaser.GetComponentsInChildren<laser>();
            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaser();
            }
        }
    }

    void CalculateMovement()
    {
        transform.Translate(Vector3.down * enemySpeed * Time.deltaTime);
        if (transform.position.y <= -3.8f)
        {
            transform.position = new Vector3(Random.Range(-9f, 9f), 8, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player_1" && !enemyHasHit)
        {
            enemyHasHit = true;
            if (player_1 != null)
            {
                player_1.Damage();
                animator.SetTrigger("OnEnemyDeath");
                enemySpeed = 0f;
            }
            audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.22f);
        }
        else if (other.name == "Player_2" && !enemyHasHit)
        {
            enemyHasHit = true;
            if (player_2 != null)
            {
                player_2.Damage();
                animator.SetTrigger("OnEnemyDeath");
                enemySpeed = 0f;
            }
            audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.22f);
        }
        else if (other.tag == "Laser" && !enemyHasHit)
        {
            enemyHasHit = true;
            if (player_1 != null)
            {
                player_1.AddScore(10);
            }
            animator.SetTrigger("OnEnemyDeath");
            enemySpeed = 0f;
            Destroy(other.gameObject);
            audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.22f);
        }
    }
}
