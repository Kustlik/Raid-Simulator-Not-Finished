using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashScript : MonoBehaviour
{
    [SerializeField] Sprite trashEmpty;
    [SerializeField] Sprite trashFull;

    [SerializeField] public PlayerMenu currentDatabase;
    [SerializeField] public ClickOnPlayer clickOnPlayer;

    void OnMouseDown()
    {
        GetComponent<Image>().sprite = trashFull;
        int index = 0;

        while (index < currentDatabase.players.Length)
        {
            while ((currentDatabase.players[index] == null) && (index < (currentDatabase.players.Length - 1)))
            {
                index++;
            }

            if ((currentDatabase.players[index] == null) && (index == currentDatabase.players.Length - 1))
            {
                break;
            }

            if((currentDatabase.players[index].GetPlayerName() == "Alammar") ||
                (currentDatabase.players[index].GetPlayerName() == "Alarien") ||
                (currentDatabase.players[index].GetPlayerName() == "Dwulicowa kurwa") ||
                (currentDatabase.players[index].GetPlayerName() == "Tanatox") ||
                (currentDatabase.players[index].GetPlayerName() == "Vejrea") ||
                (currentDatabase.players[index].GetPlayerName() == "Veirena") ||
                (currentDatabase.players[index].GetPlayerName() == "Roisik") ||
                (currentDatabase.players[index].GetPlayerName() == "Ybexx") ||
                (currentDatabase.players[index].GetPlayerName() == "Wizjoner") ||
                (currentDatabase.players[index].GetPlayerName() == "Niizuk") ||
                (currentDatabase.players[index].GetPlayerName() == "Goldtaker") ||
                (currentDatabase.players[index].GetPlayerName() == "Garolock") ||
                (currentDatabase.players[index].GetPlayerName() == "Garolotrzyk") ||
                (currentDatabase.players[index].GetPlayerName() == "Zÿlu") ||
                (currentDatabase.players[index].GetPlayerName() == "Tishaiya") ||
                (currentDatabase.players[index].GetPlayerName() == "Fundrago"))
            {
                currentDatabase.players[index] = null;
                index = 0;
            }
            index++;
        }

        currentDatabase.SortPlayersByRole();
        currentDatabase.ManagePlayers();
        clickOnPlayer.GreyAlternateRoles();
    }

    void Update()
    {
        if (GetComponent<Image>().sprite == trashFull)
        {
            int index = 0;

            while (index < currentDatabase.players.Length)
            {
                while ((currentDatabase.players[index] == null) && (index < (currentDatabase.players.Length - 1)))
                {
                    index++;
                }

                if ((currentDatabase.players[index] == null) && (index == currentDatabase.players.Length - 1))
                {
                    break;
                }

                if ((currentDatabase.players[index].GetPlayerName() == "Alammar") ||
                    (currentDatabase.players[index].GetPlayerName() == "Alarien") ||
                    (currentDatabase.players[index].GetPlayerName() == "Dwulicowa kurwa") ||
                    (currentDatabase.players[index].GetPlayerName() == "Tanatox") ||
                    (currentDatabase.players[index].GetPlayerName() == "Vejrea") ||
                    (currentDatabase.players[index].GetPlayerName() == "Veirena") ||
                    (currentDatabase.players[index].GetPlayerName() == "Roisik") ||
                    (currentDatabase.players[index].GetPlayerName() == "Ybexx") ||
                    (currentDatabase.players[index].GetPlayerName() == "Wizjoner") ||
                    (currentDatabase.players[index].GetPlayerName() == "Niizuk") ||
                    (currentDatabase.players[index].GetPlayerName() == "Goldtaker") ||
                    (currentDatabase.players[index].GetPlayerName() == "Garolock") ||
                    (currentDatabase.players[index].GetPlayerName() == "Garolotrzyk") ||
                    (currentDatabase.players[index].GetPlayerName() == "Zÿlu") ||
                    (currentDatabase.players[index].GetPlayerName() == "Tishaiya") ||
                    (currentDatabase.players[index].GetPlayerName() == "Fundrago"))
                {
                    GetComponent<Image>().sprite = trashEmpty;
                    break;
                }
                index++;
            }
        }
    }
}
