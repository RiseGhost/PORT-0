using UnityEngine;

[System.Serializable]
public class MotherBoardStatus : StatusImplement<MotherBoard>
{
    void OnValidate()
    {
        this.type = StatusType.MotherBoard;
    }
}