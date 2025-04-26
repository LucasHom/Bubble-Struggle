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
    public List<SubWave> subWaves = new List<SubWave>();
}

public class WaveInfo : MonoBehaviour
{
    [SerializeField] private GameObject largeBallPrefab;
    [SerializeField] private GameObject mediumBallPrefab;
    [SerializeField] private GameObject smallBallPrefab;
    [SerializeField] private GameObject largeBallGuardedPrefab;
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
        //Create wave 1
        Wave wave1 = new Wave();
        SubWave sub1_1 = new SubWave();
        sub1_1.enemies.Add(new BallSpawnInfo(mediumBallPrefab, 2));

        SubWave sub2_1 = new SubWave();
        sub2_1.enemies.Add(new BallSpawnInfo(mediumBallPrefab, 3));

        SubWave sub3_1 = new SubWave();
        sub3_1.enemies.Add(new BallSpawnInfo(mediumBallPrefab, 2));
        sub3_1.enemies.Add(new BallSpawnInfo(largeBallPrefab, 1));

        wave1.subWaves.Add(sub1_1);
        wave1.subWaves.Add(sub2_1);
        wave1.subWaves.Add(sub3_1);

        allWaves.Add(wave1);

        // Create wave 2
        Wave wave2 = new Wave();

        SubWave sub1_2 = new SubWave();
        sub1_2.enemies.Add(new BallSpawnInfo(mediumBallPrefab, 3));

        SubWave sub2_2 = new SubWave();
        sub2_2.enemies.Add(new BallSpawnInfo(mediumBallPrefab, 2));
        sub2_2.enemies.Add(new BallSpawnInfo(largeBallPrefab, 1));

        SubWave sub3_2 = new SubWave();
        sub3_2.enemies.Add(new BallSpawnInfo(largeBallPrefab, 2));

        wave2.subWaves.Add(sub1_2);
        wave2.subWaves.Add(sub2_2);
        wave2.subWaves.Add(sub3_2);

        allWaves.Add(wave2);

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

