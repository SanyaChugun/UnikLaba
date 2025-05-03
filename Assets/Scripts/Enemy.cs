using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;
using System;
public class Enemy : MonoBehaviour
{
    public int _health_enemy;
    [SerializeField] TextMeshPro enemyhealthText;
    private GameObject[] friends;
    private GameObject player;
    [SerializeField] private PlayerController PlayerController;
    private GameObject closestFriend;
    public GameObject nearest;
    [SerializeField] private float _enemyspeed;
    private void Start()
    {
        _health_enemy = Random.Range(1, 50);
        //enemyhealthText.text = _health_enemy.ToString();
        friends = GameObject.FindGameObjectsWithTag("Friend");
        _enemyspeed = 20;
    }
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        int playerHealth = PlayerController.health;
        if (_health_enemy > playerHealth)
        {
            transform.position = Vector3.MoveTowards(transform.position,
            player.transform.position, _enemyspeed * Time.deltaTime);
        }
        else
        {
            friends = GameObject.FindGameObjectsWithTag("Friend");
            nearest = FindClosestFriend().gameObject;
            transform.position = Vector3.MoveTowards(transform.position,
            nearest.transform.position, _enemyspeed * Time.deltaTime);
        }
    }
    GameObject FindClosestFriend()
    {
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in friends)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closestFriend = go;
                distance = curDistance;
            }
        }
        return closestFriend;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Friend")
        {
            Friend friend = collision.gameObject.GetComponent<Friend>();
            int healthFriend = friend._health;
            _health_enemy += healthFriend;
            //enemyhealthText.text = _health_enemy.ToString();
            Destroy(collision.gameObject);
            transform.localScale = Vector3.one + (Vector3.one * healthFriend) / 20 + transform.localScale;
        }
    }
    public bool isAlive()
    {
        return _health_enemy > 0;
    }
    public void Hit(int damage)
    {
        _health_enemy -= damage;
    }
}