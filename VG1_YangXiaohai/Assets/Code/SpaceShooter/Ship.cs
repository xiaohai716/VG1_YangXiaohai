using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Ship : MonoBehaviour
    {
        //Outlet
        public GameObject projectilePrefab;

        //State Tracking
        public float firingDelay = 1f;

        void Start()
        {
            StartCoroutine("FiringTimer");    
        }


        // Update is called once per frame
        void Update()
        {
            float yPosition = Mathf.Sin(GameController.instance.timeElapsed) * 3f;
            transform.position = new Vector2(0, yPosition);
        }

        void FireProjectile()
        {
            Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        }

        IEnumerator FiringTimer()
        {
            yield return new WaitForSeconds(firingDelay);

            FireProjectile();

            StartCoroutine("FiringTimer");
        }
    }
}