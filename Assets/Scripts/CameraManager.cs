using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera gameView; 
    [SerializeField] private CinemachineVirtualCamera waveView;
    [SerializeField] private CinemachineVirtualCamera cloudView;

    private CinemachineBrain cinemachineBrain;

    void Start()
    {
        cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();

        gameView.gameObject.SetActive(true);
        waveView.gameObject.SetActive(false);
        cloudView.gameObject.SetActive(false);
    }

    void Update()
    {
        // Switch cameras when pressing the "C" key
        if (Input.GetKeyDown(KeyCode.C))
        {
            SwitchToCloudView();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            SwitchToGameView();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            SwitchToWaveView();
        }
    }

    public void SwitchToGameView()
    {
        gameView.gameObject.SetActive(true);
        waveView.gameObject.SetActive(false);
        cloudView.gameObject.SetActive(false);
    }
    public void SwitchToWaveView()
    {
        gameView.gameObject.SetActive(false);
        waveView.gameObject.SetActive(true);
        cloudView.gameObject.SetActive(false);
    }
    public void SwitchToCloudView()
    {
        gameView.gameObject.SetActive(false);
        waveView.gameObject.SetActive(false);
        cloudView.gameObject.SetActive(true);
    }
}
