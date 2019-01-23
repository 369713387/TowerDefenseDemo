using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour {

    static private Hover m_instance;
    static public Hover Instance
    {
        get
        {
            return m_instance;
        }
    }

    public SpriteRenderer spriteRenderer;

    public SpriteRenderer rangeRenderer;

    private void Awake()
    {
        m_instance = this;
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
        rangeRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    private void Start()
    {        
        SpriteDeActive();
    }

    void Update () {
        if (spriteRenderer.enabled)
        {
            FollowMouse();
        }        
    }

    /// <summary>
    /// 跟随鼠标
    /// </summary>
    private void FollowMouse()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    /// <summary>
    /// 激活图片
    /// </summary>
    public void SpriteActive()
    {
        spriteRenderer.gameObject.SetActive(true);
        spriteRenderer.enabled = true;
        rangeRenderer.enabled = true;
        spriteRenderer.sprite = GameManager.Instance.clickedBtn.TowerIcon;
    }

    /// <summary>
    /// 隐藏图片
    /// </summary>
    public void SpriteDeActive()
    {
        if (GameManager.Instance.clickedBtn != null)
        {
            GameManager.Instance.clickedBtn.RemovePick();
        }       
        spriteRenderer.enabled = false;
        rangeRenderer.enabled = false;
        spriteRenderer.gameObject.SetActive(false);        
    }

}
