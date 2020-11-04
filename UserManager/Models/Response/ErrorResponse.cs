namespace UserManager.WebClient.Models.Response
{
    public class ErrorResponse
    {
        public bool IsSuccess { get; set; }
        public int ErrorId { get; set; }
        public string ErrorMsg { get; set; }
    }
}