namespace Domain.Specifications.Columns
{
    public class GetColumnByIdSpecification : Specification<Column>, ISingleResultSpecification<Column>
    {
        public GetColumnByIdSpecification(int id)
        {
            Query
                .Where (x => x.Id == id);
        }
    }
}
