using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MoveEnemy : MonoBehaviour
{
    float speed;
    [SerializeField] float moveSpeed;
    [SerializeField] float rotSpeed;
   [SerializeField] Vector2 min, max;
  
    private void Start()
    {
        int rand = Random.Range(0, 2);
        if (rand == 0)
            Left();
        else
            Right();
    }
    void Left()
    {
         transform.DOMoveX(min.x, moveSpeed).OnComplete(() =>
        {
            Right();
        });
    }
    void Right()
    {
        transform.DOMoveX(max.x, moveSpeed).OnComplete(() =>
        {
            Left();
        });
    }
    private void Update()
    {
        speed += Time.deltaTime*rotSpeed;
        transform.eulerAngles = new Vector3(0, 0, speed);
    }

}
