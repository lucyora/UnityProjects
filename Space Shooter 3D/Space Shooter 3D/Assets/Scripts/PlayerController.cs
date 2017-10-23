using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax, zMin, zMax;

}
public class PlayerController : MonoBehaviour {
    public float speed;
    public float tilt;
    public Boundary boundary;
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;

    //Sound
    public AudioClip shootSound;
    public AudioClip shootSound2;

    private new AudioSource audio;
    private float volLowRange = 0.5f;
    private float volHighRange = 1.0f;

    private float nextFire;


    void Awake()
    {
        //Audio setting   
        audio = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if(Input.GetButton("Fire1") && Time.time > nextFire)
        {
            //Volume
            float vol = Random.Range(volLowRange, volHighRange);

            nextFire = Time.time + fireRate;
            //GameObject clone
            Instantiate(shot, transform.position, transform.rotation); //as GameObject;

            //Random shoot sounds and volume.
            int rand = Random.Range(0, 2);
            if ( rand == 0 )
            {
                audio.PlayOneShot(shootSound, vol);

            }
            else
            {
                audio.PlayOneShot(shootSound2, vol);
            }
        }
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal"); 
        float moveVertical = Input.GetAxis("Vertical");
        float moveUp = Input.GetAxis("Jump");

//        transform.rotation = Quaternion.Euler(moveHorizontal, moveUp, moveVertical);
        transform.rotation = Quaternion.Euler(moveHorizontal, moveUp, 0);

        Vector3 movement = new Vector3(moveHorizontal, moveUp, moveVertical );
        GetComponent<Rigidbody>().velocity = movement * speed;

        
        GetComponent<Rigidbody>().position = new Vector3
            (
                Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax),
                Mathf.Clamp(GetComponent<Rigidbody>().position.y, boundary.yMin, boundary.yMax),
                Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
            );

        //Tilt rotation
        //GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
        GetComponent<Rigidbody>().rotation = Quaternion.Euler(GetComponent<Rigidbody>().velocity.z * -tilt, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);

    }

    
}
