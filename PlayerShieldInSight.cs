using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShieldInSight : MonoBehaviour
{
    [SerializeField] BoxCollider2D boxcollider;
    [SerializeField] float colliderDistance;
    [SerializeField] float range;
    [SerializeField] LayerMask enemylayer;
    private meleeEnemy MeleeEnemy;
    
    public bool EnemyINsight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxcollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(boxcollider.bounds.size.x * range,
            boxcollider.bounds.size.y, boxcollider.bounds.size.z), 0, Vector2.left, 0, enemylayer);
        if (hit.collider != null)
        {
            MeleeEnemy = hit.transform.GetComponent<meleeEnemy>();
        }
        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(boxcollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxcollider.bounds.size.x * range, boxcollider.bounds.size.y, boxcollider.bounds.size.z));
    }
}
