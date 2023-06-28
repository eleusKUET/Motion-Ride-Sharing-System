function getLocation(callback) {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(callback);
    }
    else {
        alert("Your location is needed for this route");
    }
}

function showPosition(position) {
    // position.coords.latitude;
    // position.coords.longitude;

}