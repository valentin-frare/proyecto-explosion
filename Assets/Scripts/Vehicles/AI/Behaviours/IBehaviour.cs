using System.Collections.Generic;

public interface IBehaviour
{
    public abstract List<IState> GetStates();
}