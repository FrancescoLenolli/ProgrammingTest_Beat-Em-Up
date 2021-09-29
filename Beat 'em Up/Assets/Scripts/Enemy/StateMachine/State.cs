﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    private EnemyControl owner;
    private StateMachine stateMachine;

    public EnemyControl Owner { get => owner; }
    public StateMachine StateMachine { get => stateMachine; }

    public void SetUp(EnemyControl owner, StateMachine stateMachine)
    {
        this.owner = owner;
        this.stateMachine = stateMachine;
    }

    public abstract void UpdateState();
}