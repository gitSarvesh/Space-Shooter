using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroid : MonoBehaviour
{
    [SerializeField]
    private float rotateSpeed = 19f;

    [SerializeField]
    private GameObject explosion;

    private spawnManager spW;
    void Start()
    {
        spW = GameObject.Find("SpawnManager").GetComponent<spawnManager>();
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Laser")
        {
            Vector3 currPos = transform.position;
            Instantiate(explosion, currPos, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(this.gameObject, 0.2f);
            spW.StartSpawning();
        }
    }
}
