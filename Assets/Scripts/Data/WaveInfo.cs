using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveInfo : MonoBehaviour
{
    //Make this take in subWaves
    //---public static Dictionary<int, List<(subWave)>> wave = new Dictionary<int, List<(Ball, int)>>();---

    //first int is the waveNum as key
    //List<(Ball, int)> will eventually be List<subWaveBall>
    //make this a class
    //---public static List<subWaveBall> subWave = new List<List<(Ball, int)>>();---
    //Then for i in subWave[currentWave] (will be a list){for i.numBalls in i: instantiate(i.theBall)}

    //Make this a class
    public static List<(Ball, int)> subWaveBall = new List<(Ball, int)>();


    // Start is called before the first frame update
    void Start()
    {
        //subWave.Add(new subWaveBall(smallBall, 2));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
