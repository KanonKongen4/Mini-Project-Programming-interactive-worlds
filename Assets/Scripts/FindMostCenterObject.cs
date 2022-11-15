using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//:) This script is responsible for:
public class FindMostCenterObject : MonoBehaviour
{
    private float limitYLow = 0.28f, limitYHigh = 0.72f, limitXLow = 0.35f, limitXHigh = 0.65f;
    private IEnumerator TargetFinder;
    private Vector2 screenSize;
    private SpawnEnemies spawnEnemies;
    private Camera gameCam;
    public GameObject targetObject, crosshairGM;
    void Start()
    {
        screenSize[0] = Screen.width; //Getting the width of the screen in pixels
        screenSize[1] = Screen.height;//Getting the height of the screen in pixels

        spawnEnemies = FindObjectOfType<SpawnEnemies>();
        gameCam = Camera.main;

        StartCoroutine(FindTarget());
        MoveCrossOutOfSight(); //Move the crosshair to a point in 3D space, which the player is unlikely to see
    }

    private void AttachCrosshairToObject(GameObject gm)
    {
        crosshairGM.transform.SetParent(gm.transform, false); //makes the crosshair gameobject a child of the input gameobject
        crosshairGM.transform.localPosition = Vector3.zero; //Resets the crosshair gameobject position
    }

    public void MoveCrossOutOfSight()
    {
        crosshairGM.transform.SetParent(null, false);
        crosshairGM.transform.position = new Vector3(9999, 9999, 999);
    }
    public void GetTarget()
    {
        float diversion = 0;

        for (int i = 0; i < spawnEnemies.enemies.Count; i++) //Loops through the list of spawned enemies
        {
            Vector3 pos = gameCam.WorldToViewportPoint(spawnEnemies.enemies[i].transform.position); // Transforming the enemy world position to screen coordinates
            // ranging from 0,0 at bottom left to 1,1 at top right //  The z position is in world units from the camera.
            if (pos.z > 13)
            {
                if (pos.x > limitXLow && pos.x < limitXHigh && pos.y > limitYLow && pos.y < limitYHigh) // if the position if within the limis..
                {
                    float lengthCathetusOpposite = 0.5f - pos.x; //..calculate the length of the opposite cathetus
                    float lengthCathetusAdjacent = 0.5f - pos.y;//..calculate the length of the adjacent cathetus

                    float hypothenuse = Mathf.Sqrt(Mathf.Pow(lengthCathetusOpposite, 2) + Mathf.Pow(lengthCathetusOpposite, 2)); //..calculate the length of the hyphotenuse aka. the distance from the center of the screen to the enemy
                    //Debug.Log("pos x " + pos.x + ", pos y:  " + pos.y + ", pos z:  " + pos.z + "hypothenuse is: " + hypothenuse);

                    if (diversion < hypothenuse) //checks if the current diversion if less than the calculated distance
                    {
                        diversion = hypothenuse; //... sets diversion to the distance
                        //TODO: find enemy closest to center of screen;
                        targetObject = spawnEnemies.enemies[i].gameObject; // ... target object is set by the index
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
                if (pos.z < 13 || pos.x < limitXLow || pos.x > limitXHigh || pos.y < limitYLow || pos.y > limitYHigh)
                {
                    targetObject = null;
                    MoveCrossOutOfSight();
                }
            }
            yield return new WaitForSeconds(0.35f);
        }
    }
}