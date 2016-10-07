using UnityEngine;

public class BombExplosion : MonoBehaviour
{
	public LayerMask m_TankMask;
	public ParticleSystem m_BombParticles;       
	public AudioSource m_BombAudio;              
	public float m_MaxDamage = 100f;                  
	public float m_ExplosionForce = 1000f;            
	public float m_ExplosionRadius = 5f;  
	public float damage;


	private void Start()
	{
	}


	private void OnTriggerEnter(Collider other)
	{
		// Find all the tanks in an area around the shell and damage them.

		Collider[] colliders = Physics.OverlapSphere (transform.position, m_ExplosionRadius, m_TankMask);

		for (int i = 0; i < colliders.Length; i++) 
		{
			Rigidbody targetRigidbody = colliders [i].GetComponent<Rigidbody> ();

			if (!targetRigidbody) 
			{
				continue;
			}

			targetRigidbody.AddExplosionForce (m_ExplosionForce, transform.position, m_ExplosionRadius);

			TankHealth targetHealth = targetRigidbody.GetComponent<TankHealth> ();

			if (!targetHealth)
				continue;

			targetHealth.TakeDamage (damage);
		}

		m_BombParticles.transform.parent = null;

		m_BombParticles.Play ();

		m_BombAudio.Play ();

		Destroy (m_BombParticles.gameObject, m_BombParticles.duration);

		Destroy (gameObject);
	}
}
