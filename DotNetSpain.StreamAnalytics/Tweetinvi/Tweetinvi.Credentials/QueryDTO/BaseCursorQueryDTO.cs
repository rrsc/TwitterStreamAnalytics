﻿using Newtonsoft.Json;
using Tweetinvi.Core.Interfaces.Credentials.QueryDTO;

namespace Tweetinvi.Credentials.QueryDTO
{
    public abstract class BaseCursorQueryDTO : IBaseCursorQueryDTO
    {
        [JsonProperty("previous_cursor")]
        public long PreviousCursor { get; set; }

        [JsonProperty("next_cursor")]
        public long NextCursor { get; set; }

        [JsonProperty("previous_cursor_str")]
        public string PreviousCursorStr { get; set; }

        [JsonProperty("next_cursor_str")]
        public string NextCursorStr { get; set; }

        [JsonIgnore]
        public string RawJson { get; set; }

        public abstract int GetNumberOfObjectRetrieved();
    }
}