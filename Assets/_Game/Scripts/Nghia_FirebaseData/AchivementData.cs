using System;
using System.Collections.Generic;

[Serializable]
public class AchivementData
{
    public bool checkAchivement;

    public AchivementData()
    {

    }
    public AchivementData(bool checkAchivement)
    {
        this.checkAchivement = checkAchivement;
    }
}
