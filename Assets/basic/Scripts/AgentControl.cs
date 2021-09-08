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

    [HideInInspector]
    public bool OnEpisode;

    Settings settings;
    DataDisplayScript TextDisplay;

    [HideInInspector]
    public int GreenCounter;
    [HideInInspector]
    public int BlueCounter;
    [HideInInspector]
    public int MissingGreenCounter;

    float dt; // Timer

    public override void Initialize()
    {
        settings = FindObjectOfType<Settings>();
        TextDisplay = FindObjectOfType<DataDisplayScript>();
        Agent_rb = this.GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
        OnEpisode = false;

        this.transform.localPosition = new Vector3(0f, 1.5f, Random.Range(-14.5f, 14.5f));
        this.transform.rotation = Quaternion.Euler(-90f, -90f, 0f);
        Agent_rb.velocity = Vector3.zero;
        Agent_rb.angularVelocity = Vector3.zero;

        GreenCounter = 0;
        BlueCounter = 0;
        MissingGreenCounter = 0;
        dt = 0.0f;

        TextDisplay.UpdateText();
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
        if (OnEpisode)
        {
            dt += Time.deltaTime;
            if (dt > settings.EpisodeLength)
            {
                EndEpisode();
            }

            MoveAgent(actionBuffers);
            TextDisplay.UpdateText();
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
                GreenCounter++;
                AddReward(1.0f);
            }

            else if (col.gameObject.CompareTag("BlueBall"))
            {
                BlueCounter++;
                AddReward(-1.0f);
            }
        }

        else
        {
            if (col.gameObject.CompareTag("Floor")) OnEpisode = true;
        }
    }
}
