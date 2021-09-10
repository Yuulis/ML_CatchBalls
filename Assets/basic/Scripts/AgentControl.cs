using System.Runtime.CompilerServices;
using System.Globalization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class AgentControl : Agent
{
    Rigidbody Agent_rb;

    [HideInInspector] public bool OnEpisode;

    // Other Scripts
    Settings settings;
    DataDisplayScript displayer;

    float dt; // Timer

    public override void Initialize()
    {
        settings = FindObjectOfType<Settings>();
        if (!settings.onTraining) displayer = FindObjectOfType<DataDisplayScript>();

        Agent_rb = this.GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
        OnEpisode = false;

        this.transform.localPosition = new Vector3(0f, 1.5f, Random.Range(-14.5f, 14.5f));
        this.transform.rotation = Quaternion.Euler(-90f, -90f, 0f);
        Agent_rb.velocity = Vector3.zero;
        Agent_rb.angularVelocity = Vector3.zero;

        if (!settings.onTraining)
        {
            displayer.AllGreenCnt = 0;
            displayer.CatchGreenCnt = 0;
            displayer.CatchBlueCnt = 0;
            displayer.MissGreenCnt = 0;
            displayer.Rate = 0f;

            displayer.DisplayUpdate();
        }

        dt = 0.0f;
    }

    public void MoveAgent(ActionBuffers actionBuffers)
    {
        var continuousActions = actionBuffers.ContinuousActions;
        var sideAction = Mathf.Clamp(continuousActions[0], -1f, 1f);

        this.transform.localPosition = new Vector3(
            this.transform.localPosition.x,
            this.transform.localPosition.y,
            Mathf.Clamp(transform.localPosition.z + (sideAction * settings.AgentSpeed * Time.deltaTime), -14.5f, 14.5f)
        );
    }

    public void GiveAdditionalReward()
    {
        AddReward(-1.0f / (settings.BallSpawnInterval * settings.EpisodeLength * (1 - settings.BlueBallRate)));
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        if (!settings.onTraining)
        {
            if (displayer.AllGreenCnt != 0) displayer.Rate = ((float)displayer.CatchGreenCnt / displayer.AllGreenCnt) * 100f;
            displayer.DisplayUpdate();
        }

        if (OnEpisode)
        {
            dt += Time.deltaTime;
            if (dt > settings.EpisodeLength)
            {
                EndEpisode();
            }

            MoveAgent(actionBuffers);
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;

        if (Input.GetKey(KeyCode.D)) continuousActionsOut[0] = 1;  // Right
        if (Input.GetKey(KeyCode.A)) continuousActionsOut[0] = -1;  // Left
    }

    private void OnCollisionEnter(Collision col)
    {
        if (OnEpisode)
        {
            if (col.gameObject.CompareTag("GreenBall"))
            {
                AddReward(1.0f);
                if (!settings.onTraining)
                {
                    displayer.CatchGreenCnt++;
                    displayer.AllGreenCnt++;
                }
            }

            else if (col.gameObject.CompareTag("BlueBall"))
            {
                AddReward(-1.0f);
            }
        }

        else
        {
            if (col.gameObject.CompareTag("Floor")) OnEpisode = true;
        }
    }
}
