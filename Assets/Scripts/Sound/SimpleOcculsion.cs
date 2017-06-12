using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine;

public class SimpleOcculsion : MonoBehaviour {
    private Transform listener; //Usually the player or main camera
    private float maxAttenuation;
    private int soundLayerMask;

    void Start ()
    {
        listener = FindObjectOfType<FirstPersonController>().GetComponent<Transform>();
        soundLayerMask = 1 << LayerMask.NameToLayer("Sound"); //Bit shift 1 by layerInt to convert layerInt into mask for only that layer
	}
	
	// Update is called once per frame
	void Update ()
    {
        ApplyObstruction();
	}

    private void ApplyObstruction()
    {
        float distFromPlayer = Vector3.Distance(listener.position, this.transform.position);
        maxAttenuation = AkSoundEngine.GetMaxRadius(this.gameObject);
        Debug.Log(string.Format("Max attenuation: {0}", maxAttenuation));
        if (distFromPlayer <= maxAttenuation)
        {
            //Determine if any objects are in the way of listening to this 
            RaycastHit playerCast;

            Physics.Raycast(this.transform.position, listener.position - this.transform.position, out playerCast, distFromPlayer, soundLayerMask);
            Debug.DrawRay(this.transform.position, listener.position - playerCast.point, Color.red);

            //If a wall is hit:
            if(playerCast.collider != null && playerCast.collider.GetComponent<Wall>() != null)
            {
                Wall wall = playerCast.collider.GetComponent<Wall>();
                Debug.Log(wall);
                switch (wall.obstruction)
                {
                    case (Wall.ObstructionType.Partial):
                        AkSoundEngine.SetObjectObstructionAndOcclusion(this.gameObject, 0, 0.5f, 0.0f);
                        break;
                    case (Wall.ObstructionType.Sealed):
                    default:
                        AkSoundEngine.SetObjectObstructionAndOcclusion(this.gameObject, 0, 0.0f, 0.3f);
                        break;
                }
            }
            else
            {
                AkSoundEngine.SetObjectObstructionAndOcclusion(this.gameObject, 0, 0.0f, 0.0f);
            }
        }
    }
}
