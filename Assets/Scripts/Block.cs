using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
     SpriteRenderer sprite;
    [SerializeField] Sprite[] sprites;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        int rand = Helper.randomziation(0, sprites.Length);
        sprite.sprite = sprites[rand];
    }
}
