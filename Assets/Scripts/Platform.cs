using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public PlatformType platformType;
    public float dropThroughDuration = 0.5f;
    Collider2D _collider;
    bool _playerIsOnPlatform;

    public enum PlatformType
    {
        Normal,
        Moving,
        DropThroughPlatform
    }
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider2D>();
        // TODO: change the enum assignment to use a ToString() method
        if (gameObject.tag == "MovingPlatform")
        {
            platformType = PlatformType.Moving;
        }
        else if (gameObject.tag == "DropThroughPlatform")
        {
            platformType = PlatformType.DropThroughPlatform;
        }
        else
        {
            platformType = PlatformType.Normal;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerIsOnPlatform && platformType == PlatformType.DropThroughPlatform
            && Input.GetAxis("Vertical") < 0 && Input.GetAxis("Jump") > 0)
        {
            _collider.enabled = false;
            StartCoroutine(EnableCollider());
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        SetPlayerIsOnPlatform(other, true);
    }

    void OnCollisionExit2D(Collision2D other)
    {
        SetPlayerIsOnPlatform(other, false);
    }

    IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(dropThroughDuration);
        _collider.enabled = true;
    }

    void SetPlayerIsOnPlatform(Collision2D other, bool value)
    {
        if (other.gameObject.tag == "Player")
        {
            _playerIsOnPlatform = value;
        }
    }
}
