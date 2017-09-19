namespace TeachersDiary.Common
{
    public class OperationStatus
    {
        public OperationStatus(bool isSuccessful)
        {
            IsSuccessful = isSuccessful;
        }

        public OperationStatus(bool isSuccessful, string message)
        {
            IsSuccessful = isSuccessful;
            Message = message;
        }

        public bool IsSuccessful { get; }
        public string Message { get; }
    }

    public class SuccessStatus : OperationStatus
    {
        public SuccessStatus() 
            : base (true)
        {

        }
    }

    public class FailureStatus : OperationStatus
    {
        public FailureStatus(string message)
            : base(false, message)
        {
            
        }
        
    }
}
