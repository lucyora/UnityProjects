using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour {

    public Transform PlayerReplica;
    public Transform MinimapCam;

    private void LateUpdate()
    {
        MinimapCam = GameObject.FindWithTag("MinimapCamera").transform;
        PlayerReplica = GameObject.FindWithTag("PlayerReplica").transform;
        Vector3 newPosition = PlayerReplica.position;

        newPosition.y = transform.position.y;
        MinimapCam.position = newPosition;
    }
}
