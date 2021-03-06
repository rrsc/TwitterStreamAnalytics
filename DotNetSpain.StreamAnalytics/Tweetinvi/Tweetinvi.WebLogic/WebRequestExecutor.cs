﻿using System;
using System.IO;
using System.Net;
using Tweetinvi.Core.Helpers;
using Tweetinvi.Core.Interfaces.Exceptions;
using Tweetinvi.Core.Interfaces.oAuth;

namespace Tweetinvi.WebLogic
{
    /// <summary>
    /// Generate a Token that can be used to perform OAuth queries
    /// </summary>
    public class WebRequestExecutor : IWebRequestExecutor
    {
        private readonly IExceptionHandler _exceptionHandler;
        private readonly IWebHelper _webHelper;

        /// <summary>
        /// Headers of the latest WebResponse
        /// </summary>
        protected WebHeaderCollection _lastHeadersResult { get; private set; }

        public WebRequestExecutor(IExceptionHandler exceptionHandler, IWebHelper webHelper)
        {
            _exceptionHandler = exceptionHandler;
            _webHelper = webHelper;
        }

        public string ExecuteWebRequest(HttpWebRequest httpWebRequest)
        {
            WebResponse webResponse = null;

            try
            {
                // Opening the connection
                webResponse = _webHelper.GetWebResponse(httpWebRequest);
                Stream stream = webResponse.GetResponseStream();
                _lastHeadersResult = webResponse.Headers;

                if (stream != null)
                {
                    // Getting the result
                    var responseReader = new StreamReader(stream);
                    return responseReader.ReadLine();
                }

                // Closing the connection
                httpWebRequest.Abort();
            }
            catch (AggregateException aex)
            {
                var webException = aex.InnerException as WebException;
                if (webException != null)
                {
                    if (webResponse != null)
                    {
                        webResponse.Dispose();
                    }

                    if (httpWebRequest != null)
                    {
                        httpWebRequest.Abort();
                    }

                    if (httpWebRequest != null)
                    {
                        if (_exceptionHandler.LogExceptions)
                        {
                            var twitterException = _exceptionHandler.AddWebException(webException, httpWebRequest.RequestUri.AbsoluteUri);
                            throw twitterException;
                        }
                        
                        throw _exceptionHandler.GenerateTwitterException(webException, httpWebRequest.RequestUri.AbsoluteUri);
                    }

                    throw webException;
                }

                throw;
            }

            return null;
        }

        public string ExecuteMultipartRequest(IMultipartWebRequest multipartWebRequest)
        {
            return multipartWebRequest.GetResult();
        }
    }
}