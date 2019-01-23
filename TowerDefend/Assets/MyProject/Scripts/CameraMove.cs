using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    [SerializeField]
    private float CameraSpeed;

    /// <summary>
    /// X轴最大值
    /// </summary>
    private float xMax;

    /// <summary>
    /// Y轴最小值
    /// </summary>
    private float yMin;	
	
	void Update () {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * CameraSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * CameraSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * CameraSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * CameraSpeed * Time.deltaTime);
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -LevelManager.Instance.TileSize / 2, xMax), Mathf.Clamp(transform.position.y, yMin, LevelManager.Instance.TileSize / 2),-10);
    }

    public void SetLimits(Vector3 maxTile)
    {
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(new Vector3(1, 0));

        xMax = maxTile.x - worldPos.x;
        yMin = maxTile.y - worldPos.y;
    }
}
