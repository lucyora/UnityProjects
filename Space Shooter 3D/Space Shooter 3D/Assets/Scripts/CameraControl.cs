using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public Transform Player;
    public Transform PlayerReplica;
    private Vector3 offset;

	// Use this for initialization
	void Start ()
    {
        //TODO: Get pos from player replica

        Player = GameObject.FindWithTag("Player").transform;
        PlayerReplica = GameObject.FindWithTag("PlayerReplica").transform;

        PlayerReplica.transform.position = Player.transform.position;
        PlayerReplica.transform.rotation = Player.transform.rotation;
        PlayerReplica.transform.localScale = Player.transform.localScale;

        offset = transform.position - PlayerReplica.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
   
    }

    private void LateUpdate()
    {
        transform.position = PlayerReplica.transform.position + offset;
        //transform.rotation = PlayerReplica.transform.rotation;

        //transform.LookAt(Player);
    }
}
