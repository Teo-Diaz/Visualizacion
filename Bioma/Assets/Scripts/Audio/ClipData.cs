using UnityEngine;

public enum AudioType
{
    Music,
    SFX,
    Ambient
}

[System.Serializable]
public class ClipData
{
    public string code;
    public AudioType type;
    public AudioClip clip;
}