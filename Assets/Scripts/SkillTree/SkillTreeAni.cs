using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeAni : MonoBehaviour
{
    private float speed = .75f;
    private Vector2 target;
    private Vector2 position;
    private Camera cam;
    public GameObject SkillTree;
    public GameObject DisplayText;

    public float targetY;

    private bool canMove;

    void Start()
    {
        canMove = false;
        target = new Vector2(SkillTree.transform.position.x, targetY);
        position = SkillTree.transform.position;

        cam = Camera.main;
    }
    // Start is called before the first frame update

    public void MoveOnScreen()
    {
        canMove = true;
    }

    private void Update()
    {
        if (canMove) {
            float step = speed * Time.deltaTime;
            float a = Mathf.Lerp(SkillTree.transform.position.y, targetY / 2f, step);
            float b = Mathf.Lerp(DisplayText.transform.position.y, targetY / 2f, step);

            SkillTree.transform.position = new Vector3(SkillTree.transform.position.x, a, SkillTree.transform.position.z);
            DisplayText.transform.position = new Vector3(DisplayText.transform.position.x, b, DisplayText.transform.position.z);
        }
    }

}
