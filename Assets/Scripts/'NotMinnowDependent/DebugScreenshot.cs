using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;
using System.Collections;

public class DebugScreenshot : UnityManager<DebugScreenshot>
{
    private const float DebugInfoVisibleTime = 2;
    private const int GUILabelHeight = 20;
    private const int GUILabelWidth = 400;
    private const string DirectoryName = "DebugScreenshots";
    
    public KeyCode CaptureKey = KeyCode.F10;

    private float lastCaptureTime = -DebugInfoVisibleTime;
    private string screenshotName;
    private GUIStyle alignLeft;
    private List<Func<string>> debugInfoToOutput; 

    void Start()
    {
        debugInfoToOutput = new List<Func<string>>();
        
        // Set gui style
        Texture2D black = new Texture2D(1, 1);
        alignLeft = new GUIStyle();
        alignLeft.alignment = TextAnchor.MiddleLeft;
        black.SetPixel(0,0,Color.black);
        alignLeft.normal.background = black;
        alignLeft.normal.textColor = Color.black;
    }

    void Update()
    {
        if (Input.GetKeyDown(CaptureKey))
        {
            screenshotName = "MinnowScreen[" + DateTime.Now.ToString("d-M-yyyy-HH-mm-ss", CultureInfo.InvariantCulture) + "]";

            if (!Directory.Exists(DirectoryName))
                Directory.CreateDirectory(DirectoryName);

            Application.CaptureScreenshot(DirectoryName + "/" + screenshotName + ".png");
            lastCaptureTime = Time.time;
        }
    }

    void OnGUI()
    {
        // Writes info for to seconds when taking a screenshot
        if (Time.time > lastCaptureTime + DebugInfoVisibleTime) return;

        int top = 0;
            
        // Output file name and author
        GUI.Box(new Rect(0, top, GUILabelWidth, GUILabelHeight), screenshotName, alignLeft);
        GUI.Box(new Rect(0, top += GUILabelHeight, GUILabelWidth, GUILabelHeight), "Captured by " + Environment.UserName + " on " + Environment.MachineName, alignLeft);
            
        // Seperator
        GUI.Box(new Rect(0, top += GUILabelHeight, GUILabelWidth, GUILabelHeight), " ", alignLeft);

        // Output player pos
        GUI.Box(new Rect(0, top += GUILabelHeight, GUILabelWidth, GUILabelHeight), "Player position:  " + GameObject.FindGameObjectWithTag("Player").transform.position.ToString(), alignLeft);

        // Write more info
        foreach (Func<string> debugInfo in debugInfoToOutput)
        {
            GUI.Box(new Rect(0, top += GUILabelHeight, GUILabelWidth, GUILabelHeight), debugInfo(), alignLeft);
        }
    }

    /// <summary>
    /// Adds a method that returns a string which will be outputted on the screenshots
    /// </summary>
    public void AddInfoToOutput(Func<string> debugInfo)
    {
        debugInfoToOutput.Add(debugInfo);
    }
}
