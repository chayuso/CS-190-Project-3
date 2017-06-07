using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {
    public enum ObstructionType
    {
        Sealed,
        Partial
    }

    public ObstructionType obstruction = ObstructionType.Sealed;

    void Start()
    {
        this.gameObject.layer = LayerMask.NameToLayer("Sound");
    }
}
