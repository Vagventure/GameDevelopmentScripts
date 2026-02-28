using System;
using UnityEngine;

public interface IProgressBar 
{
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressNormaliazed;
    }
}
