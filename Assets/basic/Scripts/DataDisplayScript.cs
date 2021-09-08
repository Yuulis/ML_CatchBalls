using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DataDisplayScript : MonoBehaviour
{
    AgentControl control;

    [SerializeField]
    private TextMeshProUGUI CatchedGreenText;
    [SerializeField]
    private TextMeshProUGUI CatchedBlueText;
    [SerializeField]
    private TextMeshProUGUI MissingGreenText;
    [SerializeField]
    private TextMeshProUGUI CatchingRateText;

    void Start()
    {
        control = FindObjectOfType<AgentControl>();
    }
}
