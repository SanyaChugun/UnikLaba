using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpawnFriends : MonoBehaviour
{
    [SerializeField] private GameObject _friendPrefab;
    [SerializeField] private GameObject LosePanel;
    [SerializeField] private GameObject WinPanel;

    void Start()
    {
        WinPanel.SetActive(false);
        LosePanel.SetActive(false);
        Time.timeScale = 1;
        for (int i = 0; i < 50; i++)
        {
            Instantiate(_friendPrefab, new Vector3(Random.Range(-500, 500), -221 ,Random.Range(-500,500)), Quaternion.identity);        }
    }
}                       