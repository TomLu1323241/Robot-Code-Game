using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    [SerializeField] float deplay = .1f;
    [SerializeField] Sprite[] sprites = null;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CoinAnimation());
    }

    IEnumerator CoinAnimation()
    {
        int count = 0;
        SpriteRenderer spriteRenderer = this.GetComponent<SpriteRenderer>();
        while(true)
        {
            spriteRenderer.sprite = sprites[count % sprites.Length];
            count++;
            yield return new WaitForSeconds(deplay);
        }
    }

}
