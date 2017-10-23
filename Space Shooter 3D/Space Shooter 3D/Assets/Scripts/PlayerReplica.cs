using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ReplicaBoundary
{
    public float RxMin, RxMax, RyMin, RyMax, RzMin, RzMax;

}
public class PlayerReplica : MonoBehaviour {
    public float speed;
    public float tilt;
    public ReplicaBoundary Rboundary;

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal"); 
        float moveVertical = Input.GetAxis("Vertical");
        float moveUp = Input.GetAxis("Jump");

        transform.rotation = Quaternion.Euler(moveHorizontal, moveUp, moveVertical);


        Vector3 movement = new Vector3(moveHorizontal, moveUp, moveVertical );
        GetComponent<Rigidbody>().velocity = movement * speed;


        GetComponent<Rigidbody>().position = new Vector3
            (
                Mathf.Clamp(GetComponent<Rigidbody>().position.x, Rboundary.RxMin, Rboundary.RxMax),
                Mathf.Clamp(GetComponent<Rigidbody>().position.y, Rboundary.RyMin, Rboundary.RyMax),
                Mathf.Clamp(GetComponent<Rigidbody>().position.z, Rboundary.RzMin, Rboundary.RzMax)
            );

        //GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
        GetComponent<Rigidbody>().rotation = Quaternion.Euler(GetComponent<Rigidbody>().velocity.z * -tilt, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
    }
}
