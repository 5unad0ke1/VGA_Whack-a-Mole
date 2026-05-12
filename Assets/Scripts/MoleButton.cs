using System;
using UnityEngine;

public sealed class MoleButton : MonoBehaviour
{
    public event Action OnClicked;
    private void OnMouseDown()
    {
        OnClicked?.Invoke();
        Debug.Log("Clicked!", gameObject);
    }
}
