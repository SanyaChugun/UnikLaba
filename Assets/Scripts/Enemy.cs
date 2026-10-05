using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;
using System;

public class Enemy : MonoBehaviour
{
    public int _health_enemy;
    [SerializeField] private TextMeshProUGUI enemyhealthText;
    private GameObject[] friends;
    private GameObject player;
    private GameObject[] patrolpoints;
    private int _currentPatrolIndex = 0;
    [SerializeField] private PlayerController PlayerController;
    private GameObject closestFriend;
    public GameObject nearest;
    [SerializeField] private float _enemyspeed;

    private void Start()
    {
        _health_enemy = Random.Range(1, 50);
        enemyhealthText.text = _health_enemy.ToString();
        friends = GameObject.FindGameObjectsWithTag("Friend");
        patrolpoints = GameObject.FindGameObjectsWithTag("PatrolPoint1");
        _enemyspeed = 20;
        transform.localScale = (Vector3.one * (_health_enemy / Random.Range(1, 10)) + transform.localScale + (Vector3.one * _health_enemy) / 20);
    }

    void Patrol()
    {
        if (patrolpoints == null || patrolpoints.Length == 0) return;
        Vector3 targetPoint = patrolpoints[_currentPatrolIndex].transform.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPoint, _enemyspeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPoint) < 0.1f)
        {
            _currentPatrolIndex = (_currentPatrolIndex + 1) % patrolpoints.Length;
        }
    }

    void Update()
    {
        bool isHide = PlayerController.isHide;
        if (isHide == true)
        {
            try
            {
                friends = GameObject.FindGameObjectsWithTag("Friend");
                nearest = FindClosestFriend()?.gameObject;
                if (nearest != null)
                {
                    transform.position = Vector3.MoveTowards(transform.position, nearest.transform.position, _enemyspeed * Time.deltaTime);
                }
                else
                {
                    Patrol();
                }
            }
            catch (System.InvalidOperationException)
            {
                Patrol();
            }
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("Player");
            int playerHealth = PlayerController.health;
            if (_health_enemy > 20)
            {
                transform.localScale = Vector3.one * _health_enemy / 2;
            }
            if (_health_enemy > playerHealth)
            {
                if (player != null)
                {
                    transform.position = Vector3.MoveTowards(transform.position, player.transform.position, _enemyspeed * Time.deltaTime);
                }
            }
            else
            {
                try
                {
                    friends = GameObject.FindGameObjectsWithTag("Friend");
                    nearest = FindClosestFriend()?.gameObject;

                    if (nearest != null)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, nearest.transform.position, _enemyspeed * Time.deltaTime);
                    }
                    else
                    {
                        Patrol();
                    }
                }
                catch (System.InvalidOperationException)
                {
                    Patrol();
                }
            }
        }
    }

    GameObject FindClosestFriend()
    {
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        closestFriend = null;

        foreach (GameObject go in friends)
        {
            if (go == null) continue;

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
            enemyhealthText.text = _health_enemy.ToString();
            Destroy(collision.gameObject);
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