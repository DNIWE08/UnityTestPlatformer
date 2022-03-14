using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static Action onJump;

    public static void PlayerJump()
    {
        onJump?.Invoke();
    }
}
