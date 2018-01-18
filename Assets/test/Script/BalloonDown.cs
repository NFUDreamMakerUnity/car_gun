using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonDown : MonoBehaviour
{
    public Transform[] downPos;
    public GameObject[] balloonPrefabs;
    private GameObject BolloonParent;

    public float startDelayTime;
    public float delayTime;

    private void Start()
    {
        BolloonParent = GameObject.FindGameObjectWithTag("BolloonParent");
        StartCoroutine("waitTime",startDelayTime);        
    }

    IEnumerator waitTime(float time)
    {        
        yield return new WaitForSeconds(time);
        StartCoroutine("DelayCreat");
    }

    IEnumerator DelayCreat()
    {
        creatBalloon();
        yield return new WaitForSeconds(delayTime);
        StartCoroutine("DelayCreat");
    }

    void creatBalloon()
    {
        Transform pos = downPos[Random.Range(0, downPos.Length)];
        GameObject prefab = balloonPrefabs[Random.Range(0, balloonPrefabs.Length)];

        GameObject tmpPrefab = Instantiate(prefab, pos.position, Quaternion.identity);
        tmpPrefab.transform.SetParent(BolloonParent.transform);
    }

    
}
