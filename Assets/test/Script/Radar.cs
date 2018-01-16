using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    public Transform Player;

    private void LateUpdate()
    {
        Vector3 newPosition = Player.transform.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        transform.rotation = Quaternion.Euler(90f, Player.eulerAngles.y, 0);
    }
}
