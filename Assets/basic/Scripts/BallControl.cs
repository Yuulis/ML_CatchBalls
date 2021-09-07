using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    AgentControl control;
    Settings settings;

    void Start()
    {
        control = FindObjectOfType<AgentControl>();
        settings = FindObjectOfType<Settings>();
    }

    void Update()
    {
        if (!control.OnEpisode) Destroy(this.gameObject);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Floor"))
        {
            if (col.gameObject.CompareTag("GreenBall") && settings.AdditionalReward)
            {
                control.GiveAdditionalReward();
            }

            Destroy(this.gameObject);
        }
        else if (col.gameObject.CompareTag("Agent"))
        {
            Destroy(this.gameObject);
        }
    }
}
