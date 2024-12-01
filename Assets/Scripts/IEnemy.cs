using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    Transform Transform { get; } // Expose the transform
    void PlayerAttacking(int damage, Vector2 direction);
}

