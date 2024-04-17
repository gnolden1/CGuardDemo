using System.Text.Json;

namespace CGuardDemo {
    public class GeoJsonController {
        private static readonly HttpClient _client = new();

        // DELETE LATER
        public static (DateTime, DateTime) GetEarliestAndLatest()
        {
            ZonesDeserializable? allZones = ReadZones();
            if (allZones == null)
                throw new Exception("Error reading zones");

            var sortedTimeseries = allZones.timeseries.OrderBy(x => x.timestamp);
            DateTime earliest = sortedTimeseries.First().timestamp;
            DateTime latest = sortedTimeseries.Last().timestamp;

            return (earliest, latest);
        }

        // FIX!!!
        public static async Task<(DateTime, DateTime)> GetEarliestAndLatestAsync(CancellationToken cancellationToken)
        {
            // FIX THIS
            StringContent content = new("INITIAL");
            using HttpResponseMessage response = await _client.PostAsync("http://localhost:8080/", content, cancellationToken);
            string body = await response.Content.ReadAsStringAsync(cancellationToken);
            string[] earliestLatest = body.Split(';');
            return (DateTime.Parse(earliestLatest[0]), DateTime.Parse(earliestLatest[1]));
            // Check for errors
        }

        // DELETE LATER
        public static Timesery[] GetZoneTimeseries(DateTime earliest, DateTime latest/*, TimeSpan offset*/)
        {
            ZonesDeserializable? allZones = ReadZones();
            if (allZones == null)
                throw new Exception("Error reading zones");

            List<Timesery> relevantZones = new();
            foreach (var item in allZones.timeseries)
            {
                if (item.timestamp >= earliest && item.timestamp <= latest)
                    relevantZones.Add(item);
            }

            return relevantZones.ToArray();
        }

        // FIX!!!
        public static async Task<Timesery[]> GetZoneTimeseriesAsync(DateTime earliest, DateTime latest, TimeSpan offset, CancellationToken cancellationToken)
        {
            StringContent content = new($"{earliest};{latest};{offset}");
            using HttpResponseMessage response = await _client.PostAsync("http://localhost:8080/", content, cancellationToken);
            string body = await response.Content.ReadAsStringAsync(cancellationToken);
            ZonesDeserializable? zones = JsonSerializer.Deserialize<ZonesDeserializable>(body);
            if (zones == null)
                throw new Exception("Error reading zones");

            return zones.timeseries;
            // Check for errors
        }

        // DELETE LATER
        private static ZonesDeserializable? ReadZones()
        {
            return JsonSerializer.Deserialize<ZonesDeserializable>(File.ReadAllText("TestFiles/testdata.json"));
        }
    }
}
