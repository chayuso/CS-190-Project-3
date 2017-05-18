using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoneChargingTrigger : AkTriggerBase
{
    public void Hit()
    {
        if (triggerDelegate != null)
        {
            triggerDelegate(null);
        }
    }
}
