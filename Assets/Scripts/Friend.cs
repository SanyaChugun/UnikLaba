using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;
public class Friend : MonoBehaviour
{
    public int _health;
    [SerializeField] private TextMeshProUGUI healthText;
    void Start()
    {
        _health = Random.Range(1, 10);
        healthText.text = _health.ToString();
        transform.localScale = Vector3.one + ((Vector3.one * _health)) /*+ friend.localScale*/;
    }
    void Update()
    {
    }
}