using System;
using UnityEngine;
using System.Collections.Generic;

public class FunctionTimer : MonoBehaviour
{
    public static FunctionTimer current;
    void Awake()
    {
        current = this;
    }

    List<Action> actions = new List<Action>();
    List<float> timers = new List<float>();
    List<string> names = new List<string>();

    public void AddTimer(Action action, float timer, string name = "None")
    {
        this.actions.Add(action);
        this.timers.Add(timer);
        this.names.Add(name);
    }

    void Update()
    {
        if (timers.Count > 0){
            for (int i = 0; i < timers.Count; i++){
                timers[i] -= Time.deltaTime;
                
                if (timers[i] <= 0){
                    actions[i]();
                    actions.RemoveAt(i);
                    timers.RemoveAt(i);
                    names.RemoveAt(i);
                }
            }
        }
    }

    public void removeTimer(string name)
    {
        for (int i = 0; i < names.Count; i++){
            if (names[i] == name){
                actions.RemoveAt(i);
                timers.RemoveAt(i);
                names.RemoveAt(i);
            }
        }
    }
}