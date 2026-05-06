using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEffectParticleManager : MonoBehaviour
{
    [SerializeField] float rotationSpeedRandom = 2.5f;
    [SerializeField] float extraVertflingSpeed = 10;
    [SerializeField] float flingSpeedRandom = 1;
    [SerializeField] float lifetime = 1;
    [SerializeField] float lifetimeRandom = 0.5f;
    [SerializeField] GameObject[] particles;
    void Start()
    {
        for (int i = 0; i < particles.Length; i++)
        {
            Rigidbody2D particleRB = particles[i].GetComponent<Rigidbody2D>();
            particleRB.AddTorque(Random.Range(-rotationSpeedRandom, rotationSpeedRandom) * 5);
            particleRB.AddForce(new Vector2 (Random.Range(-flingSpeedRandom, flingSpeedRandom), Random.Range(-flingSpeedRandom, flingSpeedRandom) + extraVertflingSpeed));
            Destroy(particles[i], lifetime + Random.Range(-lifetimeRandom, lifetimeRandom));
        }
    }
}