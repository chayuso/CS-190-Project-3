using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingTrigger : AkTriggerBase
{
    public void Hit()
    {
        if (triggerDelegate != null)
        {
            triggerDelegate(null);
        }
    }
}
