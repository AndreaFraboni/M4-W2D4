using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiator3D : MonoBehaviour
{

    [SerializeField] private GameObject quadPrefab;

    [SerializeField] private int _col, _row;

    [SerializeField] private float Offset;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;

        for (int i = 0; i < _col; i++)
        {
            for (int j = 0; j < _row; j++)
            {
                Vector3 position = startPosition + new Vector3(i * Offset, j * Offset, 0);

                GameObject griglia = Instantiate(quadPrefab, transform);
                griglia.transform.position = position;
            }
        }
    }
}
