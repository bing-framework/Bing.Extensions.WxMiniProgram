using System;
using Newtonsoft.Json;

namespace Bing.WxMiniProgram.Models
{
    /// <summary>
    /// 微信Json结果
    /// </summary>
    [Serializable]
    public class WxJsonResult
    {
        /// <summary>
        /// 错误码
        /// </summary>
        [JsonProperty("errcode")]
        public int ErrCode { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        [JsonProperty("errmsg")]
        public string ErrMsg { get; set; }
    }
}
