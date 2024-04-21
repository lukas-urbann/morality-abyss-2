using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour
{
    public float bobbingAmount = 0.075f;
    private Player.Controller _pController;
    private float _defPosition = 0, _timer = 0;

    void Start()
    {
        _pController = gameObject.transform.root.GetComponent<Player.Controller>();
        _defPosition = transform.localPosition.y;
    }
    
    void Update()
    {
        if(_pController.properties.CharacterController.isGrounded && (Mathf.Abs(_pController.properties.MoveDirection.x) > 0.1f || Mathf.Abs(_pController.properties.MoveDirection.z) > 0.1f))
        {
            if (_pController.isRunning && _pController.canRun)
                _timer += Time.deltaTime * 17;
                transform.localPosition = new Vector3(transform.localPosition.x, _defPosition + Mathf.Sin(_timer) * bobbingAmount, transform.localPosition.z);
                
            _timer += Time.deltaTime * 13;
            transform.localPosition = new Vector3(transform.localPosition.x, _defPosition + Mathf.Sin(_timer) * bobbingAmount, transform.localPosition.z);
        }
        else
        {
            _timer = 0;
            transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(transform.localPosition.y, _defPosition, Time.deltaTime * 16), transform.localPosition.z);
        }
    }
}