function geocodeAddress(givenAddress) {
    const address = givenAddress;
    if (!address || address.length < 3) {
        console.log(givenAddress);
        return;
    }
    let lat = 0;
    let lon = 0;
    const myAPIKEY = "7443e3deffdb496495140c1ab29fd2ab";
    const geocodingUrl = `https://api.geoapify.com/v1/geocode/search?text=${encodeURIComponent(address)}&apiKey=${myAPIKey}`;

    // call Geocoding API - https://www.geoapify.com/geocoding-api
    fetch(geocodingUrl).then(result => result.json())
        .then(featureCollection => {
            if (featureCollection.features.length === 0) {
                document.getElementById("status").textContent = "The address is not found";
                return;
            }

            const foundAddress = featureCollection.features[0];
            lat = foundAddress.properties.lat;
            lon = foundAddress.properties.lon;
        });
    return [lat, lon];
}
