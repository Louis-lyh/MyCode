using System;
using DG.Tweening;
using UnityEngine;

public class CoinBallSamll : MonoBehaviour
{
    // 是否出生
    public bool IsBirth;
    // Start is called before the first frame update
    void Start()
    {
        IsBirth = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 出生
    /// </summary>
    public void Birth(Vector3 birthPos,Vector3 endPos)
    {
        transform.localPosition = Vector3.zero;
        // 移动
        transform.DOMove(endPos,0.5f).OnComplete(() =>
        {
            IsBirth = true;
        });
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag.Equals("Gear"))
            Destroy();
    }
    
    private void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.tag.Equals("Gear"))
            Destroy();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag.Equals("Gear"))
            Destroy();
    }

    private void Destroy()
    {
        if(!IsBirth)
            return;
        transform.DOKill();
        GameObject.Destroy(gameObject);
    }
}
