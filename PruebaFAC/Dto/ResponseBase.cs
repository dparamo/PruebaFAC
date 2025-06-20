namespace PruebaFAC.Dto;

public class ResponseBase
{
    public bool Error { get; set; }
    public int CodigoError { get; set; }
    public string Message { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
