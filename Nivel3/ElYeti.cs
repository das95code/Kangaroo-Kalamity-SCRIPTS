using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElYeti : MonoBehaviour
{
    private float waitedTime;
    public float waitTimeToAttack = 3;

    public Animator animator;

    public GameObject bulletPrefab;

    public Transform launchSpawnPoint;


    private void Start()
    {
        waitedTime = waitTimeToAttack;
    }

    private void Update()
    {
        if (waitedTime <= 0)
        {
            waitedTime = waitTimeToAttack;
            animator.Play("yetiattack");
            Invoke("LaunchBullet", 0.8f);
            Invoke("LaunchBullet", 1f);
        }
        
        else
        {
            waitedTime -= Time.deltaTime;
        }

    }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.CompareTag("Player"))
            {
                Debug.Log("Player Damaged");
                collision.transform.GetComponent<PlayerRespawn>().PlayerDamaged();
            }
        }

        public void LaunchBullet()
        {
            GameObject newBullet;

            newBullet = Instantiate(bulletPrefab, launchSpawnPoint.position, launchSpawnPoint.rotation);
        }
}
