function reverseGeoCode(elatlon) {
    const myAPIKEY = "7443e3deffdb496495140c1ab29fd2ab";
    const reverseGeocodingUrl = `https://api.geoapify.com/v1/geocode/reverse?lat=${elatlon[0]}&lon=${elatlon[1]}&apiKey=${myAPIKey}`;
    let address = []
    // call Reverse Geocoding API - https://www.geoapify.com/reverse-geocoding-api/
    fetch(reverseGeocodingUrl).then(result => result.json())
        .then(featureCollection => {
            if (featureCollection.features.length === 0) {
                document.getElementById("status").textContent = "The address is not found";
                return;
            }

            const foundAddress = featureCollection.features[0];
            address.push(foundAddress.properties.name);
            address.push(foundAddress.properties.housenumber);
            address.push(foundAddress.properties.street);
            address.push(foundAddress.properties.postcode);
            address.push(foundAddress.properties.city);
            address.push(foundAddress.properties.state);
            address.push(foundAddress.properties.country);
        });

    return address;
}
