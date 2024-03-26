namespace CGuardDemo;
public class ZonesDeserializable {
    public Timesery[] timeseries { get; set; }
}

public class Timesery {
    public DateTime timestamp { get; set; }
    public string geojson { get; set; }
}
