using UnityEngine;
using System.Collections;

public class Audio : MonoBehaviour {

	public AudioSource source;
	public AudioClip punch,death,walk,teleport,ding,book;
	public bool running = true;

	void Start()
	{
		source = GetComponent<AudioSource> ();
	}

	public void IsRunning()
	{
		if(running == true)
		{
			source.clip = walk;
			source.Play ();
			running = false;
		}

	}

	public void stopRunning()
	{
		source.Stop ();
		source.clip = null;
		running = true;
	}

	public void PlayDeath()
	{ 
		source.PlayOneShot (death);
	}
	
	public void PlayPunch()
	{ 
		source.PlayOneShot (punch);
	}

	public void PlayTeleport()
	{
		source.PlayOneShot (teleport);
	}

	public void PlayWalking()
	{
		source.PlayOneShot (walk);
	}

	public void PlayDing()
	{
		source.PlayOneShot (ding);
	}

	public void PlayClosingBook()
	{
		source.PlayOneShot (book);
	}
}
