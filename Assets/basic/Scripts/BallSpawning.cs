using System.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallSpawning : MonoBehaviour
{
    public GameObject Area;
    public GameObject GreenBall;
    public GameObject BlueBall;

    Settings settings;
    AgentControl control;

    float dt; // Timer

    void Start()
    {
        settings = FindObjectOfType<Settings>();
        control = FindObjectOfType<AgentControl>();
    }

    void Update()
    {
        dt += Time.deltaTime;
        if (dt > settings.BallSpawnInterval)
        {
            dt = 0.0f;
            if (control.OnEpisode) SpawnBalls();
        }
    }

    public void SpawnBalls()
    {
        if (settings.HighWall) this.transform.localPosition = new Vector3(0f, 40.0f, Random.Range(-14.5f, 14.5f));
        else this.transform.localPosition = new Vector3(0f, 20.0f, Random.Range(-14.5f, 14.5f));

        int rand = Random.Range(1, 100 + 1);
        if (rand <= settings.BlueBallRate)
        {
            Instantiate(BlueBall, this.transform.localPosition + Area.transform.localPosition, Quaternion.identity);
        }
        else
        {
            Instantiate(GreenBall, this.transform.localPosition + Area.transform.localPosition, Quaternion.identity);
        }
    }
}
