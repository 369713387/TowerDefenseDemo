using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Knight3_Btn : TowerBtn {

    public Text Coins_Text;

    void Start()
    {
        Init_Data();
        Init_UI();
        //绑定按钮事件
        transform.GetChild(0).GetComponent<Button>().onClick.AddListener(BuyTower);
    }

    private void Init_Data()
    {
        Coins = 300;
        BtnName = "Knight3";
    }

    private void Init_UI()
    {
        Coins_Text.text = "$" + Coins;
        Coins_Text.color = Color.black;
    }


    void Update()
    {
        if (Coins >= PlayerData.Instance.coins)
        {
            BtnDisAble();
        }
        else
        {
            BtnEnAble();
        }
    }
    /// <summary>
    /// 可点击状态
    /// </summary>
    private void BtnDisAble()
    {
        transform.GetChild(0).GetComponent<Button>().enabled = false;
        transform.GetChild(0).GetComponent<Button>().image.color = Color.gray;
        transform.GetChild(1).GetComponent<Image>().color = Color.gray;
        transform.GetChild(2).GetComponent<Text>().color = Color.gray;
    }
    /// <summary>
    /// 不可点击状态
    /// </summary>
    private void BtnEnAble()
    {
        transform.GetChild(0).GetComponent<Button>().enabled = true;
        transform.GetChild(0).GetComponent<Button>().image.color = Color.white;
        transform.GetChild(1).GetComponent<Image>().color = Color.white;
        transform.GetChild(2).GetComponent<Text>().color = Color.black;
    }

    public override void PickTower()
    {
        GameManager.Instance.clickedBtn = this;
    }

    /// <summary>
    /// 购买塔
    /// </summary>
    public override void BuyTower()
    {
        if (PlayerData.Instance.ifCanBuy(Coins))
        {
            PickTower();
            Hover.Instance.SpriteActive();
        }
    }
    /// <summary>
    /// 移除选择的塔
    /// </summary>
    public override void RemovePick()
    {
        GameManager.Instance.clickedBtn = null;
    }
}
