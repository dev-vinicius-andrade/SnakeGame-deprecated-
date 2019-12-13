namespace SnakeGame.Infrastructure.Data.Models
{
    public class ResponseModel
    {
        public ResponseModel()
        {

        }
        public bool HasError { get; set; }
        public int? Code{ get; set; }
        public string Message { get; set; }
        public object Object { get; set; }

    }
}
