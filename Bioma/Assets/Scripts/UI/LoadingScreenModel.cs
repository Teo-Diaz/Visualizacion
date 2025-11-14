using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadingScreenModel : BaseModel
{
    [Header("REFERENCES")]
    [SerializeField] private TMP_Text loadingText;
    [Space]
    [Header("PARAMETERS")]
    [SerializeField] private string loadingBaseText = "Loading";
    [SerializeField, Min(0)] private int pointsAmount = 3;

    private Coroutine _textAnimProcess;

    private Stack<bool> loadingRequest = new Stack<bool>();

    public override void Init()
    {
    }

    public override void Show()
    {
        base.Show();

        if (_textAnimProcess != null)
            StopCoroutine(_textAnimProcess);
        _textAnimProcess = StartCoroutine(AnimateTextProcess());
    }

    public override void Hide()
    {
        base.Hide();

        if (_textAnimProcess != null)
            StopCoroutine(_textAnimProcess);
    }

    public override void Release()
    {
        if (_textAnimProcess != null)
            StopCoroutine(_textAnimProcess);
    }

    public void RequestLoad()
    {
        loadingRequest.Push(true);

        Show();
    }

    public void ReleaseLoad()
    {
        if (loadingRequest.Count == 0) return;

        loadingRequest.Pop();

        if(loadingRequest.Count < 1) Hide();
    }

    private IEnumerator AnimateTextProcess()
    {
        if (!loadingText) yield return null;

        while(true)
        {
            loadingText.text = loadingBaseText;

            for (int i = 0; i < pointsAmount; i++)
            {
                yield return new WaitForSeconds(0.5f);

                loadingText.text += ".";
            }

            yield return new WaitForSeconds(0.5f);
        }
    }
}
