async function findRoute(fid, tid, fromlat, fromlon, tolat, tolon, vehicle_type) {
    const myAPIKey = "7443e3deffdb496495140c1ab29fd2ab";
    const fromWaypoint = [fromlat, fromlon]; // latitude, longitude
    const toWaypoint = [tolat, tolon]; // latitude, longitude
    const url = `https://api.geoapify.com/v1/routing?waypoints=${fromWaypoint.join(',')}|${toWaypoint.join(',')}&mode=drive&details=instruction_details&apiKey=${myAPIKey}`;

    try {
        const res = await fetch(url);
        const result = await res.json();
        const foundRoute = result.features[0];
        const routeData = [
            foundRoute.properties.legs[0].steps[0].distance,
            foundRoute.properties.legs[0].steps[0].time
        ];
        console.log(routeData);
        document.getElementById(fid).innerHTML = "BDT " + String((Math.floor(routeData[0]) * 0.05).toFixed(1));
        document.getElementById(tid).innerHTML = String((Math.floor(routeData[0]) / 60).toFixed(1)) + " min away";
        return routeData;
    } catch (error) {
        console.error(error);
        throw error;
    }
}

async function calTime(tid, fromlat, fromlon, tolat, tolon) {
    const myAPIKey = "7443e3deffdb496495140c1ab29fd2ab";
    const fromWaypoint = [fromlat, fromlon]; // latitude, longitude
    const toWaypoint = [tolat, tolon]; // latitude, longitude
    const url = `https://api.geoapify.com/v1/routing?waypoints=${fromWaypoint.join(',')}|${toWaypoint.join(',')}&mode=drive&details=instruction_details&apiKey=${myAPIKey}`;

    try {
        const res = await fetch(url);
        const result = await res.json();
        const foundRoute = result.features[0];
        const routeData = [
            foundRoute.properties.distance,
            foundRoute.properties.time
        ];
        console.log("result.feature[0] caltime");
        console.log(result);
        console.log(routeData);
        document.getElementById(tid).innerHTML = String((Math.floor(routeData[1]) / 60).toFixed(0)) + " min away";
        return routeData;
    } catch (error) {
        console.error(error);
        throw error;
    }
}

async function calFare(fid, fromlat, fromlon, tolat, tolon, vehicle_type) {
    const myAPIKey = "7443e3deffdb496495140c1ab29fd2ab";
    const fromWaypoint = [fromlat, fromlon]; // latitude, longitude
    const toWaypoint = [tolat, tolon]; // latitude, longitude
    const url = `https://api.geoapify.com/v1/routing?waypoints=${fromWaypoint.join(',')}|${toWaypoint.join(',')}&mode=drive&apiKey=${myAPIKey}`;

    try {
        const res = await fetch(url);
        const result = await res.json();
        
        const foundRoute = result.features[0];
        const routeData = [
            foundRoute.properties.distance,
            foundRoute.properties.time
        ];
        console.log("result.feature[0] calfare");
        console.log(result.features[0].properties.distance);
        console.log(result.features[0].properties.time);
        console.log(routeData);
        let rate = 0;
        if (vehicle_type == "carl" || vehicle_type == "big_car") {
            rate = 0.02;
        }
        else if (vehicle_type == "car") rate = 0.008;
        else if (vehicle_type == "cng") rate = 0.007;
        else rate = 0.006;
        console.log("rate");
        console.log(rate);
        console.log("vehicle");
        console.log(vehicle_type);
        document.getElementById(fid).innerHTML = "BDT " + String((Math.floor(routeData[0]) * rate).toFixed(1));
        return routeData;
    } catch (error) {
        console.error(error);
        throw error;
    }
}

function getLastInstruction(fromlat, fromlon, tolat, tolon, vehicle_type) {
    if (!vehicle_type) {
        console.log(vehicle);
        return;
    }
    const url = "https://api.geoapify.com/v1/routing?waypoints=" + toString(fromlat) + "," + toString(fromlon) + "|" + toString(tolat) + "," + toString(tolon) + "&mode=" + vehicle_type + "&apiKey=7443e3deffdb496495140c1ab29fd2ab";
    console.log(url);
    let routeData = []
    fetch(url)
        .then(response => response.json())
        .then(result => {
            console.log(result);
            if (result.features.length == 0) {
                return;
            }
            const foundRoute = result.features[0];
            routeData.push(foundRoute.properties.legs[0].steps[0].distance);
            routeData.push(foundRoute.properties.legs[0].steps[0].time);
            routeData.push(foundRoute.properties.legs[0].steps[0].instruction.text);
        });
    return routeData;
}

function showRoute(from, To) {
    L.Routing.control({
        waypoints: [
            L.latLng(from[0], from[1]),
            L.latLng(To[0], To[1])
        ],
        routeWhileDragging: true
    }).addTo(map);
}

/*$scope.routingControl = L.Routing.control({
    waypoints: [
        L.latLng(fromLat, fromLng),
        L.latLng(toLat, toLng)
    ]
}).addTo(map);
$scope.removeRouting = function () {
    leafletData.getMap().then(function (map) {
        map.removeControl($scope.routingControl);
    });
};*/

