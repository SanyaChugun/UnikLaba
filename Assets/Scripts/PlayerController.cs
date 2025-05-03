using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEngine.Rendering.DebugUI;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI forceText;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private float _moveSpeed;
    public int health = 0;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Friend")
        {
// Friend friend = collision.gameObject.GetComponent<Friend>();
 //          int healthFriend = friend._health;
   //         health += healthFriend;
            forceText.text = health.ToString();
            Destroy(collision.gameObject);
        }
    }
    private void FixedUpdate()
    {
        _rigidbody.linearVelocity = new Vector3(_joystick.Horizontal * _moveSpeed, _rigidbody.linearVelocity.y, _joystick.Vertical * _moveSpeed);
        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            Console.WriteLine("check");
            transform.rotation = Quaternion.LookRotation(_rigidbody.linearVelocity);
        }
    }
}
