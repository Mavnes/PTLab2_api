namespace PTLab2_api.Data.Services
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public bool Success { get; set; } = true;
        public string? Message { get; set; } = null;
        public string? Error { get; set; } = null;
        public List<string>? ErrorMessages { get; set; } = null;

        public override bool Equals(object? obj)
        {
            return obj is ServiceResponse<T> response &&
                   EqualityComparer<T?>.Default.Equals(Data, response.Data) &&
                   Success == response.Success &&
                   Message == response.Message &&
                   Error == response.Error &&
                   EqualityComparer<List<string>?>.Default.Equals(ErrorMessages, response.ErrorMessages);
        }
    }
}
