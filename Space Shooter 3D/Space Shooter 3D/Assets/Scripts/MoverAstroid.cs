using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverAstroid : MonoBehaviour
{
    public float speed;
    public Vector3 randV;

    private void Start()
    {
        //Random speed vector for astroid
        randV = new Vector3(Random.Range(-5f,5f), Random.Range(-5f, 5f), Random.Range(-10f, 10f));
    }

    void LateUpdate()
    {
        GetComponent<Rigidbody>().velocity = randV * speed;
    }
}
