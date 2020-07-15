using System;
using System.Collections.Generic;
using System.Text;

namespace SmartPillowAuthLib.OAuth2.GoogleOAuth
{
    /// <summary>
    ///     Contains the type and value of token.
    /// </summary>
    public class GoogleOAuthToken
    {
        /// <summary>
        ///     Type of token.
        /// </summary>
        public string TokenType { get; set; }
        /// <summary>
        ///     Value of token.
        /// </summary>
        public string AccessToken { get; set; }
    }
}
