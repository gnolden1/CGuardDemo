var map;
var geoJsonLayer = null;

function initializeMap() {
    map = L.map('map').setView([59.43217, 10.53117], 12);

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

    geoJsonLayer = L.geoJSON(object);
    map.addLayer(geoJsonLayer);
}