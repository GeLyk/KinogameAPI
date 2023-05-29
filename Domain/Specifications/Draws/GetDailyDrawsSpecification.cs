namespace Domain.Specifications.Draws
{
    public class GetDailyDrawsSpecification : Specification<Draw>
    {
        public GetDailyDrawsSpecification()
        {
            Query
                .Where(draw => draw.CreatedOn.Year == DateTime.Now.Year
                 && draw.CreatedOn.Month == DateTime.Now.Month
                 && draw.CreatedOn.Day == DateTime.Now.Day);
        }
    }
}
