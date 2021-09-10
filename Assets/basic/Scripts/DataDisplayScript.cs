using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DataDisplayScript : MonoBehaviour
{
    AgentControl control;

    [SerializeField] private TextMeshProUGUI AllGreenText;
    [SerializeField] private TextMeshProUGUI CatchedGreenText;
    [SerializeField] private TextMeshProUGUI CatchedBlueText;
    [SerializeField] private TextMeshProUGUI MissingGreenText;
    [SerializeField] private TextMeshProUGUI AchievedRateText;

    [HideInInspector] public int AllGreenCnt;
    [HideInInspector] public int CatchGreenCnt;
    [HideInInspector] public int CatchBlueCnt;
    [HideInInspector] public int MissGreenCnt;
    [HideInInspector] public float Rate;

    void Start()
    {
        control = FindObjectOfType<AgentControl>();
    }

    public void DisplayUpdate()
    {
        AllGreenText.text = "(All) Green: " + AllGreenCnt.ToString();
        CatchedGreenText.text = "(Catch) Green: " + CatchGreenCnt.ToString();
        CatchedBlueText.text = "(Catch) Blue: " + CatchBlueCnt.ToString();
        MissingGreenText.text = "(Miss) Green: " + MissGreenCnt.ToString();
        AchievedRateText.text = "Rate: " + Rate.ToString() + '%';
    }
}
