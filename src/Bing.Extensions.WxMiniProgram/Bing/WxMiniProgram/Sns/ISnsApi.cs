using Bing.WxMiniProgram.Sns.Results;
using WebApiClient;
using WebApiClient.Attributes;
using WebApiClient.DataAnnotations;

namespace Bing.WxMiniProgram.Sns
{
    /// <summary>
    /// 登录授权API
    /// </summary>
    [HttpHost("https://api.weixin.qq.com/")]
    [TraceFilter]
    public interface ISnsApi : IHttpApi
    {
        /// <summary>
        /// 登录凭证校验。通过 code 换取 session_key
        /// </summary>
        /// <param name="appId">小程序AppId</param>
        /// <param name="secret">小程序AppSecret</param>
        /// <param name="jsCode">登录时获取的code</param>
        /// <param name="grantType">授权类型，此处只需填写 authorization_code</param>
        [HttpGet("")]
        ITask<JsCode2JsonResult> JsCode2SessionAsync([AliasAs("appid")] string appId, [AliasAs("secret")] string secret,
            [AliasAs("js_code")] string jsCode,
            [AliasAs("grant_type")] string grantType = "authorization_code");
    }
}
