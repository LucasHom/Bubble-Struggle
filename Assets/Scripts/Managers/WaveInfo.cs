using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BallSpawnInfo
{
    public GameObject ballPrefab;  // Use GameObject so it's editable in Inspector
    public int count;

    public BallSpawnInfo(GameObject prefab, int count)
    {
        this.ballPrefab = prefab;
        this.count = count;
    }
}

[System.Serializable]
public class SubWave
{
    public List<BallSpawnInfo> enemies = new List<BallSpawnInfo>();
}

[System.Serializable]
public class Wave
{
    public string description;

    public Wave(string description)
    {
        this.description = description;
    }

    public List<SubWave> subWaves = new List<SubWave>();
}

public class WaveInfo : MonoBehaviour
{
    [SerializeField] private GameObject largeBallPrefab;
    [SerializeField] private GameObject mediumBallPrefab;
    [SerializeField] private GameObject smallBallPrefab;
    [SerializeField] private GameObject largeBallGuardedPrefab;
    [SerializeField] private GameObject mediumBallGuardedPrefab;
    [SerializeField] private GameObject smallBallGuardedPrefab;
    [SerializeField] private GameObject supportBallPrefab;

    //Spawning
    [SerializeField] float timeBetweenSpawn = 0.5f;

    public List<Wave> allWaves = new List<Wave>();

    private void Awake()
    {

    }

    //create the waves
    void Start()
    {
        // Wave 1
        Wave wave1 = new Wave("Is that cloud... green?!");
        SubWave sub1_1 = new SubWave();
        sub1_1.enemies.Add(new BallSpawnInfo(mediumBallPrefab, 1));

        SubWave sub2_1 = new SubWave();
        sub2_1.enemies.Add(new BallSpawnInfo(mediumBallPrefab, 1));
        sub2_1.enemies.Add(new BallSpawnInfo(smallBallPrefab, 1));

        SubWave sub3_1 = new SubWave();
        sub3_1.enemies.Add(new BallSpawnInfo(mediumBallPrefab, 2));

        wave1.subWaves.Add(sub1_1);
        wave1.subWaves.Add(sub2_1);
        wave1.subWaves.Add(sub3_1);

        allWaves.Add(wave1);

        // Wave 2
        Wave wave2 = new Wave("Protecting Midge");

        SubWave sub1_2 = new SubWave();
        sub1_2.enemies.Add(new BallSpawnInfo(mediumBallPrefab, 2));

        SubWave sub2_2 = new SubWave();
        sub2_2.enemies.Add(new BallSpawnInfo(mediumBallPrefab, 1));
        sub2_2.enemies.Add(new BallSpawnInfo(smallBallPrefab, 1));

        SubWave sub3_2 = new SubWave();
        sub3_2.enemies.Add(new BallSpawnInfo(mediumBallPrefab, 2));

        SubWave sub4_2 = new SubWave();
        sub4_2.enemies.Add(new BallSpawnInfo(mediumBallPrefab, 1));
        sub4_2.enemies.Add(new BallSpawnInfo(smallBallPrefab, 3));


        wave2.subWaves.Add(sub1_2);
        wave2.subWaves.Add(sub2_2);
        wave2.subWaves.Add(sub3_2);
        wave2.subWaves.Add(sub4_2);

        allWaves.Add(wave2);

        // Wave 3
        Wave wave3 = new Wave("Sky looks heavy...");
        SubWave sub1_3 = new SubWave();
        sub1_3.enemies.Add(new BallSpawnInfo(largeBallPrefab, 1));

        SubWave sub2_3 = new SubWave();
        sub2_3.enemies.Add(new BallSpawnInfo(smallBallPrefab, 1));
        sub2_3.enemies.Add(new BallSpawnInfo(largeBallPrefab, 1));

        SubWave sub3_3 = new SubWave();
        sub3_3.enemies.Add(new BallSpawnInfo(smallBallPrefab, 1));
        sub3_3.enemies.Add(new BallSpawnInfo(mediumBallPrefab, 1));

        SubWave sub4_3 = new SubWave();
        sub4_3.enemies.Add(new BallSpawnInfo(mediumBallPrefab, 2));

        SubWave sub5_3 = new SubWave();
        sub5_3.enemies.Add(new BallSpawnInfo(smallBallPrefab, 1));
        sub5_3.enemies.Add(new BallSpawnInfo(largeBallPrefab, 1));


        wave3.subWaves.Add(sub1_3);
        wave3.subWaves.Add(sub2_3);
        wave3.subWaves.Add(sub3_3);
        wave3.subWaves.Add(sub4_3);
        wave3.subWaves.Add(sub5_3);

        allWaves.Add(wave3);

        // Wave 4
        Wave wave4 = new Wave("Persevering thick air");
        SubWave sub1_4 = new SubWave();
        sub1_4.enemies.Add(new BallSpawnInfo(largeBallPrefab, 1));
        sub1_4.enemies.Add(new BallSpawnInfo(smallBallPrefab, 1));

        SubWave sub2_4 = new SubWave();
        sub2_4.enemies.Add(new BallSpawnInfo(largeBallPrefab, 1));
        sub2_4.enemies.Add(new BallSpawnInfo(mediumBallPrefab, 1));

        SubWave sub3_4 = new SubWave();
        sub3_4.enemies.Add(new BallSpawnInfo(smallBallPrefab, 2));
        sub3_4.enemies.Add(new BallSpawnInfo(mediumBallPrefab, 2));

        SubWave sub4_4 = new SubWave();
        sub4_4.enemies.Add(new BallSpawnInfo(mediumBallPrefab, 3));
        sub4_4.enemies.Add(new BallSpawnInfo(smallBallPrefab, 1));

        SubWave sub5_4 = new SubWave();
        sub5_4.enemies.Add(new BallSpawnInfo(smallBallPrefab, 1));
        sub5_4.enemies.Add(new BallSpawnInfo(mediumBallPrefab, 1));
        sub5_4.enemies.Add(new BallSpawnInfo(largeBallPrefab, 1));

        SubWave sub6_4 = new SubWave();
        sub6_4.enemies.Add(new BallSpawnInfo(mediumBallPrefab, 4));

        SubWave sub7_4 = new SubWave();
        sub7_4.enemies.Add(new BallSpawnInfo(smallBallPrefab, 8));

        wave4.subWaves.Add(sub1_4);
        wave4.subWaves.Add(sub2_4);
        wave4.subWaves.Add(sub3_4);
        wave4.subWaves.Add(sub4_4);
        wave4.subWaves.Add(sub5_4);
        wave4.subWaves.Add(sub6_4);
        wave4.subWaves.Add(sub7_4);

        allWaves.Add(wave4);

        // Wave 5
        Wave wave5 = new Wave("TIPPING POINT");
        SubWave sub1_5 = new SubWave();
        sub1_5.enemies.Add(new BallSpawnInfo(smallBallPrefab, 3));

        SubWave sub2_5 = new SubWave();
        sub2_5.enemies.Add(new BallSpawnInfo(smallBallPrefab, 2));

        SubWave sub3_5 = new SubWave();
        sub3_5.enemies.Add(new BallSpawnInfo(smallBallPrefab, 1));

        SubWave sub4_5 = new SubWave();
        sub4_5.enemies.Add(new BallSpawnInfo(smallBallPrefab, 1));

        SubWave sub5_5 = new SubWave();
        sub5_5.enemies.Add(new BallSpawnInfo(smallBallPrefab, 1));

        SubWave sub6_5 = new SubWave();
        sub6_5.enemies.Add(new BallSpawnInfo(largeBallPrefab, 1));
        sub6_5.enemies.Add(new BallSpawnInfo(mediumBallPrefab, 2));
        sub6_5.enemies.Add(new BallSpawnInfo(smallBallPrefab, 2));
        sub6_5.enemies.Add(new BallSpawnInfo(mediumBallPrefab, 1));
        sub6_5.enemies.Add(new BallSpawnInfo(smallBallPrefab, 4));
        sub6_5.enemies.Add(new BallSpawnInfo(smallBallPrefab, 2));
        sub6_5.enemies.Add(new BallSpawnInfo(smallBallPrefab, 1));
        sub6_5.enemies.Add(new BallSpawnInfo(largeBallPrefab, 2));

        wave5.subWaves.Add(sub1_5);
        wave5.subWaves.Add(sub2_5);
        wave5.subWaves.Add(sub3_5);
        wave5.subWaves.Add(sub4_5);
        wave5.subWaves.Add(sub5_5);
        wave5.subWaves.Add(sub6_5);
        allWaves.Add(wave5);

        // Wave 6
        Wave wave6 = new Wave("Atmospheric armor");
        SubWave sub1_6 = new SubWave();
        sub1_6.enemies.Add(new BallSpawnInfo(largeBallGuardedPrefab, 1));

        SubWave sub2_6 = new SubWave();
        sub2_6.enemies.Add(new BallSpawnInfo(largeBallPrefab, 2));
        sub2_6.enemies.Add(new BallSpawnInfo(mediumBallPrefab, 2));

        SubWave sub3_6 = new SubWave();
        sub3_6.enemies.Add(new BallSpawnInfo(largeBallPrefab, 4));

        wave6.subWaves.Add(sub1_6);
        wave6.subWaves.Add(sub2_6);
        wave6.subWaves.Add(sub3_6);
        allWaves.Add(wave6);

        // Wave 7
        Wave wave7 = new Wave("");
        SubWave sub1_7 = new SubWave();
        sub1_7.enemies.Add(new BallSpawnInfo(mediumBallPrefab, 5));

        SubWave sub2_7 = new SubWave();
        sub2_7.enemies.Add(new BallSpawnInfo(largeBallPrefab, 3));

        SubWave sub3_7 = new SubWave();
        sub3_7.enemies.Add(new BallSpawnInfo(mediumBallPrefab, 6));

        wave7.subWaves.Add(sub1_7);
        wave7.subWaves.Add(sub2_7);
        wave7.subWaves.Add(sub3_7);
        allWaves.Add(wave7);

        // Wave 8
        Wave wave8 = new Wave("");
        SubWave sub1_8 = new SubWave();
        sub1_8.enemies.Add(new BallSpawnInfo(largeBallPrefab, 2));
        sub1_8.enemies.Add(new BallSpawnInfo(mediumBallPrefab, 3));

        SubWave sub2_8 = new SubWave();
        sub2_8.enemies.Add(new BallSpawnInfo(largeBallPrefab, 5));

        SubWave sub3_8 = new SubWave();
        sub3_8.enemies.Add(new BallSpawnInfo(mediumBallPrefab, 7));

        wave8.subWaves.Add(sub1_8);
        wave8.subWaves.Add(sub2_8);
        wave8.subWaves.Add(sub3_8);
        allWaves.Add(wave8);

        // Wave 9
        Wave wave9 = new Wave("");
        SubWave sub1_9 = new SubWave();
        sub1_9.enemies.Add(new BallSpawnInfo(mediumBallPrefab, 6));

        SubWave sub2_9 = new SubWave();
        sub2_9.enemies.Add(new BallSpawnInfo(largeBallPrefab, 4));

        SubWave sub3_9 = new SubWave();
        sub3_9.enemies.Add(new BallSpawnInfo(mediumBallPrefab, 8));

        wave9.subWaves.Add(sub1_9);
        wave9.subWaves.Add(sub2_9);
        wave9.subWaves.Add(sub3_9);
        allWaves.Add(wave9);

        // Wave 10
        Wave wave10 = new Wave("");
        SubWave sub1_10 = new SubWave();
        //1 ball
        sub1_10.enemies.Add(new BallSpawnInfo(smallBallPrefab, 1));

        //crazy wave
        SubWave sub2_10 = new SubWave();
        sub2_10.enemies.Add(new BallSpawnInfo(largeBallPrefab, 6));

        SubWave sub3_10 = new SubWave();
        sub3_10.enemies.Add(new BallSpawnInfo(mediumBallPrefab, 10));

        wave10.subWaves.Add(sub1_10);
        wave10.subWaves.Add(sub2_10);
        wave10.subWaves.Add(sub3_10);
        allWaves.Add(wave10);

    }

    public void SpawnSubWave(int waveIndex, int subWaveIndex, Vector3 spawnPoint)
    {
        if (waveIndex >= allWaves.Count) return;
        if (subWaveIndex >= allWaves[waveIndex].subWaves.Count) return;

        SubWave subWave = allWaves[waveIndex].subWaves[subWaveIndex];

        StartCoroutine(SpawnSubWaveCoroutine(subWave, spawnPoint));
    }

    private IEnumerator SpawnSubWaveCoroutine(SubWave subWave, Vector3 spawnPoint)
    {
        foreach (BallSpawnInfo info in subWave.enemies)
        {
            for (int i = 0; i < info.count; i++)
            {
                Instantiate(info.ballPrefab, spawnPoint, Quaternion.identity);

                if (i % 3 == 0) //reduce fps impact
                {
                    yield return null; // wait 1 frame
                }

                yield return new WaitForSeconds(timeBetweenSpawn);
            }
            
        }
    }

}

