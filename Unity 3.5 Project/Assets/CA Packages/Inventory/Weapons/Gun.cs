using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour 
{
	/***
	* Gun
	***/
	public int BurstCount = 1;
	public float RateOfFireRPS = 1.0f;
	public float FireDelayS = 0.0f;
    public GameObject MuzzleFlashPrefab;
    public float MuzzleFlashDisplayTime = 0.05f;
    public GameObject BrassEjection;
    public Transform BrassEjectionTransform;
	
	private bool Firing = false;
	private float TotalFiringTime = 0.0f;
	private float ElapsedTimeSinceLastShot = 0.0f;
	private int RoundsFire = 0;
    private float CurrentMuzzleFlashDisplayTime = 0.0f;
	private bool HasStartedFiring = false;
	
	/***
	* Audio
	***/
	public AudioPlaybackArray TriggerPulledAudio;
	public AudioPlaybackArray ShotFiredAudio;
	public AudioPlaybackArray StoppedFiringAudio;

    private AudioPlaybackArray TriggerPulledAudioPlaybackAudio;
    private AudioPlaybackArray ShotFiredAudioPlayingAudio;
	
	/***
	* VFX
	***/
	public GameObject TriggerPulledVFX;
	public GameObject ShotFiredVFX;
	public GameObject FiringVFX;
	public GameObject StoppedFiringVFX;
	
	/***
	* Animation
	***/
	public AnimationClip StartFiringAnimation;
	public AnimationClip ShotFiredAnimation;
	public AnimationClip FiringAnimation;
	public AnimationClip StopFiringAnimation;
	
	void Awake() {
        if (MuzzleFlashPrefab && MuzzleFlashPrefab.renderer)
            MuzzleFlashPrefab.renderer.enabled = false;

		ElapsedTimeSinceLastShot = (1.0f / RateOfFireRPS);	
	}
	
	void Update() {
		// Update the muzzle Flash
		if (MuzzleFlashPrefab)
        {
            CurrentMuzzleFlashDisplayTime += Time.deltaTime;
            if (CurrentMuzzleFlashDisplayTime > MuzzleFlashDisplayTime)
            {
                if (MuzzleFlashPrefab.renderer)
                    MuzzleFlashPrefab.renderer.enabled = false;
            }
        }
		
		ElapsedTimeSinceLastShot += Time.deltaTime;
		
        if(!Firing)
        	return;
        
        HealthScript Health = GetComponent<HealthScript>();
        if (Health && !Health.IsAlive())
        	ClientStopFire();
        
        // Check to see if we have a regeneration component, if we do, ensure we can fire
        RegenerationScript Regen = GetComponent<RegenerationScript>();
        if (Regen && !Regen.HasRegenerated())
        {
	        // Stop Firing
            if(Firing)
   		        ClientStopFire();
   		        
            return;
        }
        	
		// Check to see if the rate of fire has expired.
		TotalFiringTime += Time.deltaTime;
		
		// Check to see if the Fire Delay has elapsed.
		if(TotalFiringTime - FireDelayS < 0.0f)
			return;
			
		if(ElapsedTimeSinceLastShot < (1.0f / RateOfFireRPS))
			return;
		
		HideHelperWeapon WeaponHelper = GetComponent<HideHelperWeapon>();
		if(!HasStartedFiring)
		{
			HasStartedFiring = true;
        	if (FiringVFX && FiringVFX.particleEmitter)
            	FiringVFX.particleEmitter.emit = true;
			        
        	if(FiringAnimation && WeaponHelper)
        	{
        		GameObject Weapon = GetComponent<HideHelperWeapon>().GetSpawnedWeapon();
        		if(Weapon && Weapon.animation && !Weapon.animation.GetClip("Firing"))
        		{
					Weapon.animation.AddClip(FiringAnimation, "Firing");
        		}
        
       			Weapon.animation.CrossFade("Firing", 0.6f);
        	}
		}
			
		ElapsedTimeSinceLastShot = 0.0f;
		RoundsFire += BurstCount;
			
		InstantFireScript InstantFire = GetComponent<InstantFireScript>();
		if(InstantFire)
			InstantFire.Fire(BurstCount);
		
		ProjectileFireScript ProjectileFire = GetComponent<ProjectileFireScript>();
		if(ProjectileFire)
			ProjectileFire.Fire();
		
		// Show The Muzzle Flash
        if (MuzzleFlashPrefab)
        {
            // Create a random rotation in the Z
            CurrentMuzzleFlashDisplayTime = 0.0f;
            float RandRotation = UnityEngine.Random.Range(0.0f, 360.0f);
            MuzzleFlashPrefab.transform.Rotate(0.0f, 0.0f, RandRotation);
            
            if (MuzzleFlashPrefab.renderer)
                MuzzleFlashPrefab.renderer.enabled = true;
        }
        
        if(ShotFiredAudio)
        {
			if(!ShotFiredAudioPlayingAudio)
        		ShotFiredAudioPlayingAudio = Instantiate(ShotFiredAudio, transform.position, transform.rotation) as AudioPlaybackArray;
				
			ShotFiredAudioPlayingAudio.PlayAtPoint(transform.position);
        }

        if (TriggerPulledAudioPlaybackAudio)
            TriggerPulledAudioPlaybackAudio.StopAudio();
        
        if(ShotFiredAnimation && WeaponHelper)
        {
        	GameObject Weapon = GetComponent<HideHelperWeapon>().GetSpawnedWeapon();
        		if(Weapon && Weapon.animation && !Weapon.animation.GetClip("Fire"))
        	{
				Weapon.animation.AddClip(ShotFiredAnimation, "Fire");
        	}
        
			Weapon.animation.Stop("Fire");
       		Weapon.animation.Play("Fire");
        }
        
        if(BrassEjection)
        {
        	GameObject go = Instantiate(BrassEjection, BrassEjectionTransform.position, BrassEjectionTransform.rotation) as GameObject;
        	
        	Collider ThisCollider = transform.root.GetComponent<Collider>();
        	Collider goCollider = go.GetComponentInChildren<Collider>();
            if (ThisCollider && goCollider)
            {
                Physics.IgnoreCollision(ThisCollider, goCollider);

                Rigidbody body = go.GetComponentInChildren<Rigidbody>();
                if (body)
                {
                    Vector3 Dir = Vector3.Normalize(transform.position - transform.root.position);
                    Vector3 EjectDir = Vector3.right;
                    if (Vector3.Dot(transform.root.right, Dir) < 0.0f)
                        EjectDir *= -1.0f;
                    EjectDir.y = Random.Range(0.2f, 1.2f);
                    EjectDir = transform.rotation * EjectDir;

                    float RndVal = Random.Range(-1.0f, 1.0f);
                    Vector3 ForcePos = new Vector3(0.0f, 0.0f, RndVal);
                    ForcePos = transform.rotation * ForcePos;

                    body.AddForceAtPosition(EjectDir.normalized, go.transform.position + ForcePos, ForceMode.Impulse);
                }
            }
        }
        	
       	if(ShotFiredVFX)
        	Instantiate(ShotFiredVFX, transform.position, transform.rotation);
        	
        // Tell the Regeneration Script we've been used.
        if (Regen)
            Regen.Use();
	}
	
	public bool IsFiring() {
		return Firing;
	}
	
	public void ClientStartFire() {
        HealthScript Health = GetComponent<HealthScript>();
        if (Health && !Health.IsAlive())
            return;
        
        // Check to see if we have a regeneration component, if we do, ensure we can fire
        RegenerationScript Regen = GetComponent<RegenerationScript>();
        if (Regen && !GetComponent<RegenerationScript>().HasRegenerated())
            return;
        
        if(Firing)
        	return;
        	
        Debug.Log("ClientStartFire");
        networkView.RPC("StartFire", RPCMode.All);
	}
	
	public void ClientStopFire() {
        Debug.Log("ClientStopFire");
        if (networkView)
            networkView.RPC("StopFire", RPCMode.All);
	}
	
	[RPC]
	void StartFire() {
		
        HealthScript Health = GetComponent<HealthScript>();
        if (Health && !Health.IsAlive())
        	return;
        	
        Debug.Log("StartFire");
		Firing = true;
		TotalFiringTime = 0.0f;
		
		InstantFireScript InstantFire = GetComponent<InstantFireScript>();
		if(InstantFire)
			InstantFire.InitFire();
		
		ProjectileFireScript ProjectileFire = GetComponent<ProjectileFireScript>();
		if(ProjectileFire)
			ProjectileFire.InitFire();
		
		if(TriggerPulledAudio)
        {
            TriggerPulledAudioPlaybackAudio = Instantiate(TriggerPulledAudio, transform.position, transform.rotation) as AudioPlaybackArray;
            TriggerPulledAudioPlaybackAudio.PlayAudio();
        }
			
		if(TriggerPulledVFX)
        	Instantiate(TriggerPulledVFX, transform.position, transform.rotation);
		
		HideHelperWeapon WeaponHelper = GetComponent<HideHelperWeapon>();       
		if(StartFiringAnimation && WeaponHelper)
        {
        	GameObject Weapon = GetComponent<HideHelperWeapon>().GetSpawnedWeapon();
        		if(Weapon && Weapon.animation && !Weapon.animation.GetClip("StartFiring"))
        	{
				Weapon.animation.AddClip(StartFiringAnimation, "StartFiring");
        	}
			
			if(Weapon.animation.IsPlaying("StopFiring"))
			{
				Weapon.animation.Blend("StopFiring", 0.0f, 0.8f);
				Weapon.animation.Blend("StartFiring", 1.0f, 0.8f);
			}
			else
	       		Weapon.animation.Play("StartFiring");
        }
	}
	
	[RPC]
	void StopFire() {
        Debug.Log("StopFire");
		Firing = false;
		TotalFiringTime = 0.0f;
		HasStartedFiring = false;
		
		if(StoppedFiringAudio)
        {
        	AudioPlaybackArray go = Instantiate(StoppedFiringAudio, transform.position, transform.rotation) as AudioPlaybackArray;
        	go.PlayAudio();
        }

        if (ShotFiredAudioPlayingAudio && ShotFiredAudioPlayingAudio.audio && ShotFiredAudioPlayingAudio.audio.loop)
            ShotFiredAudioPlayingAudio.StopAudio();
			
        if (FiringVFX && FiringVFX.particleEmitter)
            FiringVFX.particleEmitter.emit = false;
		
		HideHelperWeapon WeaponHelper = GetComponent<HideHelperWeapon>();
        if(FiringAnimation && WeaponHelper)
        {
        	GameObject Weapon = GetComponent<HideHelperWeapon>().GetSpawnedWeapon();
        
			if(Weapon)
			{
				Weapon.animation.Stop("StartFiring");
				Weapon.animation.Stop("Firing");
				
				if(StopFiringAnimation)
				{
        			if(Weapon.animation && !Weapon.animation.GetClip("StopFiring"))
        			{
						Weapon.animation.AddClip(StopFiringAnimation, "StopFiring");
        			}
				}
				
				if(Weapon.animation.IsPlaying("StartFiring"))
				{
					Weapon.animation.Blend("StartFiring", 0.0f, 0.8f);
					Weapon.animation.Blend("StartFiring", 1.0f, 0.8f);
				}
				else
	       			Weapon.animation.Play("StopFiring");
			}
        }
			
		if(StoppedFiringVFX)
        {
        	Vector3 Position = transform.position + (transform.rotation * StoppedFiringVFX.transform.position);
        	Quaternion Rotation = transform.rotation * StoppedFiringVFX.transform.rotation;
        	GameObject go = Instantiate(StoppedFiringVFX, Position, Rotation) as GameObject;

            if (go)
                go.transform.parent = transform;
        }
	}
}
