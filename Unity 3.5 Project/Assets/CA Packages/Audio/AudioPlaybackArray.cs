using UnityEngine;
using System.Collections;

public class AudioPlaybackArray : MonoBehaviour {
	
	public AudioClip[] AudioArray;
	
	bool HasStartedPlaying = false;
	
    GameObject SpawnedAudioGameObject = null;
    AudioSource SpawnedAudioObject = null;
	
	// Update is called once per frame
	void Update () 
    {
		if(!HasStartedPlaying)
		{
			if(audio.isPlaying)
			{
				Debug.Log("HasStartedPlaying");
				HasStartedPlaying = true;
			}
			else if(SpawnedAudioObject && SpawnedAudioObject.isPlaying)
				HasStartedPlaying = true;
		}
		
		// Check to see if we are still playing audio
		if(HasStartedPlaying)
		{
			if(SpawnedAudioObject && !SpawnedAudioObject.isPlaying)
			{
        		Destroy(SpawnedAudioGameObject);
        		Destroy(gameObject);
			}
			else if(audio.clip && !audio.isPlaying)
			{
        		Destroy(gameObject);
			}
		}
	}
	
	public void PlayAudio() 
    {
        if (audio.loop && audio.isPlaying)
            return;

        int Index = Random.Range(0, AudioArray.Length);
		audio.clip = AudioArray[Index];
		audio.Play();
	}

    public void PlayAtPoint(Vector3 Position) 
    {
		if(!SpawnedAudioGameObject)
	        SpawnedAudioGameObject = new GameObject("One shot audio");
		
		if(!SpawnedAudioObject)
		{
			SpawnedAudioObject = SpawnedAudioGameObject.GetComponent<AudioSource>();
			if(!SpawnedAudioObject)
        		SpawnedAudioObject = SpawnedAudioGameObject.AddComponent<AudioSource>();
		}
    
		SpawnedAudioGameObject.transform.position = Position;
        int Index = Random.Range(0, AudioArray.Length);
        SpawnedAudioObject.clip = AudioArray[Index];
        SpawnedAudioObject.Play();
    }

    public void StopAudio()
    {
        if (HasStartedPlaying)
        {
            if (SpawnedAudioObject)
            {
                SpawnedAudioGameObject.audio.Stop();
            }
            else if (audio.clip)
            {
                audio.Stop();
            }
        }
    }
}
