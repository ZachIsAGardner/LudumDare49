using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float ThrustPower = 80;
    public float Power = 50;
    public float RotationPower = 50;
    public float MaxThrust = 2;
    public float ThrustRegen = 2;
    public float Thrust { get; private set; }
    public Vector3? Checkpoint { get; set; }
    public float MaxSpeed = 8;
    public LayerMask CollisionMask;

    public bool Paused { get; private set; } = false;

    private float thrustRegenerateCooldownDuration = 10f;
    private float thrustRegenerateCooldown = 0;

    private List<ParticleSystem> effects;

    private Vector3 startPosition;
    Rigidbody rigidbody;

    public void Pause()
    {
        rigidbody.velocity = Vector3.zero;
        Paused = true;
    }

    public void UnPause()
    {
        Paused = false;
    }

    void Start()
    {
        startPosition = transform.position;
        rigidbody = GetComponent<Rigidbody>();    
        effects = GetComponentsInChildren<ParticleSystem>().ToList();
        effects.ForEach(e => e.Stop());
    }

    void Update()
    {       
        bool hit = Physics.Raycast(transform.position, Vector3.down, 2f, CollisionMask);
        if (hit)
        {
            thrustRegenerateCooldown = 0;
        }
        rigidbody.velocity = new Vector3(
            Mathf.Sign(rigidbody.velocity.x) * Mathf.Min(Mathf.Abs(rigidbody.velocity.x), MaxSpeed),
            Mathf.Sign(rigidbody.velocity.y) * Mathf.Min(Mathf.Abs(rigidbody.velocity.y), MaxSpeed),
            Mathf.Sign(rigidbody.velocity.z) * Mathf.Min(Mathf.Abs(rigidbody.velocity.z), MaxSpeed)
        );

        if (Paused) 
        {
            rigidbody.velocity = Vector3.zero;
            return;
        }

        if (transform.position.y < -5)
        {
            Restart();
        }

        if (Input.GetKey(KeyCode.Space) && Thrust > 0)
        {
            if (rigidbody.velocity.y < 0)
            {
                rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);
            }
            rigidbody.AddForce(new Vector3(0, ThrustPower * Time.deltaTime, 0));
            Thrust -= Time.deltaTime;
            thrustRegenerateCooldown = thrustRegenerateCooldownDuration;
            effects.ForEach(e => e.Play());
        }
        else if (thrustRegenerateCooldown <= 0)
        {
            effects.ForEach(e => e.Stop());
            Thrust += ThrustRegen * Time.deltaTime * ((hit) ? 4 : 1);
            if (Thrust >= MaxThrust) Thrust = MaxThrust;
        }
        else
        {
            effects.ForEach(e => e.Stop());

            thrustRegenerateCooldown -= Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            rigidbody.AddForce(new Vector3(0, 0, Power * Time.deltaTime));
            rigidbody.AddTorque(new Vector3(1, 0, 0) * RotationPower * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rigidbody.AddForce(new Vector3(-Power * Time.deltaTime, 0, 0));
            rigidbody.AddTorque(new Vector3(0, 0, 1) * RotationPower * Time.deltaTime);
        }

         if (Input.GetKey(KeyCode.DownArrow))
        {
            rigidbody.AddForce(new Vector3(0, 0, -Power * Time.deltaTime));
            rigidbody.AddTorque(new Vector3(-1, 0, 0) * RotationPower * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rigidbody.AddForce(new Vector3(Power * Time.deltaTime, 0, 0));
            rigidbody.AddTorque(new Vector3(0, 0, -1) * RotationPower * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Lava")
        {
            Restart();
        }
    }

    void Restart()
    {
        transform.position = Checkpoint ?? startPosition;
        rigidbody.velocity = Vector3.zero;
    }
}
