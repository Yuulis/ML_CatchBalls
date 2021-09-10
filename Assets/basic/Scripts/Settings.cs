using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    // Training Mode?
    public bool onTraining;

    // Ball spawning interval (sec)
    public float BallSpawnInterval;

    // Blue ball spawing rate (%)
    public int BlueBallRate;

    // Area Type
    public bool HighWall;

    // Agent Speed
    public float AgentSpeed;

    // Episode length (sec)
    public float EpisodeLength;

    // Give Agent Additional Reward?
    public bool AdditionalReward;
}
