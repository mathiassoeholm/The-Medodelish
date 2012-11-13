using UnityEngine;
using System.Collections;

public class GUIManager : UnityManager<GUIManager>
{
    public Texture2D PlayerOneText;
    public Texture2D PlayerTwoText;

    private Renderer currentPlayer;


    private void Start()
    {
        GameObject.Find("ScreenOverlay").guiTexture.pixelInset = new Rect(-(Screen.width / 2), -(Screen.height / 2), Screen.width, Screen.height);
        
        this.currentPlayer = GameObject.Find("CurrentPlayer").renderer;

        EventManager.Instance.PassedTurn += this.SetCurrentPlayer;
    }

    private void SetCurrentPlayer(int newCurrentPlayer)
    {
        this.currentPlayer.material.mainTexture = newCurrentPlayer == 1 ? this.PlayerOneText : this.PlayerTwoText;
        iTween.PunchScale(this.currentPlayer.gameObject, iTween.Hash("amount", transform.localScale * 0.16f, "easeType", "easeOutBounce", "time", 1.5f));
    }
}
