namespace Domain.Specifications.Draws
{
    public class GetLatestDrawSpecification : Specification<Draw>, ISingleResultSpecification<Draw>
    {
        public GetLatestDrawSpecification()
        {
            Query
                .OrderByDescending(x => x.CreatedOn).Take(1);
        }
    }
}
