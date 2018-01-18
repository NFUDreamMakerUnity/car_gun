using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    private GameObject player;
    public float farRange;

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            IfDestroy();
        }                
    }

    void IfDestroy()
    {
        float range = Vector3.Distance(transform.position, player.transform.position);
        if (range > farRange)
        {
            Destroy(gameObject);
        }
    }
}
