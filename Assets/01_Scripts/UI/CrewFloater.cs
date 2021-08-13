using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CrewFloater : MonoBehaviour
{
    [SerializeField] private GameObject _crew_Prefab;

    [SerializeField] private List<Sprite> _sprites;

    private bool[] _crewStates = new bool[12];

    private float _timer = 0.5f;

    private float _distance = 11.0f;

    private void Start()
    {
        for (int i = 0; i < _sprites.Count * 2; i++)
        {
            SpawnFloatingCrew((EPlayerColor) i, Random.Range(0, _distance));
        }
    }

    void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            SpawnFloatingCrew((EPlayerColor) Random.Range(0, 12), _distance);
            _timer = 1.0f;
        }
    }

    public void SpawnFloatingCrew(EPlayerColor playerColor, float dist)
    {
        if (!_crewStates[(int) playerColor])
        {
            _crewStates[(int) playerColor] = true;
            
            float angle = Random.Range(0.0f, 360.0f);

            Vector3 spawnPos = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0f) * _distance;
            Vector3 direction = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0.0f);
            float floatingSpeed = Random.Range(1.0f, 4.0f);
            float rotateSpeed = Random.Range(-3.0f, 3.0f);
            
            var crew = Instantiate(_crew_Prefab, spawnPos, Quaternion.identity).GetComponent<FloatingCrew>();
            crew.SetFloatingCrew(_sprites[Random.Range(0, _sprites.Count)], playerColor, direction, floatingSpeed,
                rotateSpeed, Random.Range(0.5f, 1.0f));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var crew = other.GetComponent<FloatingCrew>();
        if (crew != null)
        {
            _crewStates[(int) crew.GetCrewColor()] = false;
            Destroy(crew.gameObject);
        }
    }
}
