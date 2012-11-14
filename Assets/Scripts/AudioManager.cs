using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioCollection
{
    public int repeatInterval;
    public AudioClip[] audioClips;

    [HideInInspector]
    public List<int> recentlyPlayedSounds = new List<int>();
}

public class AudioManager : UnityManager<AudioManager>
{
    enum MusicState
    {
        FullVolume,
        FadingIn,
        FadingOut,
        Muted
    }

    public float MusicBpm;

    // The music volume when fully faded in, maximum is 1
    public float musicFullVolume = 1;

    // The speed in which the music fades
    public float fadeFactor = 0.15f;

    // Current state of the music (FullVolume, FadingIn, FadingOut or Muted)
    private MusicState musicState = MusicState.Muted;

    // List keeping track of all audio sources in the scene, used to play sound effects
    private List<AudioSource> audioSources = new List<AudioSource>();

    private AudioCollection collectionToPlay;
    private float collectionToPlayVolume;
    private float collectionToPlayPitch;

    private float nextBeat;
    private bool beatInverter;

    // Use this for initialization
    void Start()
    {
        // Make the music fade in
        musicState = MusicState.FadingIn;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMusicFade();

        if (Time.time > nextBeat)
        {
            beatInverter = !beatInverter;
            EventManager.Instance.OnMusicBeat(beatInverter);

            nextBeat = Time.time + 1 / (MusicBpm / 60);
        }
    }

    public void PlayCipFromCollection(AudioCollection collection, float volume = 1, float pitch = 1)
    {
        // Check for repeat interval
        if (collection.audioClips.Length <= collection.repeatInterval)
        {
            Debug.LogError("Number of sounds must be bigger than repeat interval");
            return;
        }
        
        collectionToPlay = collection;
        collectionToPlayVolume = volume;
        collectionToPlayPitch = pitch;

        StartCoroutine("PlayCollectionClip");
    }

    IEnumerator PlayCollectionClip()
    {
        yield return null;

        int clipToPlay;

        do
        {
            clipToPlay = Random.Range(0, collectionToPlay.audioClips.Length);

        } while (collectionToPlay.recentlyPlayedSounds.Contains(clipToPlay));

        collectionToPlay.recentlyPlayedSounds.Add(clipToPlay);

        if (collectionToPlay.recentlyPlayedSounds.Count > collectionToPlay.repeatInterval)
            collectionToPlay.recentlyPlayedSounds.RemoveAt(0);

        PlaySound(collectionToPlay.audioClips[clipToPlay], collectionToPlayVolume, collectionToPlayPitch);
    }

    void HandleMusicFade()
    {
        // We use a switch statement to handle music state
        switch (musicState)
        {
            case MusicState.FadingIn:
                // Add to volume, and use Mathf.Min() to make sure we don't go above full volume
                audio.volume = Mathf.Min(audio.volume + fadeFactor * Time.deltaTime, musicFullVolume);

                // Check if we reached full volume and switch state to FullVolume
                if (audio.volume >= musicFullVolume)
                    musicState = MusicState.FullVolume;

                break;
            case MusicState.FadingOut:
                // Subtract from volume, and use Mathf.Max() to make sure we don't go below 0
                audio.volume = Mathf.Max(audio.volume - fadeFactor * Time.deltaTime, 0);

                // Check if volume reached 0 and switch state to mutes
                if (audio.volume <= 0)
                    musicState = MusicState.Muted;

                break;
        }
    }


    public void PlaySound(AudioClip clip, Vector3 position, float volume = 1, float pitch = 1)
    {
        PlaySound(clip, position, null, volume, pitch);
    }

    public void PlaySound(AudioClip clip, Transform parent, float volume = 1, float pitch = 1)
    {
        PlaySound(clip, parent.transform.position, parent, volume, pitch);
    }

    public void PlaySound(AudioClip clip, float volume = 1, float pitch = 1)
    {
        PlaySound(clip,Vector3.zero, null, volume, pitch);
    }

    public void PlaySound(AudioClip clip, Vector3 positon, Transform parent, float volume = 1, float pitch = 1)
    {
        // We need an audio source to play a sound
        AudioSource audioSource = new AudioSource();
        bool didFindAudioSource = false;

        // Loops through all audio sources we've created so far
        foreach (AudioSource source in audioSources)
        {
            // If an existing audio source is not playing any sound, select that one
            if (!source.isPlaying)
            {
                audioSource = source;
                didFindAudioSource = true;
                break;
            }
        }

        // If we didn't find a usable audiosource in the scene, create a new one
        if (!didFindAudioSource)
        {
            // Create audio source
            audioSource = new GameObject("AudioSource").AddComponent<AudioSource>();

            // Add new audio source to our list
            audioSources.Add(audioSource);
        }

        // Set position for audio source (used for 3d sounds)
        audioSource.transform.position = positon;

        // Set audio source parent
        audioSource.transform.parent = parent;

        // Assign the clip to the selected audio source
        audioSource.clip = clip;

        // Set volume
        audioSource.volume = volume;

        // Set pitch
        audioSource.pitch = pitch;

        // Play the clip with the selected audio source
        audioSource.Play();
    }
}