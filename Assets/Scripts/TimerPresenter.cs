using R3;
using TMPro;
using UnityEngine;

public sealed class TimerPresenter : MonoBehaviour
{
    [SerializeField] private InGameManager _inGameManager;
    [SerializeField] private TMP_Text _text;

    private void Start()
    {
        _inGameManager.CurrentTime.Subscribe(OnUpdate)
            .RegisterTo(destroyCancellationToken);
    }

    public void OnUpdate(float time)
    {
        _text.SetText(time.ToString("F1"));
    }
}
