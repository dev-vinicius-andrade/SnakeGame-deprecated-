using System.Net;
using SnakeGame.Infrastructure.Data.Models;

namespace SnakeGame.Infrastructure.Helpers
{
    public class ResponseHelper
    {

       

        public static ResponseModel DefaultUnauthorized
            => CreateUnauthorized(Constants.UNAUTHORIZED);
        public static ResponseModel DefaultInvalidUser
            => CreateUnauthorized(Constants.INVALID_USER);

        public static ResponseModel DefaultInvalidUsernamePassword
            => CreateUnauthorized(Constants.INVALID_USERNAME_PASSWORD);
        public static ResponseModel DefaultLoggedUser
            =>CreateOk(Constants.LOGGED);
        public static ResponseModel DefaultRegisteredUser
            =>CreateOk(Constants.REGISTERED);
        public static ResponseModel DefaultUserAlreadyExists
            =>CreateBadRequest(Constants.USER_ALREADY_EXISTS);
        public static ResponseModel CreateBadRequest(string message)=>new ResponseModel
        {
            HasError = true,
            Message = message,
            Code = (int)HttpStatusCode.OK

        };
        public static ResponseModel CreateUnauthorized(string message)=>new ResponseModel
        {
            HasError = true,
            Message = message,
            Code = (int)HttpStatusCode.Unauthorized

        };
        public static ResponseModel CreateOk(string message)=>new ResponseModel
        {
            HasError = false,
            Message = message,
            Code = (int)HttpStatusCode.OK

        };
    }
}
