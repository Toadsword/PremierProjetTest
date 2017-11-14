using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour {

    [SerializeField] private float scrollSpeed = 1.5f;
    [SerializeField] private float tileSizeX = 64.0f;
    [SerializeField] private GameObject player;

    private Vector2 startPosition;

    // Use this for initialization
    void Start ()
    {
        startPosition = transform.position;	
	}
	
	// Update is called once per frame
	void Update ()
    {
        float newPosition = player.transform.position.x * scrollSpeed;
        transform.position = startPosition + Vector2.right * newPosition;
    }
}
