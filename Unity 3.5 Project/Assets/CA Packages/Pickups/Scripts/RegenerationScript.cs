using UnityEngine;
using System.Collections;

public class RegenerationScript : MonoBehaviour
{
    public float RegenLossPerUse = 1.0f;
    public float IdleRegeneration = 0.0f;
    public float RegenWhenDepleted = 0.1f;
    public float RegenerationWarning = 0.7f;

    public float ElapsedCooldownTime = 1.0f;

    public bool HidesObjectWhenDepleted = true;
    public bool Depleted = false;
    
    public float GetElapsedCooldown()
    {
    	return ElapsedCooldownTime;
    }
    
    public float GetRegenerationWarning()
    {
    	return RegenerationWarning;
    }

    public bool HasRegenerated()
    {
        return !Depleted;
    }
	
	// Update is called once per frame
	void Update () {
        if (Depleted)
        {
            ElapsedCooldownTime += RegenWhenDepleted * Time.deltaTime;
        }
        else
        {
            ElapsedCooldownTime += IdleRegeneration * Time.deltaTime;
        }

        if (ElapsedCooldownTime > 1.0f)
        {
            ElapsedCooldownTime = 1.0f;

            if (Network.isServer && networkView)
                networkView.RPC("ClientRestored", RPCMode.All);
        }
	}

    public void Use() {
        if (Depleted)
            return;

        ElapsedCooldownTime -= RegenLossPerUse;
        if (ElapsedCooldownTime <= 0.0f)
        {
            ElapsedCooldownTime = 0.0f;

            if (Network.isServer && networkView)
                networkView.RPC("ClientDepleted", RPCMode.All);
        }
    }

    void HideObject(bool Hide)
    {
    	if(!HidesObjectWhenDepleted)
    		return;
    	
        Light[] Lights = GetComponentsInChildren<Light>();
        for (int n = 0; n < Lights.Length; n++)
            Lights[n].enabled = !Hide;

        Renderer[] Vis = GetComponentsInChildren<Renderer>();
        for (int n = 0; n < Vis.Length; n++)
            Vis[n].enabled = !Hide;
    }

    [RPC]
    void ClientDepleted()
    {
        Depleted = true;
        HideObject(true);
    }

    [RPC]
    void ClientRestored()
    {
        Depleted = false;
        HideObject(false);
    }
}
