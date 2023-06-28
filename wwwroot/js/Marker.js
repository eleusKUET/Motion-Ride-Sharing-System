function addMarker(lat, lon) {
    return L.marker(new L.LatLng(lat, lon)).addTo(map);
}

function indicateThisLocation(lat, lon) {
    map.panTo(new L.LatLng(lat, lon));
}