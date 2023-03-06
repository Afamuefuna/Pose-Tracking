using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetPos : MonoBehaviour
{
    [SerializeField] private Transform pos;
    [SerializeField] private Transform trackedPos;
    [SerializeField] private TMP_Text xPos, yPos, zPos;
    [SerializeField] private int annote;
    [SerializeField] private int firstMatchPos, secondMatchPos;
    [SerializeField] private bool hasMatchedFirst, hasMatchedSecond;
    [SerializeField] private Animator _animator;

    // Update is called once per frame
    void Update()
    {
        if (pos.GetChild(annote) != null)
        {
            trackedPos = pos.GetChild(annote);
            var localPosition = trackedPos.localPosition;
            
            xPos.text = localPosition.x.ToString();
            yPos.text = localPosition.y.ToString();
            zPos.text = localPosition.z.ToString();

            if (trackedPos.localPosition.y >= secondMatchPos)
            {
                if (!isJumping)
                {
                    if (!hasMatchedSecond)
                    {
                        _animator.SetBool("Jump", true);
                        GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpForce;
                        isJumping = true;
                        hasMatchedSecond = true;
                        hasMatchedFirst = false;
                    }
                }
            }
            
            if (trackedPos.localPosition.y <= firstMatchPos)
            {
                if (!hasMatchedFirst)
                {
                    hasMatchedFirst = true;
                    hasMatchedSecond = false;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            _animator.SetBool("Jump", false);
        }
    }

    private float jumpForce = 5f;
    [SerializeField]private bool isJumping;
}
