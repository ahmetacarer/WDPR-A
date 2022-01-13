namespace WDPR_A.Extensions;
public static class HttpRequestExtensionMethods
{

    public static bool IsAjax(this HttpRequest request, string httpVerb = "")
    {
        if (request == null)
        {
            throw new ArgumentNullException("Request object is Null.");
        }

        if (!string.IsNullOrEmpty(httpVerb))
        {
            if (request.Method != httpVerb)
            {
                return false;
            }
        }

        if (request.Headers != null)
        {
            return request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }

        return false;
    }
}