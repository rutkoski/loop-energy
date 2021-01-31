using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TaskController : Singleton<TaskController>
{

    private class TaskToRun
    {
        public int FramesSkip { get; set; }
        public Action Action { get; set; }
    }
    
    private object lockObject = new object();

    private List<TaskToRun> lstAct = new List<TaskToRun>();

    public void RunOnMainThread(Action action, int FramesSkip = 0)
    {
        lock (this.lockObject)
        {
            this.lstAct.Add(new TaskToRun() { FramesSkip = FramesSkip, Action = action });
        }
    }

    public void RunOnBackground(Action action)
    {
        new Thread(new ThreadStart(action)).Start();
    }

    private void Update()
    {
        this.RunActions();
    }

    private void RunActions()
    {
        lock (this.lockObject)
        {
            if (this.lstAct.Count == 0) return;

            for (int x = 0; x < this.lstAct.Count; x++)
            {
                try
                {
                    if (this.lstAct[x].FramesSkip <= 0)
                    {
                        this.lstAct[x].Action.Invoke();
                        this.lstAct.Remove(this.lstAct[x]);
                        print("RunActions " + this.lstAct.Count);
                        x--;
                    }
                    else
                    {
                        this.lstAct[x].FramesSkip--;
                    }
                }
                catch
                {
                }
            }
        }
    }
}
