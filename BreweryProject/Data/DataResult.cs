namespace BreweryProject.Data
{
    public class DataResult<T>
    {
        public T Data { get; set; }

        public string ErrorMessage { get; set; }
    }
}
