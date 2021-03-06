﻿using System;
using Tweetinvi.Core.Enum;
using Tweetinvi.Core.Injectinvi;
using Tweetinvi.Core.Interfaces.Factories;
using Tweetinvi.Core.Interfaces.Models;
using Tweetinvi.Core.Interfaces.Models.Parameters;

namespace Tweetinvi.Logic.Model.Parameters
{
    public class TweetSearchParameters : ITweetSearchParameters
    {
        private readonly IFactory<IGeoCode> _geoCodeUnityFactory;
        private readonly IGeoFactory _geoFactory;

        public TweetSearchParameters(
            IFactory<IGeoCode> geoCodeUnityFactory,
            IGeoFactory geoFactory)
        {
            _geoCodeUnityFactory = geoCodeUnityFactory;
            _geoFactory = geoFactory;

            MaximumNumberOfResults = -1;
            SinceId = -1;
            MaxId = -1;

            TweetSearchFilter = TweetSearchFilter.All;
        }

        public string SearchQuery { get; set; }
        public string Locale { get; set; }
        public int MaximumNumberOfResults { get; set; }

        public Language Lang { get; set; }
        public IGeoCode GeoCode { get; set; }
        public SearchResultType SearchType { get; set; }

        public DateTime Since { get; set; }
        public DateTime Until { get; set; }
        
        public long SinceId { get; set; }
        public long MaxId { get; set; }

        public TweetSearchFilter TweetSearchFilter { get; set; }

        public void SetGeoCode(ICoordinates coordinates, int radius, DistanceMeasure measure)
        {
            GeoCode = _geoCodeUnityFactory.Create();
            GeoCode.Coordinates = coordinates;
            GeoCode.Radius = radius;
            GeoCode.DistanceMeasure = measure;
        }

        public void SetGeoCode(double longitude, double latitude, int radius, DistanceMeasure measure)
        {
            var coordinates = _geoFactory.GenerateCoordinates(longitude, latitude);
            SetGeoCode(coordinates, radius, measure);
        }

        public void ClearGeoCode()
        {
            GeoCode = null;
        }
    }
}