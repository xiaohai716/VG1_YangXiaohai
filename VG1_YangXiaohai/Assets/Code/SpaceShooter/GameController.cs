using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SpaceShooter
{

    public class GameController : MonoBehaviour
    {
        public static GameController instance;

        //Outlets
        public Transform[] spawnPoints;
        public GameObject[] asteroidPrefabs;
        public GameObject explosionPrefab;
        public TMP_Text textScore;
        public TMP_Text textMoney;
        public TMP_Text missileSpeedUpgradeText;
        public TMP_Text bonusUpgradeText;

        //Configuration
        public float maxAsteroidDelay = 2f;
        public float minAsteroidDelay = 0.2f;

        // State Tracking
        public float timeElapsed;
        public float asteroidDelay;
        public int score;
        public int money;
        public float missileSpeed = 2f;
        public float bonusMultiplier = 1f;

        void Awake()
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        void Start()
        {
            StartCoroutine("AsteroidSpawnTimer");

            score = 0;
            money = 0;
        }

        // Update is called once per frame
        void Update()
        {
            // Increment passage of time for each frame of the game
            timeElapsed += Time.deltaTime;

            // Computer Asteroid Delay
            float decreaseDelayOverTime = maxAsteroidDelay - ((maxAsteroidDelay - minAsteroidDelay) / 30f * timeElapsed);
            asteroidDelay = Mathf.Clamp(decreaseDelayOverTime, minAsteroidDelay, maxAsteroidDelay);

            UpdateDisplay();
        }

        void UpdateDisplay()
        {
            textScore.text = score.ToString();
            textMoney.text = money.ToString();
        }

        public void EarnPoints(int pointAmount)
        {
            score += Mathf.RoundToInt(pointAmount * bonusMultiplier);
            money += Mathf.RoundToInt(pointAmount * bonusMultiplier);
        }

        void SpawnAsteroid()
        {
            //Pick random spawn points and random asteroid prefabs
            int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
            Transform randomSpawnPoint = spawnPoints[randomSpawnIndex];
            int randomAsteroidIndex = Random.Range(0, asteroidPrefabs.Length);
            GameObject randomAsteroidPrefab = asteroidPrefabs[randomAsteroidIndex];

            //Spawn
            Instantiate(randomAsteroidPrefab, randomSpawnPoint.position, Quaternion.identity);
        }

        IEnumerator AsteroidSpawnTimer()
        {
            //wait
            yield return new WaitForSeconds(asteroidDelay);

            //spawn
            SpawnAsteroid();

            //repeat
            StartCoroutine("AsteroidSpawnTimer");
        }

        public void UpgradeMissileSpeed()
        {
            int cost = Mathf.RoundToInt(25 * missileSpeed);
            if (cost < money)
            {
                money -= cost;

                missileSpeed += 1f;

                missileSpeedUpgradeText.text = "Missile Speed $" + Mathf.RoundToInt(25 * missileSpeed);
            }
        }

        public void UpgradeBonus()
        {
            int cost = Mathf.RoundToInt(100 * bonusMultiplier);
            if (cost < money)
            {
                money -= cost;

                bonusMultiplier += 1f;

                bonusUpgradeText.text = "Multiplier $" + Mathf.RoundToInt(100 * bonusMultiplier);
            }
        }
    }
}
