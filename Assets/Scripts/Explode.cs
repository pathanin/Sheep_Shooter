using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour {

    public LayerMask m_SheepMask;
    public ParticleSystem m_ExplosionParticles;
    public AudioSource m_ExplosionAudio;
    public float m_ExplosionForce = 1000f;
    public float m_MaxLifeTime = 6f;
    public float m_ExplosionRadius = 50f;

    public int scoreValue;
    private GameController gameController;


    private void Start()
    {
        Destroy(gameObject, m_MaxLifeTime);
        GameObject gameCOntrollerObject = GameObject.FindWithTag("GameController");
        if (gameCOntrollerObject != null)
        {
            gameController = gameCOntrollerObject.GetComponent<GameController>();
        }
        else
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary" || other.tag == "Shot" || other.tag == "Player")
            return;
        // Find all the sheep in an area around the bolt and damage them.
        //Debug.Log("Heyy");
        Collider[] colliders = Physics.OverlapSphere(other.transform.position, m_ExplosionRadius);
        for (int i = 0; i < colliders.Length; i++)
        {
            Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();

            if (!targetRigidbody)
            {
                continue;
            }
            targetRigidbody.AddExplosionForce(m_ExplosionForce, transform.position, m_ExplosionRadius);
            gameController.AddScore(scoreValue);
            Destroy(colliders[i].gameObject);
            Debug.Log("Destroy by Explosion");

        }

        m_ExplosionParticles.transform.parent = null;
        m_ExplosionParticles.Play();

        Destroy(m_ExplosionParticles.gameObject, m_ExplosionParticles.duration);
        Destroy(gameObject);
        //Destroy(other.gameObject);
        gameController.AddScore(scoreValue);
        //Debug.Log("Destroy sheep22222");

    }
    
}
