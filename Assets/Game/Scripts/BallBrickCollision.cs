using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallBrickCollision : MonoBehaviour
{
    public InGameScoreMechanism scoreMechanism;
    public InGameManager gameManager;
    public Rigidbody2D potion;
    public GameObject newBrickPrefab;
    public GameObject brickShard;
    public Sprite[] breakEffectSprites;
    public int brickScore;
    public int potionRarity;
    private readonly Vector2[] _forces = {new Vector2(-1,2), new Vector2(1,2), new Vector2(1,0), 
        new Vector2(-1,0)};
    private readonly Vector3[] _positions = {new Vector3(-0.3f, 0.3f,0), new Vector3(0.3f, 0.3f,0), 
        new Vector3(0.3f, -0.3f,0), new Vector3(-0.3f, -0.3f,0)};
    private const float TIME_TO_DELAY_ENABLE = 0.09f;
    private BoxCollider2D _boxCollider2D;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        BoxCollider2D brickBoxCollider =  gameObject.GetComponent<BoxCollider2D>();
        brickBoxCollider.enabled = false;
        StartCoroutine(DelayEnable(brickBoxCollider));

        _boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        scoreMechanism.IncreaseScore(brickScore);
        
        BrickBreakEffect();
        
        if (potion != null) ThrowPotion();
        
        if (newBrickPrefab != null) ChangeBrick();
        
        else gameManager.ChangeBricksCount(false);

        StartCoroutine(DestroyBrick());
    }

    private void ThrowPotion()
    {
        if (Random.Range(0, potionRarity) == 0)
        {
            Instantiate(potion, gameObject.transform.position, Quaternion.Euler(0,0,-30));
        }
    }

    private void ChangeBrick()
    {
        GameObject newBrick =  Instantiate(newBrickPrefab, gameObject.transform.position, 
            gameManager.transform.rotation);
        newBrick.GetComponent<BallBrickCollision>().scoreMechanism = scoreMechanism;
        newBrick.GetComponent<BallBrickCollision>().gameManager = gameManager;
    }

    private IEnumerator DelayEnable(BoxCollider2D brickBoxCollider)
    {
        yield return new WaitForSeconds(TIME_TO_DELAY_ENABLE);
        brickBoxCollider.enabled = true;

    }

    private void BrickBreakEffect()
    {
        Transform transform = gameObject.transform;
        for (int i = 0; i < breakEffectSprites.Length; ++i)
        {
            GameObject newBrickShard = Instantiate(brickShard,transform.position, transform.rotation);
            newBrickShard.GetComponent<SpriteRenderer>().sprite = breakEffectSprites[i];
            Rigidbody2D rigidbody2D = newBrickShard.GetComponent<Rigidbody2D>();
            switch (i)
            {
                case 0:
                    rigidbody2D.AddForce(_forces[0],ForceMode2D.Impulse);
                    newBrickShard.transform.position += _positions[0];
                    break;
                case 1:
                    rigidbody2D.AddForce(_forces[1],ForceMode2D.Impulse);
                    newBrickShard.transform.position += _positions[1];
                    break;
                case 2:
                    rigidbody2D.AddForce(_forces[2],ForceMode2D.Impulse);
                    newBrickShard.transform.position += _positions[2];
                    break;
                case 3:
                    rigidbody2D.AddForce(_forces[3],ForceMode2D.Impulse);
                    newBrickShard.transform.position += _positions[3];
                    break;
                default:
                    throw new Exception("Brick breaks to 4 pieces only");
            }
            
        }
        StartCoroutine(DestroyShards());
    }

    private IEnumerator DestroyShards()
    {
        GameObject[] shards = GameObject.FindGameObjectsWithTag("Shard");
        yield return new WaitForSeconds(0.7f);
        foreach (GameObject shard in shards) Destroy(shard);
    }

    private IEnumerator DestroyBrick()
    {
        _boxCollider2D.enabled = false;
        _spriteRenderer.sprite = null;
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
