using System;

using UnityEngine;
using System.Collections;

public class EventManager : UnityManager<EventManager>
{
    public Action<Monster> TappedOnMonster;
    public Action<int> PassedTurn;
    public Action<int> GameOver;
    public Action<bool> OnMusicBeat;

    private void Awake()
    {
        this.AddDebugLogs();
    }

    private void AddDebugLogs()
    {
        this.TappedOnMonster     += (monster) => Debug.Log("Tapped on " + monster.name);
        this.PassedTurn          += (playersTurn) => Debug.Log("Combination completed succesfully, passed turn to player " + playersTurn);
        this.GameOver            += (playerFailed) => Debug.Log("Player " + playerFailed + " failed");
        this.OnMusicBeat         += (playerFailed) => Debug.Log("Beat");
    }
}
