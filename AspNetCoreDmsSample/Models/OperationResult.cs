using System.Collections.Generic;

namespace DMSSample.Models
{
    public class OperationResult
    {
        public OperationResult(){
            Errors = new List<ResultError>();
            Warnings = new List<ResultWarning>();
        }
        public int ReturnCode { get; set; }
        public ResultSucccess Success { get; set; }

        public List<ResultError> Errors { get; set; }

        public List<ResultWarning> Warnings { get; set; } 

        public static OperationResult Succeeded(string message){
            OperationResult result = new OperationResult();
            result.ReturnCode = 0;
            result.Success = new ResultSucccess();
            result.Success.Message = message;
            return result;
        }

        public static OperationResult Failed(string message, List<ResultError> errors){
            OperationResult result = new OperationResult();
            result.ReturnCode = -1;
            result.Errors = errors;
            return result;
        }

        public static OperationResult Warning(string message, List<ResultWarning> warnings){
            OperationResult result = new OperationResult();
            result.ReturnCode = 1;
            result.Warnings = warnings;
            return result;
        }
    }

    public class ResultSucccess
    {
        public string Message { get; set; }
    }

    public class ResultError
    {
        public ResultError(){}
        public ResultError(int errorCode, string errorMessage){
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class ResultWarning
    {
        public ResultWarning(int warningCode, string warningMessage){
            WarningCode = warningCode;
            WarningMessage = warningMessage;
        }
        public int WarningCode { get; set; }
        public string WarningMessage { get; set; }
    }
}