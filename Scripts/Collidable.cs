using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidable : MonoBehaviour
{
    // Some variables for the objects
    public ContactFilter2D filter;
    private BoxCollider2D boxCollider;
    private Collider2D[] hits = new Collider2D[10];

    // Catching absent boxcollider for every object via error
    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void Update()
    {
        // Collision work
        boxCollider.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;

            OnCollide(hits[i]);

            // Cleaning up the array
            hits[i] = null;
        }
    }

    // Virtual = overwritable
    protected virtual void OnCollide(Collider2D coll)
    {
        Debug.Log("OnCollide was not changed in " + this.name);
    }
}
