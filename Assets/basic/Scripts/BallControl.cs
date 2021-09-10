using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    AgentControl control;
    Settings settings;
    DataDisplayScript displayer;

    void Start()
    {
        control = FindObjectOfType<AgentControl>();
        settings = FindObjectOfType<Settings>();
        displayer = FindObjectOfType<DataDisplayScript>();
    }

    void Update()
    {
        if (!control.OnEpisode) Destroy(this.gameObject);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Floor"))
        {
            if (this.gameObject.CompareTag("GreenBall") && settings.AdditionalReward)
            {
                if (!settings.onTraining)
                {
                    displayer.MissGreenCnt++;
                    displayer.AllGreenCnt++;
                }

                control.GiveAdditionalReward();
            }

            Destroy(this.gameObject);
        }
        else if (col.gameObject.CompareTag("Agent"))
        {
            if (this.gameObject.CompareTag("BlueBall") && !settings.onTraining)
            {
                displayer.CatchBlueCnt++;
            }
            Destroy(this.gameObject);
        }
    }
}
