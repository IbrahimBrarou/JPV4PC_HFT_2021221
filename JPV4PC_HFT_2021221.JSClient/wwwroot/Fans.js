let fans = [];
let connection = null;
getdata();
setupSignalR();
let fanIdToUpdate = -1;

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:37793/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("FanCreated", (user, message) => {
        getdata();
    });

    connection.on("FanDeleted", (user, message) => {
        getdata();
    });
    connection.on("FanUpdated", (user, message) => {
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
    await fetch('http://localhost:37793/fans')
        .then(x => x.json())
        .then(y => {
            fans = y;
            display();
        });
}
function display() {
    document.getElementById('resultarea').innerHTML = "";
    fans.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.id + "</td><td>"
            + t.name + "</td><td>" + t.city + "</td><td>" + t.email + "</td><td>" +
            `<button type="button" onclick="remove(${t.id})">Delete</button>` +
            `<button type="button" onclick="showupdate(${t.id})">Update City</button>`
            + "</td></tr>";
    });
    document.getElementById('fanname').value = "";
    document.getElementById('fancity').value = "";
    document.getElementById('fanemail').value = "";
}
function showupdate(id) {
    document.getElementById('fancityToUpdate').value = fans.find(t => t['id'] == id)['city'];
    document.getElementById('updateformdiv').style.display = 'flex';
    fanIdToUpdate = id;
}
function remove(id) {
    fetch('http://localhost:37793/fans/' + id, {
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
    let Fanname = document.getElementById('fanname').value;
    let Fancity = document.getElementById('fancity').value;
    let Fanemail = document.getElementById('fanemail').value;

    fetch('http://localhost:37793/fans', {
        method: 'POST',
        headers: {
            'Accept': 'application/json, text/plain, */*',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(
            { City: Fancity, Name: Fanname, Email: Fanemail })
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
    let FancityToUpd = document.getElementById('fancityToUpdate').value;
    let Fanemail = fans.find(t => t['id'] == fanIdToUpdate)['email'];
    let Fanname = fans.find(t => t['id'] == fanIdToUpdate)['name'];
    fetch('http://localhost:37793/fans', {
        method: 'PUT',
        headers: {
            'Accept': 'application/json, text/plain, */*',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(
            { Name: Fanname, Email: Fanemail, City: FancityToUpd, Id: fanIdToUpdate })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}
