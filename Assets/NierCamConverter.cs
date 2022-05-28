using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class NierCamConverter : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    private CinemachineBrain _brain;
    private bool camOnTurn;
    private NierCamDonus _nierCamDonus;
    private Vector3 disToPlayer;
    private bool shouldFollow;
    private void Start()
    {
        _brain = _camera.GetComponent<CinemachineBrain>();
    }

    private void LateUpdate()
    {
        if (!_brain.enabled && _nierCamDonus)
        {
            //new Vector3(transform.position.x,_nierCamDonus.transform.position.y,transform.position.z)
            _camera.transform.rotation = Quaternion.Euler(0,-90,0);
            if (shouldFollow)
            {
                _camera.transform.position = new Vector3(_camera.transform.position.x,transform.position.y,transform.position.z) - new Vector3(0,disToPlayer.y,disToPlayer.z);

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Nier"))
        {
            disToPlayer = Vector3.zero;
            StartCoroutine(LerpToPlayer());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Nier"))
        {
            _nierCamDonus= other.GetComponent<NierCamDonus>();
            if (_nierCamDonus)
            {
                _nierCamDonus.ControlMeshRenderers(false);
                _brain.enabled = false;
                var donusPos = IsFirstPivot(_nierCamDonus) ? _nierCamDonus.FirstPoint.position : _nierCamDonus.LastPoint.position;
                if (!camOnTurn)
                {
                    StartCoroutine(CamTurn(donusPos));
                    camOnTurn = true;
                }
                

            }
        }
    }

    IEnumerator LerpToPlayer()
    {
        Debug.Log("burdaiim");
        shouldFollow = false;
        camOnTurn = false;
        _camera.transform.LookAt(transform);
        _camera.transform.position = Vector3.Slerp(_camera.transform.position, transform.position,Time.deltaTime * 5);
        yield return new WaitForSeconds(0.2f);
        _brain.enabled = true;
        if(_nierCamDonus)
            _nierCamDonus.ControlMeshRenderers(true);
        _nierCamDonus = null;

    }
    IEnumerator CamTurn(Vector3 pos)
    {
        
        while (Vector3.Distance(_camera.transform.position,pos)>0.02f)
        {
            Debug.Log("gsd");
            _camera.transform.position = Vector3.Slerp(_camera.transform.position, pos, Time.deltaTime * 5);
    
            yield return null;
        }
        if (Vector3.Distance(_camera.transform.position,pos)<=0.02f)
        {
            disToPlayer = transform.position - _camera.transform.position;
            shouldFollow = true;
            camOnTurn = false;
        }
    
        
        
        
    }
    

    bool IsFirstPivot(NierCamDonus camDonus)
    {
        return Vector3.SqrMagnitude(_camera.transform.position - camDonus.FirstPoint.position) <= 
               Vector3.SqrMagnitude(_camera.transform.position - camDonus.LastPoint.position);
    }
}
