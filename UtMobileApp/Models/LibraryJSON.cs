using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace UtMobileApp.Models
{
    public class LibraryJSON
    {
        public class Id
        {
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class Updated
        {
            [JsonProperty("$t")]
            public DateTime t { get; set; }
        }

        public class Category
        {
            public string scheme { get; set; }
            public string term { get; set; }
        }

        public class Title
        {
            public string type { get; set; }
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class Link
        {
            public string rel { get; set; }
            public string type { get; set; }
            public string href { get; set; }
        }

        public class Name
        {
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class Email
        {
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class Author
        {
            public Name name { get; set; }
            public Email email { get; set; }
        }

        public class OpenSearchTotalResults
        {
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class OpenSearchStartIndex
        {
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class Id2
        {
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class Updated2
        {
            [JsonProperty("$t")]
            public DateTime t { get; set; }
        }

        public class Category2
        {
            public string scheme { get; set; }
            public string term { get; set; }
        }

        public class Title2
        {
            public string type { get; set; }
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class Content
        {
            public string type { get; set; }
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class Link2
        {
            public string rel { get; set; }
            public string type { get; set; }
            public string href { get; set; }
        }

        public class GsxNumber
        {
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class GsxBooktitles
        {
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class GsxNameandsurname
        {
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class GsxPlaceofpublciation
        {
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class GsxYear
        {
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class GsxCkd7g
        {
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class Entry
        {
            public Id2 id { get; set; }
            public Updated2 updated { get; set; }
            public List<Category2> category { get; set; }
            public Title2 title { get; set; }
            public Content content { get; set; }
            public List<Link2> link { get; set; }
            [JsonProperty("gsx$number")]
            public GsxNumber Number { get; set; }
            [JsonProperty("gsx$booktitles")]
            public GsxBooktitles BookTitle { get; set; }
            [JsonProperty("gsx$nameandsurname")]
            public GsxNameandsurname Author { get; set; }
            [JsonProperty("gsx$placeofpublciation")]
            public GsxPlaceofpublciation PlaceOfPublication { get; set; }
            [JsonProperty("gsx$year")]
            public GsxYear Year { get; set; }
            [JsonProperty("gsx$_ckd7g")]
            public GsxCkd7g gsx_ckd7g { get; set; }
        }

        public class Feed
        {
            public string xmlns { get; set; }
            [JsonProperty("xmlns$openSearch")]
            public string xmlnsopenSearch { get; set; }
            [JsonProperty("xmlns$gsx ")]
            public string xmlnsgsx { get; set; }
            public Id id { get; set; }
            public Updated updated { get; set; }
            public List<Category> category { get; set; }
            public Title title { get; set; }
            public List<Link> link { get; set; }
            public List<Author> author { get; set; }
            [JsonProperty("openSearch$totalResults")]
            public OpenSearchTotalResults openSearchtotalResults { get; set; }
            [JsonProperty("openSearch$startIndex")]
            public OpenSearchStartIndex openSearchstartIndex { get; set; }
            public List<Entry> entry { get; set; }
        }

        public class RootObject
        {
            public string version { get; set; }
            public string encoding { get; set; }
            public Feed feed { get; set; }
        }
    }
}
