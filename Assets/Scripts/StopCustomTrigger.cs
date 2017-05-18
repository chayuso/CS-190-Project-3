using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopCustomTrigger : AkTriggerBase
{
    public void Hit()
    {
        if (triggerDelegate != null)
        {
            triggerDelegate(null);
        }
    }
}