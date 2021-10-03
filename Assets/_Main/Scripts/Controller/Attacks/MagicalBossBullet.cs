using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicalBossBullet : MonoBehaviour, IPooleable, IBullet
{
    [SerializeField] private PooleableType type;
    [SerializeField] private GameObject trapPrefab;
    private Vector2 offset = new Vector2(0, -0.3f);
    private AttackStats _attackStats;
    private bool canMove;
    private float timer;
    private Vector2 direction;

    public PooleableType Type => type;

    public void Initialize(Transform shootingPoint, AttackStats attackStats, Transform target = null)
    {
        _attackStats = attackStats;
        canMove = true;
        timer = _attackStats.LifeMagicalAttack;

        transform.position = shootingPoint.position;
        transform.rotation = shootingPoint.rotation;
        direction = transform.right;

        if (target != null) //Si le paso un objetivo, reescribimos la direccion
        {
            direction = target.position - shootingPoint.position;
            var rotation = direction.normalized;
            transform.right = rotation;
        }
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
            OnCollision();

        if (canMove)
            transform.position += (Vector3)direction * _attackStats.SpellSpeed * Time.deltaTime;


    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        LifeController life = collision.GetComponent<LifeController>();
        if (life != null)
        {
            life.TakeDamage(_attackStats.MagicalDamage);
            OnCollision();
        }
        else if (collision.gameObject.layer == 10) //Si collisiona con ground layer...
        {
            print(collision.name);
            Vector3 position = transform.position;
            Trap(position);
        }
    }
    private void Trap(Vector3 position)
    {
        var number = Random.Range(0, 2);
        print(number);
        if(number == 0) //para que no lo haga en todas las balas.
        {
            var trap = Instantiate(trapPrefab);
            trap.transform.position = position + (Vector3)offset;
        }
        OnCollision();
    }

    private void OnCollision()
    {
        canMove = false;
        timer = 0;
        PoolManager.instance.Store(this);
    }
}
