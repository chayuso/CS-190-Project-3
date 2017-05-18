using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOffTrigger : AkTriggerBase
{
    public void Hit()
    {
        if (triggerDelegate != null)
        {
            triggerDelegate(null);
        }
    }
}
