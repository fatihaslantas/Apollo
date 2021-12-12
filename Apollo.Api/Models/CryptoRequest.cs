namespace Apollo.Api.Models;

public class CryptoRequest
{
    public CryptoRequest(string text)
    {
        this.Text = text;
    }
    public string Text { get; set; }
}
