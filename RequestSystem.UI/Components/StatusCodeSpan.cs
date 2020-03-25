using Microsoft.AspNetCore.Mvc;

namespace RequestSystem.UI.Components
{
    [ViewComponent]
    public class StatusCodeSpan
    {
        public string Invoke(int statusCode) => statusCode switch
        {
            0 => "Открыта",
            1 => "Решена",
            2 => "Возврат",
            3 => "Закрыта",
            _ => string.Empty
        };
    }
}
