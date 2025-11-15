using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct InfoDataHelper
{
    [Header("ID")]
    [SerializeField] private string _key;
    [Header("References")]
    [SerializeField] private GameObject _model;
    [SerializeField] private Sprite _image;
    [SerializeField] private string _videoName;
    [Header("Data")]
    [SerializeField] private string _title;
    [SerializeField, TextArea] private string _description;

    public string Key => _key;
    public GameObject Model => _model;
    public Sprite Image => _image;
    public string VideoName => _videoName;
    public string Title => _title;
    public string Description => _description;
}

public struct InfoData
{
    private GameObject _model;
    private Sprite _image;
    private string _videoName;
    private string _title;
    private string _description;

    public GameObject Model => _model;
    public Sprite Image => _image;
    public string VideoName => _videoName;
    public string Title => _title;
    public string Description => _description;

    public InfoData(GameObject model, Sprite image, string videoName, string title, string description)
    {
        _model = model;
        _image = image;
        _videoName = videoName;
        _title = title; 
        _description = description;
    }
}

[CreateAssetMenu(fileName = "New Info Bank", menuName = "InfoBank")]
public class InfoBank : ScriptableObject
{
    [SerializeField] private List<InfoDataHelper> infoList = new List<InfoDataHelper>();

    private Dictionary<string, InfoData> _infoDatas = new Dictionary<string, InfoData>();

    public IReadOnlyDictionary<string, InfoData> InfoDatas => _infoDatas;

    private void OnEnable()
    {
        _infoDatas = new Dictionary<string, InfoData>(infoList.Count);

        foreach (var helper in infoList)
        {
            if (string.IsNullOrWhiteSpace(helper.Key))
            {
                Debug.LogWarning($"InfoBank: Entry must have a key");
                continue;
            }

            if (_infoDatas.ContainsKey(helper.Key))
            {
                Debug.LogWarning($"InfoBank: Key already exists! '{helper.Key}'");
                continue;
            }

            _infoDatas.Add(
                helper.Key,
                new InfoData(helper.Model, helper.Image, helper.VideoName, helper.Title, helper.Description)
            );
        }
    }
}