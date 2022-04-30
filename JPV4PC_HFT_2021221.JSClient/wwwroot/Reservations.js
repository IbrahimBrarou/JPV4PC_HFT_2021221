let reservations = [];
let connection = null;
getdata();
setupSignalR();
let reservationIdToUpdate = -1;

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:37793/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("ReservationCreated", (user, message) => {
        getdata();
    });

    connection.on("ReservationDeleted", (user, message) => {
        getdata();
    });
    connection.on("ReservationUpdated", (user, message) => {
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
    await fetch('http://localhost:37793/Reservations')
        .then(x => x.json())
        .then(y => {
            reservations = y;
            display();
        });
}
function display() {
    document.getElementById('resultarea').innerHTML = "";
    reservations.forEach(t =>
    {
        if (t.artistId != null && t.fanId != null) {
            document.getElementById('resultarea').innerHTML +=
                "<tr><td>" + t.id + "</td><td>"
                + t.fanId + "</td><td>" + t.artistId + "</td><td>" + t.dateTime + "</td><td>" +
                `<button type="button" onclick="remove(${t.id})">Delete</button>` +
                `<button type="button" onclick="showupdate(${t.id})">Update Date</button>`
                + "</td></tr>";
        }
        
    });
    document.getElementById('reservationfanid').value = "";
    document.getElementById('reservationartistid').value = "";
    document.getElementById('reservationdate').value = "";
}
function showupdate(id) {
    document.getElementById('reservationdateToUpdate').value = reservations.find(t => t['id'] == id)['dateTime'];
    document.getElementById('updateformdiv').style.display = 'flex';
    reservationIdToUpdate = id;
}
function remove(id) {
    fetch('http://localhost:37793/reservations/' + id, {
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
    let Reserfanid = document.getElementById('reservationfanid').value;
    let Reserartistid = document.getElementById('reservationartistid').value;
    let Reserdate = document.getElementById('reservationdate').value;

    fetch('http://localhost:37793/reservations', {
        method: 'POST',
        headers: {
            'Accept': 'application/json, text/plain, */*',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(
            { FanId: Reserfanid, ArtistId: Reserartistid, DateTime: Reserdate })
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
    let ReserdateToUpd = document.getElementById('reservationdateToUpdate').value;
    let Reserfanid = reservations.find(t => t['id'] == reservationIdToUpdate)['fanid'];
    let Reserartistid = reservations.find(t => t['id'] == reservationIdToUpdate)['artistid'];
    fetch('http://localhost:37793/Reservations', {
        method: 'PUT',
        headers: {
            'Accept': 'application/json, text/plain, */*',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(
            { FanId: Reserfanid, ArtistId: Reserartistid, DateTime: ReserdateToUpd ,Id:reservationIdToUpdate})
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}