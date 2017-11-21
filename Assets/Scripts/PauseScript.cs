using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PauseScript : MonoBehaviour {

    [SerializeField]
    private GameObject pausePanel;
    [SerializeField]
    private GameObject uiGamePanel;

    private bool isPaused = false;

    public event Action OnPauseEvent;

    private static PauseScript instance;
    public static PauseScript Instance
    {
        get
        {
            return instance;
        }
    }

    public void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start ()
    {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButton("Pause") && !isPaused)
        {
            isPaused = true;
            pausePanel.SetActive(true);
            uiGamePanel.SetActive(false);
            Time.timeScale = 0;
            OnPauseEvent();
        }
	}

    public void OnResumeGame()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        uiGamePanel.SetActive(true);
        Time.timeScale = 1;
    }
}
