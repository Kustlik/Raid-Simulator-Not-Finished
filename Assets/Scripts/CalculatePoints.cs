using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalculatePoints : MonoBehaviour
{
    [SerializeField] GameObject mainScreen;
    [SerializeField] TextMesh[] scoreTexts;
    [SerializeField] Text kills;
    [SerializeField] Text fortifyPoints;

    int killAmount = 0;
    int fortifyAmount = 0;
    int tempFortifyAmount = 0;

    void Start()
    {
        for(int index = 0; index < scoreTexts.Length; index++)
        {
            scoreTexts[index].text = "ALIVE";
            kills.text = "0/37";
            fortifyPoints.text = "0";
        }
    }

    void Update()
    {
        if (mainScreen.activeInHierarchy == true)
        {
            killAmount = 0;
            fortifyAmount = 0;

            for (int index = 0; index < scoreTexts.Length; index++)
            {
                if(scoreTexts[index].text != "ALIVE")
                {
                    killAmount++;
                    fortifyAmount++;

                    if (scoreTexts[index].text != "DEAD")
                    {
                        scoreTexts[index].GetComponent<TextMesh>().color = new Color32(255, 255, 0, 255);

                        int.TryParse(scoreTexts[index].text, out tempFortifyAmount);
                        fortifyAmount = fortifyAmount + (tempFortifyAmount - 1);
                        tempFortifyAmount = 0;
                    }
                }
            }

            kills.text = killAmount.ToString() + "/37";
            fortifyPoints.text = fortifyAmount.ToString();

            if (kills.text == "37/37")
            {
                kills.GetComponent<Text>().color = new Color32(255, 255, 0, 255);
            }
        }
    }
}
