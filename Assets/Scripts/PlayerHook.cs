using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHook : MonoBehaviour
{
    public Collider2D hookCollider;
    bool isActive = false;
    public static bool isHookMoving, isHookStuck, isReeling, isBouncing;
    public GameObject hook, hookOnCharacter;
    public GameObject hookOriginalPos;
    public float hookSpeed, gravityScale;
    public GameObject player;
    public float playerMoveSpeed;

    private Vector3 targetPosition;

    public Transform lineRendererHolder;
    public LineRenderer lineRederer;

    public Rigidbody2D playerRigidbody;
    public float reelForce;
    public float maxReelForce;

    void Start()
    {
        reelForce = 175;
        maxReelForce = 1300;

        isActive = false;
        hookSpeed = 9;
        gravityScale = 0.7f;

        lineRederer = lineRendererHolder.GetComponent<LineRenderer>();
        lineRederer.enabled = false;
    }

    void Update()
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        if (!isHookMoving && isHookStuck == false)
        {
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            transform.rotation = rotation;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (isHookMoving == false)
            {
                if (!isActive)
                {
                    hook.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;

                    hook.transform.position = hookOnCharacter.transform.position;
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                    hook.transform.rotation = rotation;

                    isActive = true;
                    hookCollider.enabled = isActive;
                    hook.GetComponent<Rigidbody2D>().gravityScale = gravityScale;

                    hook.SetActive(true);
                    hookOnCharacter.SetActive(false);

                    Vector2 hookDirection = dir.normalized;

                    targetPosition = transform.position + (Vector3)(hookDirection * 1000f);

                    isHookMoving = true;

                }
            }
        }
        if (isHookMoving)
        {
            if (isBouncing == false)
            {
                hook.transform.position = Vector3.MoveTowards(hook.transform.position, targetPosition, hookSpeed * Time.deltaTime);
            }
        }

        if (isHookMoving == true || isHookStuck == true)
        {
            hook.transform.position = hookOnCharacter.transform.position;

            isBouncing = false;
            isReeling = false;
            isHookMoving = false;
            isHookStuck = false;

            hook.SetActive(false);
            hookOnCharacter.SetActive(true);

            isActive = false;
            hookCollider.enabled = isActive;
        }
    

    if(isHookStuck == true)
        {
            if(Input.GetKey(KeyCode.E))
            {
                isReeling = true;

                Vector2 directionToHook = hook.transform.position - player.transform.position;

                directionToHook.Normalize();

                float currentReelForce = Mathf.Lerp(reelForce, maxReelForce, Time.time);

                playerRigidbody.AddForce(directionToHook * currentReelForce * Time.deltaTime);
            }
        }

       else
        {
            isReeling = false ;
        }

        if(isHookMoving || isHookStuck)
        {
            lineRederer.SetPosition(0, transform.position);
            lineRederer.SetPosition(1, lineRendererHolder.position);
            lineRederer.enabled = true;
        }
        else
        {
            lineRederer.enabled = false;
        }
    }
}

