using UnityEngine;
using System;

public class BumperHit : MonoBehaviour
{
    [SerializeField] private int scoreValue = 100;
    public static event Action<Transform, int> onBumperHit;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball")) {
            onBumperHit?.Invoke(transform, scoreValue);//bericht versturen dat er een bumper geraakt is. De tag en waarde sturen we mee

        }
    }
}
