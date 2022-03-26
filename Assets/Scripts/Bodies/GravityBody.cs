using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *
 */

/*
 * This class is the main
 */

[ExecuteInEditMode]
[RequireComponent(typeof(Rigidbody))]
public class GravityBody : MonoBehaviour
{
    public float mass {get; private set;}
    public float radius;
    public float surfaceGravity;
    public Vector3 initialVelocity;
    public string displayName;
    Vector3 velocity;

    Rigidbody rb;

    
    void Awake(){
        rb = GetComponent<Rigidbody>();
        
        velocity = initialVelocity;
        RecalculateMass ();
    }

    public void UpdateOrbits (GravityBody[] allBodies, float timeStep){
        foreach(GravityBody body in allBodies){
            if(body != this){
                float dist = (body.rb.position - rb.position).sqrMagnitude;
                Vector3 force = (body.rb.position - rb.position).normalized;
                Vector3 acceleration = force * UniverseConstants.gravity * body.mass / dist;
                velocity += acceleration * timeStep;
            }
        }
    }

    public void UpdateVelocity (Vector3 acceleration, float timeStep) {
        velocity += acceleration * timeStep;
    }

    public void UpdatePosition (float timeStep) {
        rb.MovePosition (rb.position + velocity * timeStep);

    }


    public string getName(){
        return displayName;
    }

    public void RecalculateMass () {
        mass = surfaceGravity * radius * radius / UniverseConstants.gravity;
        Rigidbody.mass = mass;
    }

    void OnValidate () {
        RecalculateMass ();
        if (GetComponentInChildren<BodySurfaceHandler> ()) {
            GetComponentInChildren<BodySurfaceHandler> ().transform.localScale = Vector3.one * radius;
        }
        gameObject.name = name;
    }

    public Vector3 Position{
        get{return rb.position;}
    }

    public Rigidbody Rigidbody {
        get {
            if (!rb) {
                rb = GetComponent<Rigidbody> ();
            }
            return rb;}
    }

}
