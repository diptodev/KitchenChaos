using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProgressBarUI
{
   
    public event EventHandler<OnIProgressBarUIEventArgs> OnIProgressBarUI;
    public class OnIProgressBarUIEventArgs : EventArgs
    {
        public float normalizedProgressBarValue;
        public string modeColor;
    }
}
