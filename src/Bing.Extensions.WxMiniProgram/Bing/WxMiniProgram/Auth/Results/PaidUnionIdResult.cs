using Bing.WxMiniProgram.Models;
using Newtonsoft.Json;

namespace Bing.WxMiniProgram.Auth.Results
{
    public class PaidUnionIdResult : WxJsonResult
    {
        /// <summary>
        /// 用户唯一标识，调用成功后返回
        /// </summary>
        [JsonProperty("unionid")]
        public string UnionId { get; set; }
    }
}
