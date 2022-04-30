let services = [];
let connection = null;
getdata();
setupSignalR();
let serviceIdToUpdate = -1;

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:37793/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("ServiceCreated", (user, message) => {
        getdata();
    });

    connection.on("ServiceDeleted", (user, message) => {
        getdata();
    });
    connection.on("ServiceUpdated", (user, message) => {
        getdata();
    });

    connection.onclose(async () => {
        await start();
    });
    start();
}
async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};
async function getdata() {
    await fetch('http://localhost:37793/Services')
        .then(x => x.json())
        .then(y => {
            services = y;
            display();
        });
}
function display() {
    document.getElementById('resultarea').innerHTML = "";
    services.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.id + "</td><td>"
            + t.name + "</td><td>" + t.rating + "</td><td>" + t.price + " $</td><td>" +
            `<button type="button" onclick="remove(${t.id})">Delete</button>` +
            `<button type="button" onclick="showupdate(${t.id})">Update Cost</button>`
            + "</td></tr>";

    });
    document.getElementById('servicename').value = "";
    document.getElementById('servicerating').value = 1;
    document.getElementById('servicecost').value = "";
}
function showupdate(id) {
    document.getElementById('servicecostToUpdate').value = services.find(t => t['id'] == id)['price'];
    document.getElementById('updateformdiv').style.display = 'flex';
    serviceIdToUpdate = id;
}
function remove(id) {
    fetch('http://localhost:37793/Services/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}
function create() {
    let Servicename = document.getElementById('servicename').value;
    let Servicerating = document.getElementById('servicerating').value ;
    let Servicecost = document.getElementById('servicecost').value;

    fetch('http://localhost:37793/Services', {
        method: 'POST',
        headers: {
            'Accept': 'application/json, text/plain, */*',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(
            { Name: Servicename, Rating: Servicerating, Price: Servicecost })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}
function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let ServicecostToUpd = document.getElementById('servicecostToUpdate').value;
    let Servicename = reservations.find(t => t['id'] == serviceIdToUpdate)['name'];
    let Servicerating = reservations.find(t => t['id'] == serviceIdToUpdate)['rating'];
    fetch('http://localhost:37793/services', {
        method: 'PUT',
        headers: {
            'Accept': 'application/json, text/plain, */*',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(
            { Name: Servicename, Rating: Servicerating, Price: ServicecostToUpd, Id: serviceIdToUpdate })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}