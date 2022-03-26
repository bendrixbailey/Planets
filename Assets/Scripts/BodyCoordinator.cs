using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyCoordinator : MonoBehaviour
{
    GravityBody[] bodies;
    static BodyCoordinator instance;

    void Awake () {

        bodies = FindObjectsOfType<GravityBody> ();
        Time.fixedDeltaTime = UniverseConstants.timeStep;
        Debug.Log ("Setting fixedDeltaTime to: " + UniverseConstants.timeStep);
    }

    void FixedUpdate () {
        for (int i = 0; i < bodies.Length; i++) {
            Vector3 acceleration = CalculateAcceleration (bodies[i].Position, bodies[i]);
            bodies[i].UpdateVelocity (acceleration, UniverseConstants.timeStep);
            //bodies[i].UpdateVelocity (bodies, Universe.physicsTimeStep);
        }

        for (int i = 0; i < bodies.Length; i++) {
            bodies[i].UpdatePosition (UniverseConstants.timeStep);
        }

    }

    public static Vector3 CalculateAcceleration (Vector3 point, GravityBody ignoreBody = null) {
        Vector3 acceleration = Vector3.zero;
        foreach (var body in Instance.bodies) {
            if (body != ignoreBody) {
                float sqrDst = (body.Position - point).sqrMagnitude;
                Vector3 forceDir = (body.Position - point).normalized;
                acceleration += forceDir * UniverseConstants.gravity * body.mass / sqrDst;
            }
        }

        return acceleration;
    }

    public static GravityBody[] Bodies {
        get {
            return Instance.bodies;
        }
    }

    static BodyCoordinator Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<BodyCoordinator> ();
            }
            return instance;
        }
    }
}
