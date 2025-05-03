using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;
public class Friend : MonoBehaviour
{
    private Transform friend;
    public int _health;
    [SerializeField] TextMeshPro healthText;
    void Start()
    {
        _health = Random.Range(1, 10);
        //healthText.text = _health.ToString();
        //friend.localScale = Vector3.one + (Vector3.one * _health) / 10 /*+ friend.localScale*/;
    }
    void Update()
    {
    }
}