using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] Text score;
    [SerializeField] Text surplusBullet;

    private int nowScore;
    private float nowBullet;

    public float maxBulletAmount;
    [SerializeField] Image bulletBar;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        nowBullet = maxBulletAmount;
    }

    private void Update()
    {
        showUI();
    }

    public void AddScore(int i)
    {
        nowScore += i;
    }

    public void AddBullet(float i)
    {
        nowBullet += i;

        if (nowBullet < 0)
        {
            nowBullet = 0;
        }
    }

    void showUI()
    {
        score.text = "得分: " + nowScore;
        surplusBullet.text = nowBullet.ToString();
        bulletBar.fillAmount = nowBullet / maxBulletAmount;
    }
}
