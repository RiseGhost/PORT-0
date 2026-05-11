using UnityEngine;

/*
    Description:
        This class will be responsible for returning all different types of content to the save in Resources.
*/

public class BuildContentPopUp
{
    public static PopContentTask Build()
    {
        return Resources.Load<PopContentTask>("UI/PopUp/TaskContent");
    }
}