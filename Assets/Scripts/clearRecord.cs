using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class clearRecord : MonoBehaviour
{
    [SerializeField] WinCondition winCondition;

    void OnMouseDown()
    {
        winCondition.record = 0;
    }
}
