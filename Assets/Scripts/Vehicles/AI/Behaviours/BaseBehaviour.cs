using System.Collections.Generic;

public class BaseBehaviour : IBehaviour
{
    public BaseBehaviour()
    {
    }

    public List<IState> GetStates()
    {
        throw new System.NotImplementedException();
    }
}