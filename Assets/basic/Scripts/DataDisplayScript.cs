using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DataDisplayScript : MonoBehaviour
{
    AgentControl control;

    [SerializeField]
    private TextMeshProUGUI CatchedGreen;
    [SerializeField]
    private TextMeshProUGUI CatchedBlue;
    [SerializeField]
    private TextMeshProUGUI MissingGreen;
    [SerializeField]
    private TextMeshProUGUI GreenRate;
    void Start()
    {
        control = FindObjectOfType<AgentControl>();
        CatchedGreen.text = "Green: 0";
        CatchedGreen.text = "Blue: 0";
        CatchedGreen.text = "(Miss) Green: 0";
        CatchedGreen.text = "Catching Rate: 0%";
    }

    public void UpdateText()
    {
        CatchedGreen.text = "Green: " + control.GreenCounter.ToString();
        CatchedBlue.text = "Blue: " + control.BlueCounter.ToString();
        MissingGreen.text = "(Miss) Green: " + control.MissingGreenCounter.ToString();

        int sum = control.GreenCounter + control.BlueCounter + control.MissingGreenCounter;
        GreenRate.text = "Catching Rate: " + sum.ToString() + '%';
    }
}
