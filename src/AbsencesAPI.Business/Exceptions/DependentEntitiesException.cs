using AbsencesAPI.Common.Model;
using System.Runtime.Serialization;

namespace AbsencesAPI.Business.Exceptions;

[Serializable]
public class DependentEntitiesException : Exception
{
    public List<int> DependentEntities { get; }
    public string DependentEntityName { get; }

    public DependentEntitiesException()
    {
    }

    public DependentEntitiesException(List<int> dependentEntities, string dependentEntityName)
    {
        this.DependentEntities = dependentEntities;
        DependentEntityName = dependentEntityName;
    }

    public DependentEntitiesException(string? message) : base(message)
    {
    }

    public DependentEntitiesException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected DependentEntitiesException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}