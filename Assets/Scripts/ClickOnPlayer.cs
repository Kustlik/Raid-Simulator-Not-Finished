using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickOnPlayer : MonoBehaviour
{
    [SerializeField] public PlayerMenu currentDatabase;
    [SerializeField] public PlayerMenu exportDatabase;
    [SerializeField] GameObject lightenBar;
    [SerializeField] int playerPosition;
    [SerializeField] Text tipWindow;
    int exportposition = 0;
    public PlayerData[] players;
    public PlayerData playerToExport;
    RaidRoosterCounter raidRoosterCounter;

    [SerializeField] ShowCharacterStats showCharacterStats;

    public bool mirrorImgIsActive = false;

    bool showPanelOnce = true;
    bool hidePanelOnce = true;

    void OnMouseOver()
    {
        players = currentDatabase.ReturnPlayerData();
        if ((players[playerPosition] != null) && ((currentDatabase.playerNick[playerPosition].text != "")))
        {
            lightenBar.GetComponent<Image>().color = new Color32(255, 255, 255, 80);

            if ((showPanelOnce == true) && (currentDatabase.playerRole.Length == 0))
            {
                if (currentDatabase.playerBars[playerPosition].GetComponent<Image>().color != new Color32(135, 135, 135, 030))
                {
                    if ((exportDatabase.players[exportDatabase.players.Length - 1] == null) && (currentDatabase.playerNick[playerPosition].text != ""))
                    {
                        players = currentDatabase.ReturnPlayerData();
                        playerToExport = players[playerPosition];
                    }
                }

                showCharacterStats.playerData = players[playerPosition];
                showCharacterStats.allPlayers = currentDatabase;

                showCharacterStats.ShowPanel();
                showPanelOnce = false;
                hidePanelOnce = true;
            }
        }
        if ((players[playerPosition] != null) && (currentDatabase.playerNick[playerPosition].text != "") && (currentDatabase.playerBars[playerPosition].GetComponent<Image>().color == new Color32(135, 135, 135, 030)))
        {
            for (int index = 0; index < 20; index++)
            {
                while ((exportDatabase.players[index] == null) && (index < 19))
                {
                    index++;
                }

                if ((exportDatabase.players[index] == null) && (index == 19))
                {
                    break;
                }

                if (currentDatabase.players[playerPosition].GetPlayerName() == exportDatabase.players[index].GetPlayerName())
                {
                    if (exportDatabase.players[index].GetPlayerRole() == "Tank")
                    {
                        tipWindow.GetComponent<Text>().color = new Color32(255, 0, 0, 255);
                        tipWindow.text = "Player " + currentDatabase.players[playerPosition].GetPlayerName() + " is currently assigned as another role (Tank).";
                    }
                    else if (exportDatabase.players[index].GetPlayerRole() != "Tank")
                    {
                        tipWindow.GetComponent<Text>().color = new Color32(255, 0, 0, 255);
                        tipWindow.text = "Player " + currentDatabase.players[playerPosition].GetPlayerName() + " is currently assigned as another role (" + exportDatabase.players[index].GetPlayerProximity() + " " + exportDatabase.players[index].GetPlayerRole() + ").";
                    }
                }
            }
        }
    }

    void OnMouseExit()
    {
        players = currentDatabase.ReturnPlayerData();
        if ((players[playerPosition] != null) && ((currentDatabase.playerNick[playerPosition].text != "")))
        {
            lightenBar.GetComponent<Image>().color = new Color32(255, 255, 255, 0);

            if ((hidePanelOnce == true) && (currentDatabase.playerRole.Length == 0))
            {
                showCharacterStats.HidePanel();
                showPanelOnce = true;
                hidePanelOnce = false;
            }
        }
        if (currentDatabase.playerRole.Length == 0)
        {
            tipWindow.GetComponent<Text>().color = new Color32(255, 255, 255, 255);
            tipWindow.text = "Choose your Raid group, remember to fullfill minimum requirements. Choosing an affix, invalidates estabilishing new record. Remember that Damage is randomized through different parses, so different simulations gives different results.";
        }
    }

    void OnMouseDown()
    {
        if (currentDatabase.playerBars[playerPosition].GetComponent<Image>().color != new Color32(135, 135, 135, 030))
        {
            if ((exportDatabase.players[exportDatabase.players.Length - 1] == null) && (currentDatabase.playerNick[playerPosition].text != ""))
            {
                players = currentDatabase.ReturnPlayerData();
                playerToExport = players[playerPosition];
                if (mirrorImgIsActive == false)
                {
                    players[playerPosition] = null;
                }
                if (currentDatabase.playerRole.Length == 0)
                {
                    currentDatabase.SortPlayersByRole();
                }
                else
                {
                    currentDatabase.SortPlayers();
                }
                currentDatabase.ManagePlayers();

                while ((exportDatabase.players[exportposition] != null) && (exportposition < exportDatabase.players.Length))
                {
                    exportposition++;
                }

                exportDatabase.players[exportposition] = playerToExport;
                exportposition = 0;

                DeleteDuplicates();

                if (exportDatabase.playerRole.Length == 0)
                {
                    exportDatabase.SortPlayersByRole();
                }
                else
                {
                    exportDatabase.SortPlayers();
                }
                exportDatabase.ManagePlayers();
                lightenBar.GetComponent<Image>().color = new Color32(255, 255, 255, 0);

                if(mirrorImgIsActive == false)
                {
                    GreyAlternateRoles();
                }
            }
        }
        //    GetComponent<Transform>().localScale = new Vector3(0.74f, 0.74f, 1);
        players = currentDatabase.ReturnPlayerData();
        if ((players[playerPosition] != null) && ((currentDatabase.playerNick[playerPosition].text != "")))
        {
            lightenBar.GetComponent<Image>().color = new Color32(255, 255, 255, 80);

            if (currentDatabase.playerRole.Length == 0)
            {
                if (currentDatabase.playerBars[playerPosition].GetComponent<Image>().color != new Color32(135, 135, 135, 030))
                {
                    if ((exportDatabase.players[exportDatabase.players.Length - 1] == null) && (currentDatabase.playerNick[playerPosition].text != ""))
                    {
                        players = currentDatabase.ReturnPlayerData();
                        playerToExport = players[playerPosition];
                    }
                }

                showCharacterStats.playerData = players[playerPosition];
                showCharacterStats.allPlayers = currentDatabase;

                showCharacterStats.ShowPanel();
            }
        }
        else if (players[playerPosition] == null)
        {
            if ((hidePanelOnce == true) && (currentDatabase.playerRole.Length == 0))
            {
                showCharacterStats.HidePanel();
                showPanelOnce = true;
                hidePanelOnce = false;
            }
        }

    }

    public void DeleteDuplicates()
    {
        int index = 0;

        if (currentDatabase.playerRole.Length == 0)
        {
            while (index < 31)
            {
                while ((currentDatabase.players[index] == null) && (index < 30))
                {
                    index++;
                }

                if ((currentDatabase.players[index] == null) && (index == 30))
                {
                    break;
                }

                for (int playerCheck = 0; playerCheck < 32; playerCheck++)
                {
                    while (((currentDatabase.players[playerCheck] == null) && (playerCheck < 31)) || ((playerCheck == index) && (playerCheck < 31)))
                    {
                        playerCheck++;
                    }

                    if ((currentDatabase.players[playerCheck] == null) && (playerCheck == 31))
                    {
                        break;
                    }

                    if ((currentDatabase.players[index].GetPlayerName() == currentDatabase.players[playerCheck].GetPlayerName()) 
                        && (currentDatabase.players[index].GetPlayerRole() == currentDatabase.players[playerCheck].GetPlayerRole())
                        && (currentDatabase.players[index].GetPlayerProximity() == currentDatabase.players[playerCheck].GetPlayerProximity()))
                    {
                        currentDatabase.players[index] = null;
                        index = 0;
                        break;
                    }
                }
                index++;
            }
        }

        else if (exportDatabase.playerRole.Length == 0)
        {
            while (index < 31)
            {
                while ((exportDatabase.players[index] == null) && (index < 30))
                {
                    index++;
                }

                if ((exportDatabase.players[index] == null) && (index == 30))
                {
                    break;
                }

                for (int playerCheck = 0; playerCheck < 32; playerCheck++)
                {
                    while (((exportDatabase.players[playerCheck] == null) && (playerCheck < 31)) || ((playerCheck == index) && (playerCheck < 31)))
                    {
                        playerCheck++;
                    }

                    if ((exportDatabase.players[playerCheck] == null) && (playerCheck == 31))
                    {
                        break;
                    }

                    if ((exportDatabase.players[index].GetPlayerName() == exportDatabase.players[playerCheck].GetPlayerName())
                        && (exportDatabase.players[index].GetPlayerRole() == exportDatabase.players[playerCheck].GetPlayerRole())
                        && (exportDatabase.players[index].GetPlayerProximity() == exportDatabase.players[playerCheck].GetPlayerProximity()))
                    {
                        exportDatabase.players[index] = null;
                        index = 0;
                        break;
                    }
                }
                index++;
            }
        }
    }

    public void GreyAlternateRoles()
    {
        int index = 0;

        if (currentDatabase.playerRole.Length == 0)
        {
            while (index < 32)
            {
                while ((currentDatabase.players[index] == null) && (index < 31))
                {
                    index++;
                }

                if ((currentDatabase.players[index] == null) && (index == 31))
                {
                    break;
                }

                for (int roosterIndex = 0; roosterIndex < 20; roosterIndex++)
                {
                    while ((exportDatabase.players[roosterIndex] == null) && (roosterIndex < 19))
                    {
                        roosterIndex++;
                    }

                    if ((exportDatabase.players[roosterIndex] == null) && (roosterIndex == 19))
                    {
                        break;
                    }

                    if ((currentDatabase.players[index].GetPlayerName() == exportDatabase.players[roosterIndex].GetPlayerName()) && (mirrorImgIsActive == false) && (currentDatabase.playerNick[index].text != ""))
                    {
                        currentDatabase.playerBars[index].GetComponent<Image>().color = new Color32(135, 135, 135, 030);
                        currentDatabase.playerClass[index].GetComponent<Image>().color = new Color32(255, 255, 255, 030);
                        currentDatabase.playerNick[index].GetComponent<Text>().color = new Color32(255, 255, 255, 030);
                    }
                }
                index++;
            }
        }
        else
        {
            while (index < 32)
            {
                while ((exportDatabase.players[index] == null) && (index < 31))
                {
                    index++;
                }

                if ((exportDatabase.players[index] == null) && (index == 31))
                {
                    break;
                }

                for (int roosterIndex = 0; roosterIndex < 20; roosterIndex++)
                {
                    while ((currentDatabase.players[roosterIndex] == null) && (roosterIndex < 19))
                    {
                        roosterIndex++;
                    }

                    if ((currentDatabase.players[roosterIndex] == null) && (roosterIndex == 19))
                    {
                        break;
                    }

                    if ((exportDatabase.players[index].GetPlayerName() == currentDatabase.players[roosterIndex].GetPlayerName()) && (mirrorImgIsActive == false) && (exportDatabase.playerNick[index].text != ""))
                    {
                        exportDatabase.playerBars[index].GetComponent<Image>().color = new Color32(135, 135, 135, 030);
                        exportDatabase.playerClass[index].GetComponent<Image>().color = new Color32(255, 255, 255, 030);
                        exportDatabase.playerNick[index].GetComponent<Text>().color = new Color32(255, 255, 255, 030);
                    }
                }
                index++;
            }
        }
    }
}
