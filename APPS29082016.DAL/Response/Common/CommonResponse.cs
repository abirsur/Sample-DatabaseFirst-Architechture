namespace APPS29082016.DAL.Response.Common
{
    public class CommonResponse
    {
        public static string GetStatusMessage(int code)
        {
            string statusMessage = @"Unknown error";
            switch (code)
            {
                case 2:
                    statusMessage = "Successful";
                    break;
                case -2:
                    statusMessage = "No result found";
                    break;
                case -4:
                    statusMessage = "Invalid Parameters";
                    break;
                case -6:
                    statusMessage = "Null or Empty string not allowed";
                    break;
                case -8:
                    statusMessage = "Duplicate entry not allowed";
                    break;
                case -99:
                    statusMessage = "Exception found. Please follow exception message";
                    break;
                default:
                    statusMessage = "Unknown error";
                    break;
            }
            return statusMessage;
        }
    }
}