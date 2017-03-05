using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	private GameController gameController;

	void Start(){
		GameObject gameCOntrollerObject = GameObject.FindWithTag ("GameController");
		if (gameCOntrollerObject != null) {
			gameController = gameCOntrollerObject.GetComponent<GameController> ();
		} else {
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Boundary" || other.tag == "Shot")
			return;
		Instantiate (explosion, transform.position, transform.rotation);// as GameObject;
		if (other.tag == "Player") {
			Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
			gameController.DecreaseLife ();
            gameController.ReduceScore(scoreValue);
            if(gameController.isGameOver())
            {
                Destroy(other.gameObject);
            }
        }else
        {
            gameController.AddScore(scoreValue);
        }
        if (other.tag != "Player")
        {
        Destroy(other.gameObject);
        }
        Destroy(gameObject);
        
    }
}
