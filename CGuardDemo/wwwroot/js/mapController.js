var map;
var geoJsonLayer = null;

function initializeMap() {
    map = L.map('map').setView([59.23, 4.51], 12);

    L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
    }).addTo(map);
}

function addGeoJsonToMap(geoJson, isObject) {
    if (geoJsonLayer != null)
        map.removeLayer(geoJsonLayer);

    let object;
    if (isObject == true) {
        object = geoJson;
    } else {
        let formatted = geoJson.replace(/(&quot\;)/g, "\"");
        object = JSON.parse(formatted);
    }

    var geojsonMarkerOptions = {
        radius: 5,
        fillColor: "red",
        color: "red",
        weight: 1,
        opacity: 1,
        fillOpacity: 0.8
    };

    var windTurbine = L.icon({
        iconUrl: 'windTurbine.png',
        iconSize: [140, 140],
        iconAnchor: [60, 114]
    });

    geoJsonLayer = L.geoJSON(object, {
        style: function (feature) {
            if (feature.geometry.type == 'MultiLineString')
                return { color: '#f2737d' };
            return { color: feature.properties.color };
        },
        pointToLayer: function (feature, latlng) {
            //return L.marker(latlng, { icon: windTurbine });
            return L.circleMarker(latlng, geojsonMarkerOptions);
        }
    });
    map.addLayer(geoJsonLayer);
}