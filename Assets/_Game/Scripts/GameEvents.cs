﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class GameEvents 
{
    public static UnityEvent missionComplateEvent = new UnityEvent();
    public static UnityAction endGameEvent;
}
