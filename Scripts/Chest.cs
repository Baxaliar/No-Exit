using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collactable
{
    public Sprite emptyChest;
    public int goldenAmount = 5;


    protected override void OnCollect()
    {
        if (!collected)
        {
            collected= true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            GameManager.instance.goldens += goldenAmount;
            GameManager.instance.ShowText("+" + goldenAmount + " דמכüהום³ג!", 25, Color.yellow, transform.position, Vector3.up * 25, 1.5f);            
        }
    }
}
