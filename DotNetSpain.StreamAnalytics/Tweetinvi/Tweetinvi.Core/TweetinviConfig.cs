﻿namespace Tweetinvi.Core
{
    public static class TweetinviConfig
    {
        public const long DEFAULT_ID = -1;
        public static bool SHOW_DEBUG = false;

        static TweetinviConfig()
        {
# if DEBUG
            SHOW_DEBUG = true;
#endif
        }
    }
}