namespace TakeHomeMunrosApi.Queries
{ public interface ISortingCriteria
    {
        string PropertyName { get; }
        bool IsAscending { get; }
        int? Priority { get; }
    }
}
