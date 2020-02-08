using Bing.WxMiniProgram.Models;
using Newtonsoft.Json;

namespace Bing.WxMiniProgram.Sns.Results
{
    /// <summary>
    /// JsCode2Json 结果
    /// </summary>
    public class JsCode2JsonResult : WxJsonResult
    {
        /// <summary>
        /// 用户唯一标识
        /// </summary>
        [JsonProperty("openid")]
        public string OpenId { get; set; }

        /// <summary>
        /// 会话密钥
        /// </summary>
        [JsonProperty("session_key")]
        public string SessionKey { get; set; }

        /// <summary>
        /// 用户在开放平台的唯一标识符，在满足 UnionID 下发条件的情况下会返回。具体参看：https://mp.weixin.qq.com/debug/wxadoc/dev/api/uinionID.html
        /// </summary>
        [JsonProperty("unionid")]
        public string UnionId { get; set; }
    }
}
