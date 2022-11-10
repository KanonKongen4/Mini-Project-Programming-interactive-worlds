using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//:) This script is responsible for:
public class FindMostCenterObject : MonoBehaviour
{
    private float limitLow = 0.28f, limitHigh = 0.72f, limitXLow = 0.35f, limitXHigh = 0.65f;
    private IEnumerator TargetFinder;
    private Vector2 screenSize;
    private SpawnEnemies spawnEnemies;
    private Camera gameCam;
    public GameObject targetObject, crosshairGM;
    void Start()
    {
        screenSize[0] = Screen.width;
        screenSize[1] = Screen.height;

        spawnEnemies = FindObjectOfType<SpawnEnemies>();
        gameCam = Camera.main;

        StartCoroutine(FindTarget());
        MoveCrossOutOfSight();
    }

    private void AttachCrosshairToObject(GameObject gm)
    {
        crosshairGM.transform.SetParent(gm.transform, false);
        crosshairGM.transform.localPosition = Vector3.zero;
    }
    //public void DetachCrosshair()
    //{
    //    targetObject = null;
    //    crosshairGM.transform.SetParent(null, false);
    //}

    public void MoveCrossOutOfSight()
    {
        crosshairGM.transform.SetParent(null, false);
        crosshairGM.transform.position = new Vector3(9999, 9999, 999);
    }
    public void GetTarget()
    {
        float diversion = 0;

        for (int i = 0; i < spawnEnemies.enemies.Count; i++)
        {
            Vector3 pos = gameCam.WorldToViewportPoint(spawnEnemies.enemies[i].transform.position);
            if (pos.z > 13)
            {
                if (pos.x > limitXLow && pos.x < limitXHigh && pos.y > limitLow && pos.y < limitHigh)
                {
                    float lengthCathetusOpposite = 0.5f - pos.x;
                    float lengthCathetusAdjacent = 0.5f - pos.y;
                    float hypothenuse = Mathf.Sqrt(Mathf.Pow(lengthCathetusOpposite, 2) + Mathf.Pow(lengthCathetusOpposite, 2));
                    Debug.Log("pos x " + pos.x + ", pos y:  " + pos.y + ", pos z:  " + pos.z + "hypothenuse is: " + hypothenuse);

                    if (diversion < hypothenuse)
                    {
                        diversion = hypothenuse;
                        //TODO: find enemy closest to center of screen;
                        targetObject = spawnEnemies.enemies[i].gameObject;
                        AttachCrosshairToObject(targetObject);
                    }
                }
            }
        }
    }

    private IEnumerator FindTarget()
    {
        while (true)
        {
            GetTarget();
            if (targetObject != null)
            {
                Vector3 pos = gameCam.WorldToViewportPoint(targetObject.transform.position);
                if (pos.z < 13 || pos.x < limitXLow || pos.x > limitXHigh || pos.y < limitLow || pos.y > limitHigh)
                {
                    targetObject = null;
                    MoveCrossOutOfSight();
                }
            }
            yield return new WaitForSeconds(0.35f);
        }
    }
}