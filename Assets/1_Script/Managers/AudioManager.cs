using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum ClipType
{
    Fire,
    DoorHit,
    NegGateEnter,
    PosGateEnter,
    Chain,
    Wood,
    Brick
};

[System.Serializable]
public class ClipProperty
{
    public ClipType clipType;
    public float volume = 1f;
    public float minPitch = 0.85f, maxPitch = 1.1f;
    public List<AudioClip> clips = new List<AudioClip>();
}

[RequireComponent(typeof(AudioSource))]
public class AudioManager : Singleton<AudioManager>
{
    private AudioSource audioSource;
    [SerializeField] List<ClipProperty> clipPropertiesList = new List<ClipProperty>();
    private Dictionary<ClipType, ClipProperty> clipPropertiesDic = new Dictionary<ClipType, ClipProperty>();
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        // Performans için liste değil Dictionary kullanıyoruz. Editorde göstermek için de liste tutuyoruz
        foreach (ClipProperty clipProperty in clipPropertiesList)
        {
            clipPropertiesDic.Add(clipProperty.clipType, clipProperty);
        }
    }
    public void PlaySound(ClipType clipType, bool randomPlay = false, bool randomPitch = false)
    {
        AudioClip audioClip = null;
        if (clipPropertiesDic.TryGetValue(clipType, out var clipProperty))
        {
            if (randomPlay)
            {
                var clipCount = clipProperty.clips.Count - 1;
                audioClip = clipProperty.clips[Random.Range(0, clipCount)];
            }
            else
            {
                audioClip = clipProperty.clips.FirstOrDefault();
            }

            if (audioClip != null)
            {
                Debug.Log("AudioClip listesi boş");
                return;
            }
        }
        else
        {
            Debug.Log("ClipPropertiesList listesi editörde boş");
            return;
        }

        if (randomPitch)
        {
            audioSource.pitch = Random.Range(clipProperty.minPitch, clipProperty.maxPitch);
        }
        else
        {
            audioSource.pitch = 1f;
        }

        audioSource.PlayOneShot(audioClip, clipProperty.volume);
    }
}
