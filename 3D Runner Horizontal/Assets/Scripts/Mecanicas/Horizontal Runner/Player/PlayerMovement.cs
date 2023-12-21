using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Lane
{
    Left, Middle, Right
}


public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Lane _currentLane = Lane.Middle;
    [SerializeField] 
    private Lane _previousLane = Lane.Middle;
    [SerializeField]
    private Dictionary<Lane, float> _lanePositions = new Dictionary<Lane, float>();
    private Vector3 _currentPlayerPosition = new Vector3();
    private float _timeToChangeLane = 0;

    private void Start()
    {
        _lanePositions[Lane.Middle] =  0f;
        _lanePositions[Lane.Left]   = -6.65f;
        _lanePositions[Lane.Right]  =  6.65f;
    

    }

    /// <summary>
    ///  Muda a raia que o personagem se movimenta.
    /// </summary>
    /// <param name="lane">Seta qual </param>
    public void ChangeLane(Lane lane)
    {
        _previousLane = _currentLane;
        _currentLane = lane;
        _timeToChangeLane = 0;
    }

    public Lane GetCurrentLane()
    {
        return _currentLane;
    }

    public void ChangeLanePosition(Lane lane, float position)
    {
        _lanePositions[lane] = position;
    }

    private float GetPlayerInLanePosition(float velocity)
    {
        if(_currentPlayerPosition.x != _lanePositions[_currentLane]) {
            if (velocity <= 8)
                _timeToChangeLane += 8 * Time.deltaTime;
            else if (velocity <= 16)
                _timeToChangeLane += velocity *  Time.deltaTime;
            else
                _timeToChangeLane += 16 * Time.deltaTime;
        }

        return Mathf.Lerp(
            _lanePositions[_previousLane], 
            _lanePositions[_currentLane], 
            _timeToChangeLane
            );
    }

    public Vector3 GetCurrentPosition(float velocity)
    {
        _currentPlayerPosition = new Vector3(
                                    GetPlayerInLanePosition(velocity), 
                                    GetCurrentYPosition(), 
                                    _currentPlayerPosition.z + velocity * Time.deltaTime
                                );

        return _currentPlayerPosition;
    }

    public void SetStartPositionPlayer(Vector3 playerPosition)
    {
        _currentPlayerPosition = playerPosition;
    }

    public void Stop()
    {

    }

    public float GetCurrentYPosition()
    {
        return _currentPlayerPosition.y;
    }

    public void SetCurrentYPosition(float y) { 
        _currentPlayerPosition.y = y;
    }

    public void Jump(float heightJump)
    {
        float _timeToJump = 0;
        float _y_position = _currentPlayerPosition.y;
        while (_currentPlayerPosition.y != heightJump)
        {
            Debug.Log(_timeToJump);

            _timeToJump += 2 * Time.deltaTime;
            SetCurrentYPosition(
                Mathf.Lerp(
                    _y_position,
                    heightJump,
                    _timeToJump
                )
            );
        }
        TurnDown();
    }

    public void TurnDown()
    {
        float _timeToJump = 0;
        float _y_position = _currentPlayerPosition.y;
        while (_y_position != 1.36f)
        {
            _timeToJump += 2 * Time.deltaTime;
       
            SetCurrentYPosition(
                Mathf.Lerp(
                    _y_position,
                    1.36f,
                    _timeToJump
                )
            );
        }
    }
}
