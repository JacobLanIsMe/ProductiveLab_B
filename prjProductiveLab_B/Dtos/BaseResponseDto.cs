namespace prjProductiveLab_B.Dtos
{
    public class BaseResponseDto
    {
        public bool isSuccess { get; set; }
        public string? errorMessage { get; set; }
        public void SetSuccess()
        {
            this.isSuccess = true;
            this.errorMessage = string.Empty;
        }
        public void SetError(string error)
        {
            this.isSuccess = false;
            this.errorMessage = error;
        }
    }
}
