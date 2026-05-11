/*
    The MiniGameTechnologyArea represent the technological are effected by the mini game, for example:
    - Technology;
    - Programming languages;
    - Frameworks;
    etc...
*/

using UnityEngine;

[System.Serializable]
public struct MiniGameTechnologyArea
{
    public Technology technology;
    public Texture2D icon;
}