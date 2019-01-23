using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            StartCoroutine(collision.transform.GetComponent<Enemy>().EnemyBornEx(new Vector3(1f, 1f, 1f), new Vector3(0.1f, 0.1f, 0.1f)));
            StartCoroutine(waitForAnim(collision.transform.GetComponent<Enemy>()));
            SubPlayerHp();
        }
    }

    private void SubPlayerHp()
    {
        PlayerData.Instance.hp--;
        PlayerData.Instance.Save();
        GameManager.Instance.Update_Ui();
    }

    IEnumerator waitForAnim(Enemy enemy)
    {
        yield return new WaitForSeconds(1f);
        enemy.Release();
    }
}
