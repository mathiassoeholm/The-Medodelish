using System;
using System.Collections.Generic;

using UnityEngine;
using System.Collections;

public class GameManager : UnityManager<GameManager>
{
    public Texture2D switchActiveTex;
    public Texture2D switchInactiveTex;
    public GameObject StitchPrefab;
    public Vector3 SwitchStartPos;

    private const float StitchSpacingX = 2;
    private const float StitchSpacingY = 1;
    private const float MaxStitchPosX = 12.21619f;

    private List<Monster> monsterCombination = new List<Monster>();
    private List<Renderer> switches = new List<Renderer>(); 

    private int currentMonsterIndex;
    private int currentPlayer = 1; // 1 or 2

    private Vector3 nextSwitchPositon;
    private Vector3 titleStartScale;
    private Renderer titleRenderer;
    private Renderer wonRenderer;
    private Renderer currentPlayerRenderer;

    void Awake()
    {
        this.nextSwitchPositon = this.SwitchStartPos;
        this.titleRenderer = GameObject.Find("Title").renderer;
        this.titleStartScale = this.titleRenderer.transform.localScale;
        this.wonRenderer = GameObject.Find("Won").renderer;
        this.currentPlayerRenderer = GameObject.Find("CurrentPlayer").renderer;
    }

    void Start()
    {
        EventManager.Instance.GameOver += this.EndGame;
    }

    void AddStitch()
    {
        this.switches.Add(((GameObject)Instantiate(this.StitchPrefab, this.nextSwitchPositon, this.StitchPrefab.transform.rotation)).renderer);

        if (this.nextSwitchPositon.x + StitchSpacingX > MaxStitchPosX)
        {
            this.nextSwitchPositon.x = this.SwitchStartPos.x;
            this.nextSwitchPositon.y -= StitchSpacingY;
        }
        else
        {
            this.nextSwitchPositon.x += StitchSpacingX;
        }
    }

    void EndGame(int playerFailed)
    {
        // Set player won text
        currentPlayerRenderer.material.mainTexture = playerFailed == 1
                                                         ? GUIManager.Instance.PlayerTwoText
                                                         : GUIManager.Instance.PlayerOneText;

        wonRenderer.enabled = true;

        // Bounce effect on status text
        iTween.PunchScale(
                GameObject.Find("StatusText"),
                iTween.Hash("amount", GameObject.Find("StatusText").transform.localScale * 0.2f, "easeType", "easeOutBounce", "time", 1.2f));

        // Remove all switches
        for (int i = switches.Count - 1; i >= 0; i--)
        {
            Destroy(switches[i].gameObject);
        }

        // Write out player who lost
        this.currentPlayerRenderer.material.mainTexture = currentPlayer == 1
                                                              ? GUIManager.Instance.PlayerTwoText
                                                              : GUIManager.Instance.PlayerOneText;

        // Make sure player 1 starts
        this.currentPlayer = 1;

        // Clear collections
        switches = new List<Renderer>();
        monsterCombination = new List<Monster>();

        // Reset switch pos
        nextSwitchPositon = SwitchStartPos;

        // Reset index
        currentMonsterIndex = 0;

        // Show title
        iTween.ScaleTo(
                this.titleRenderer.gameObject,
                iTween.Hash("scale", titleStartScale, "easeType", "easeInCubic", "time", 0.8f));
    }

    void DeactivateAllSwitches()
    {
        foreach (Renderer switchRender in this.switches)
        {
            switchRender.material.mainTexture = this.switchInactiveTex;
        }
    }

    void ActivateSwitch(int switchIndex)
    {
        this.switches[switchIndex].material.mainTexture = this.switchActiveTex;
        iTween.PunchScale(this.switches[switchIndex].gameObject, iTween.Hash("amount", transform.localScale * 0.16f, "easeType", "easeOutBounce", "time", 1.5f));
    }

    public void AddMonster(Monster monster)
    {
        // Do special stuff for first monster
        if (this.monsterCombination.Count == 0)
        {
            // Remove title
            iTween.ScaleTo(
                this.titleRenderer.gameObject,
                iTween.Hash("scale", Vector3.zero, "easeType", "easeOutCubic", "time", 0.8f));

            // Remove won text
            wonRenderer.enabled = false;

            // Show current player text
            currentPlayerRenderer.enabled = true;

            // Add switch
            this.AddStitch();
        }

        // If this is the new monster, add it to the list
        if (this.currentMonsterIndex == this.monsterCombination.Count)
        {
    
            // Adds the monster
            this.monsterCombination.Add(monster);

            this.AddStitch();
            this.ActivateSwitch(this.currentMonsterIndex);

            // Pass turn
            this.currentMonsterIndex = 0;
            this.currentPlayer = this.currentPlayer == 1 ? 2 : 1;
            EventManager.Instance.PassedTurn(this.currentPlayer);

            this.DeactivateAllSwitches();
        }
        else if (this.monsterCombination[this.currentMonsterIndex] != monster)
        {
            // Tapped on wrong monster :-(
            EventManager.Instance.GameOver(this.currentPlayer);
        }
        else
        {
            this.ActivateSwitch(this.currentMonsterIndex);
            
            // Guess was correct, proceed
            this.currentMonsterIndex++;
        }
    }
}
