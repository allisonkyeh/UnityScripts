using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharController : MonoBehaviour
{
	public float speed = 6f;			//The speed that the player will move.
	public float turnSpeed = 60f;		
	public float turnSmoothing = 15f;

	private Vector3 movement;
	private Vector3 turning;
	private Animator anim;
	private Rigidbody rb;

	void Awake()
	{
		// Get references
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
		// Store input axes
		float x = Input.GetAxisRaw("Horizontal");
		float y = Input.GetAxisRaw ("Vertical");

		Move (x, y);
		Animating(x, y);
	}

	void Move(float x, float y)
	{
		// Move the player
		movement.Set (x, 0f, y);
		// Makes it so Player moves in relation to where the camera is pointing
		movement = Camera.main.transform.TransformDirection(movement);
		movement.y = 0.0f;

    // .normalized takes away the speed variation
    movement = movement * speed * Time.deltaTime;
    rb.MovePosition (transform.position + movement);
		rb.AddForce (movement * speed * movement.magnitude);

		if(x != 0f || y != 0f) { Rotating(x, y); }
	}

	void Rotating(float x, float y)
	{
		Vector3 targetDirection = new Vector3(x, 0f, y);
		// Makes it so Player moves in relation to where the camera is pointing
		targetDirection = Camera.main.transform.TransformDirection(targetDirection.normalized);
		targetDirection.y = 0.0f;

		Quaternion targetRotation = Quaternion.LookRotation (targetDirection, Vector3.up);
		Quaternion newRotation = Quaternion.Lerp (GetComponent<Rigidbody>().rotation, targetRotation, turnSmoothing * Time.deltaTime);

		GetComponent<Rigidbody>().MoveRotation(newRotation);
	}

	void Animating(float x, float y)
	{
		Vector3 vel = new Vector3(x, 0f, y);
		anim.SetFloat ("Speed", vel.magnitude);
	}
}
