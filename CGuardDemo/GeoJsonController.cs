using System.Text.Json;

namespace CGuardDemo {
    public class GeoJsonController {
        // REWRITE LATER
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

        // REWRITE LATER
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

        // DELETE LATER
        private static ZonesDeserializable? ReadZones()
        {
            return JsonSerializer.Deserialize<ZonesDeserializable>(File.ReadAllText("TestFiles/testdata.json"));
        }
    }
}
