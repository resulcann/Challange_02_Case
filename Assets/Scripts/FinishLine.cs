using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private bool _isTriggered;

    public bool GetIsTriggered() => _isTriggered;
    public void SetIsTriggered(bool value) => _isTriggered = value;
}
