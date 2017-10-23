using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;

    //Sound
    public AudioClip crashSoft;
    public AudioClip crashHard;
    public AudioClip Warning;

    private new AudioSource audio;
    private float lowPitchRange = 0.25f;
    private float highPitchRange = 0.75f;
    private float velToVol = 0.0005f;
    private float velocityClipSplit = 30f;

    private Transform PlayerReplica;
    private GameController gameController;

    private void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if( gameController ==  null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }

        audio = GetComponent<AudioSource>();
        PlayerReplica = GameObject.FindWithTag("PlayerReplica").transform;

    }
    private void Awake()
    {
       audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if( other.tag == "Boundary")
        {
            return;
        }

        if( other.tag == "Warning")
        {
            audio.PlayOneShot(Warning, 1.0f);
            return;
        }

        if( other.tag == "Bolt")
        {
            //Particle Effect
            Instantiate(explosion, transform.position, transform.rotation);

            //Sound
            Vector3 offset = PlayerReplica.position - this.transform.position;
            float magnitude = offset.sqrMagnitude;

            audio.pitch = Random.Range(lowPitchRange, highPitchRange);
            float hitVol = magnitude * velToVol;

            //Close
            if ( magnitude < velocityClipSplit)
            {
                audio.PlayOneShot(crashHard, hitVol);
            }
            else //Far
            {
                audio.PlayOneShot(crashSoft, hitVol);
            }
        }

        if( other.tag =="Player")
        { 
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController.GameOver();
        }

        gameController.AddScore(scoreValue);
        Destroy(other.gameObject);
        Destroy(gameObject);
    }

}
