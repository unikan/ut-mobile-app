using System;
using System.Collections.Generic;
using System.Text;

namespace UtMobileApp.Models
{
    class EventsJSON
    {
        public class StartDateDetails
        {
            public string year { get; set; }
            public string month { get; set; }
            public string day { get; set; }
            public string hour { get; set; }
            public string minutes { get; set; }
            public string seconds { get; set; }
        }

        public class EndDateDetails
        {
            public string year { get; set; }
            public string month { get; set; }
            public string day { get; set; }
            public string hour { get; set; }
            public string minutes { get; set; }
            public string seconds { get; set; }
        }

        public class UtcStartDateDetails
        {
            public string year { get; set; }
            public string month { get; set; }
            public string day { get; set; }
            public string hour { get; set; }
            public string minutes { get; set; }
            public string seconds { get; set; }
        }

        public class UtcEndDateDetails
        {
            public string year { get; set; }
            public string month { get; set; }
            public string day { get; set; }
            public string hour { get; set; }
            public string minutes { get; set; }
            public string seconds { get; set; }
        }

        public class CostDetails
        {
            public string currency_symbol { get; set; }
            public string currency_position { get; set; }
            public List<object> values { get; set; }
        }

        public class Urls
        {
            public string self { get; set; }
            public string collection { get; set; }
        }

        public class Category
        {
            public string name { get; set; }
            public string slug { get; set; }
            public int term_group { get; set; }
            public int term_taxonomy_id { get; set; }
            public string taxonomy { get; set; }
            public string description { get; set; }
            public int parent { get; set; }
            public int count { get; set; }
            public string filter { get; set; }
            public int id { get; set; }
            public Urls urls { get; set; }
        }

        public class Event
        {
            public int id { get; set; }
            public string global_id { get; set; }
            public List<string> global_id_lineage { get; set; }
            public string author { get; set; }
            public string status { get; set; }
            public string date { get; set; }
            public string date_utc { get; set; }
            public string modified { get; set; }
            public string modified_utc { get; set; }
            public string url { get; set; }
            public string rest_url { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public string excerpt { get; set; }
            public string slug { get; set; }
            public bool image { get; set; }
            public bool all_day { get; set; }
            public string start_date { get; set; }
            public StartDateDetails start_date_details { get; set; }
            public string end_date { get; set; }
            public EndDateDetails end_date_details { get; set; }
            public string utc_start_date { get; set; }
            public UtcStartDateDetails utc_start_date_details { get; set; }
            public string utc_end_date { get; set; }
            public UtcEndDateDetails utc_end_date_details { get; set; }
            public string timezone { get; set; }
            public string timezone_abbr { get; set; }
            public string cost { get; set; }
            public CostDetails cost_details { get; set; }
            public string website { get; set; }
            public bool show_map { get; set; }
            public bool show_map_link { get; set; }
            public bool hide_from_listings { get; set; }
            public bool sticky { get; set; }
            public bool featured { get; set; }
            public List<Category> categories { get; set; }
            public List<object> tags { get; set; }
            public object venue { get; set; }
            public List<object> organizer { get; set; }
        }

        public class RootObject
        {
            public List<Event> events { get; set; }
            public string rest_url { get; set; }
            public string total { get; set; }
            public int total_pages { get; set; }
        }
    }
}
