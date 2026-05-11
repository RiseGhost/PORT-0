using System.Collections.Generic;

public interface Status<T>
{
    public StatusType GetType();
    public T GetValue();
}