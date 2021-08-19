using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mirrorImg : MonoBehaviour
{
    [SerializeField] StartSimulationEvent simulationFile;
    [SerializeField] GameObject lightenBar;
    [SerializeField] GameObject chooseAffix;
    [SerializeField] ClickOnPlayer[] exportingScript;
    [SerializeField] PlayerMenu currentDatabase;
    [SerializeField] PlayerMenu exportDatabase;

    void OnMouseOver()
    {
        lightenBar.GetComponent<Image>().color = new Color32(255, 255, 255, 80);
    }

    void OnMouseExit()
    {
        lightenBar.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
    }

    void OnMouseDown()
    {
        for (int index = 0; index < 32; index++)
        {
            exportingScript[index].mirrorImgIsActive = !exportingScript[index].mirrorImgIsActive;
            Debug.Log(exportingScript[index].mirrorImgIsActive);
        }

        lightenBar.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        if (chooseAffix.activeInHierarchy == true)
        {
            int activeCheck = 0;

            chooseAffix.SetActive(false);
            simulationFile.Affix[9] = false;

            for(int index = 0; index < 20; index++)
            {
                while (exportDatabase.players[activeCheck] != null)
                {
                    activeCheck++;
                }

                if (currentDatabase.players[index] != null)
                {
                    exportDatabase.players[activeCheck] = currentDatabase.players[index];
                    currentDatabase.players[index] = null;
                }
            }

            exportingScript[0].DeleteDuplicates();
            exportDatabase.SortPlayersByRole();
            exportDatabase.ManagePlayers();
            currentDatabase.ManagePlayers();

            exportingScript[0].DeleteDuplicates();
            exportDatabase.SortPlayersByRole();
            exportDatabase.ManagePlayers();
            currentDatabase.ManagePlayers();
        }
        else if (chooseAffix.activeInHierarchy == false)
        {
            int activeCheck = 0;

            chooseAffix.SetActive(true);
            simulationFile.Affix[9] = true;

            for (int index = 0; index < 20; index++)
            {
                while (exportDatabase.players[activeCheck] != null)
                {
                    activeCheck++;
                }

                if (currentDatabase.players[index] != null)
                {
                    exportDatabase.players[activeCheck] = currentDatabase.players[index];
                    currentDatabase.players[index] = null;
                }
            }

            exportingScript[0].DeleteDuplicates();
            exportDatabase.SortPlayersByRole();
            exportDatabase.ManagePlayers();
            currentDatabase.ManagePlayers();

            exportingScript[0].DeleteDuplicates();
            exportDatabase.SortPlayersByRole();
            exportDatabase.ManagePlayers();
            currentDatabase.ManagePlayers();
        }
        simulationFile.ResetAffixBar();
    }
}
