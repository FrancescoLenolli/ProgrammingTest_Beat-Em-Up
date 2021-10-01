using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField]
    private State startingState;

    private State currentState;
    private List<State> states;
    private EnemyControl owner;

    private void Start()
    {
        InitStates();
    }

    private void Update()
    {
        currentState?.UpdateState();
    }

    public void SwitchState(Type newStateType)
    {
        State newState = states.Where(state => state.GetType() == newStateType).First();
        if (newState)
            currentState = newState;
        else
            Debug.Log($"Cannot find State of type {newStateType.Name} in {gameObject.name}.");
    }

    public void InitStates()
    {
        owner = GetComponent<EnemyControl>();
        states = new List<State>(owner.GetComponents<State>());

        foreach (State state in states)
        {
            state.SetUp(owner, this);
        }

        currentState = startingState;
    }

    public void Stop()
    {
        currentState = null;
    }
}
