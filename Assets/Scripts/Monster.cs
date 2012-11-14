using System.Collections.Generic;

using UnityEngine;
using System.Collections;

public class Monster : BaseMonoBehaviour
{
    private const float MoveSpeed = 15;
    private const float BeatPeek = 0.3f;
    
    public static List<Monster> Monsters { get; private set; }

    public AudioClip EndSound;
    public float AudioPitch = 1;
    public float MaxYPos;

    private float startYPos;
    private Vector3 target;

    private bool isIdle;

    // Methods
    void Start()
    {
        Monsters = Monsters ?? new List<Monster>();

        target = transform.position;
        startYPos = transform.position.y;
        isIdle = true;

        EventManager.Instance.OnMusicBeat += this.BeatAction;

        // Add to static monster list
        Monsters.Add(this);
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target, MoveSpeed * Time.deltaTime);
    }

    private void BeatAction(bool beatInvert)
    {
        if(isIdle)
        {
            target.y = beatInvert ? startYPos : startYPos + BeatPeek;
        }
    }

    public void StartSound()
    {
        audio.pitch = this.AudioPitch;
        audio.Play();
        isIdle = false;
    }

    public void StopSound()
    {
        audio.Stop();
        AudioManager.Instance.PlaySound(this.EndSound, 1, this.AudioPitch);
        isIdle = true;
    }

    public void GoDown()
    {
        target = new Vector3(transform.position.x, startYPos, transform.position.z);
    }

    public void GoUp()
    {
        target = new Vector3(transform.position.x, MaxYPos, transform.position.z);
    }
}
