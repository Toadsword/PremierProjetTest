using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour {

    [SerializeField] private Text textNbrDie;

	// Use this for initialization
	void Start () {
        StartCoroutine(ChangeScene());
        InfoPlayer infoPlayer = FindObjectOfType<InfoPlayer>();
        textNbrDie.text = "Nbr deaths : " + infoPlayer.GetPlayerDieCount();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene("Menu");
    }
}
