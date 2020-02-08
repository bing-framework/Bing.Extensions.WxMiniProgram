using Bing.WxMiniProgram.Auth.Results;
using WebApiClient;
using WebApiClient.Attributes;
using WebApiClient.DataAnnotations;

namespace Bing.WxMiniProgram.Auth
{
    /// <summary>
    /// 授权API
    /// </summary>
    [HttpHost("https://api.weixin.qq.com/")]
    [TraceFilter]
    public interface IAuthApi : IHttpApi
    {
        /// <summary>
        /// 登录凭证校验。通过 code 换取 session_key
        /// </summary>
        /// <param name="appId">小程序AppId</param>
        /// <param name="secret">小程序AppSecret</param>
        /// <param name="jsCode">登录时获取的code</param>
        /// <param name="grantType">授权类型，此处只需填写 authorization_code</param>
        [HttpGet("sns/jscode2session")]
        ITask<JsCode2JsonResult> JsCode2SessionAsync([AliasAs("appid")] string appId, [AliasAs("secret")] string secret,
            [AliasAs("js_code")] string jsCode,
            [AliasAs("grant_type")] string grantType = "authorization_code");

        /// <summary>
        /// 获取支付用户唯一标识。用户支付完成后，获取该用户的 UnionId，无需用户授权。
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="openId">支付用户唯一标识</param>
        /// <param name="transactionId">微信支付订单号</param>
        /// <param name="mchId">微信支付分配的商户号，和商户订单号配合使用</param>
        /// <param name="outTradeNo">微信支付商户订单号，和商户号配合使用</param>
        [HttpGet("wxa/getpaidunionid")]
        ITask<PaidUnionIdResult> GetPaidUnionIdAsync([AliasAs("access_token")] string accessToken,
            [AliasAs("openid")] string openId, [AliasAs("transaction_id")] string transactionId = "",
            [AliasAs("mch_id")] string mchId = "", [AliasAs("out_trade_no")] string outTradeNo = "");

        /// <summary>
        /// 获取小程序全局唯一后台接口调用凭据
        /// </summary>
        /// <param name="appId">小程序唯一凭证，即 AppID，可在「微信公众平台 - 设置 - 开发设置」页中获得。（需要已经成为开发者，且帐号没有异常状态）</param>
        /// <param name="secret">小程序唯一凭证密钥，即 AppSecret，获取方式同 appid</param>
        /// <param name="grantType"></param>
        /// <returns></returns>
        [HttpGet("cgi-bin/token")]
        ITask<AccessTokenResult> GetAccessTokenAsync([AliasAs("appid")]string appId, [AliasAs("secret")]string secret,
            [AliasAs("grant_type")]string grantType = "client_credential");
    }
}
