namespace API.DTOs;
public class ResponseMessageDto
{
    public string Msg { get; set; }

    public ResponseMessageDto(string message)
    {
        Msg = message;
    }
}
