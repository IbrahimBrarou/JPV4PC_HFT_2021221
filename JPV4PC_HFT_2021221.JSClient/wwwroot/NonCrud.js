let artistsearnings = [];
let mostpaidartist = [];
let lesspaidartist = [];
let bestfan = [];
let worstfan = [];
let deciding = null;



async function BestFan() {
    document.getElementById('resultarea').innerHTML = "";
    document.getElementById('headresult').innerHTML = "";
    await fetch('http://localhost:37793/Noncrudfan/BestFans')
        .then(x => x.json())
        .then(y => {
            bestfan = y;
            deciding = "BF";
            display(deciding);
        });
}
async function WorstFan() {
    document.getElementById('resultarea').innerHTML = "";
    document.getElementById('headresult').innerHTML = "";
    await fetch('http://localhost:37793/Noncrudfan/WorstFans')
        .then(x => x.json())
        .then(y => {
            worstfan = y;
            deciding = "WF";
            display(deciding);
        });
}
async function ArtistEarnings() {
    document.getElementById('resultarea').innerHTML = "";
    document.getElementById('headresult').innerHTML = "";
    await fetch('http://localhost:37793/Noncrudartist/ArtistsEarnings')
        .then(x => x.json())
        .then(y => {
            artistsearnings = y;
            deciding = "AE";
            display(deciding);
        });
}
async function Mostpaid() {
    document.getElementById('resultarea').innerHTML = "";
    document.getElementById('headresult').innerHTML = "";
    await fetch('http://localhost:37793/Noncrudartist/Mostpaidart')
        .then(x => x.json())
        .then(y => {
            mostpaidartist = y;
            deciding = "MP";
            display(deciding);
        });
}
async function Lesspaid() {
    document.getElementById('resultarea').innerHTML = "";
    document.getElementById('headresult').innerHTML = "";
    await fetch('http://localhost:37793/Noncrudartist/Lesspaidart')
        .then(x => x.json())
        .then(y => {
            lesspaidartist = y;
            deciding = "LP";
            display(deciding);
        });
}
function display() {
    if (deciding ==="BF") {
        document.getElementById('headresult').innerHTML += "<tr><th>Fan Id</th><th>Number of reservations</th></tr> " ;
        document.getElementById('resultarea').innerHTML = "";
        bestfan.forEach(t => {
            document.getElementById('resultarea').innerHTML += "<tr><td>" + t.key + "</td><td>" + t.value + "</td></tr>";
        });
    }
    else if (deciding === "WF") {
        document.getElementById('headresult').innerHTML += "<tr><th>Fan Id</th><th>Number of reservations</th></tr> ";
        document.getElementById('resultarea').innerHTML = "";
        worstfan.forEach(t => {
            document.getElementById('resultarea').innerHTML += "<tr><td>" + t.key + "</td><td>" + t.value + "</td></tr>";
        });
    }
    else if (deciding === "AE") {
        document.getElementById('headresult').innerHTML += "<tr><th>Artist Name</th><th>Overall Earnings</th></tr> ";
        document.getElementById('resultarea').innerHTML = "";
        artistsearnings.forEach(t => {
            document.getElementById('resultarea').innerHTML += "<tr><td>" + t.key + "</td><td>" + t.value + " $</td></tr>";
        });
    }
    else if (deciding === "MP") {
        document.getElementById('headresult').innerHTML += "<tr><th>Artist Name</th><th>Overall Earnings</th></tr> ";
        document.getElementById('resultarea').innerHTML = "";
        mostpaidartist.forEach(t => {
            document.getElementById('resultarea').innerHTML += "<tr><td>" + t.key + "</td><td>" + t.value + " $</td></tr>";
        });
    }
    else if (deciding === "LP") {
        document.getElementById('headresult').innerHTML += "<tr><th>Artist Name</th><th>Overall Earnings</th></tr> ";
        document.getElementById('resultarea').innerHTML = "";
        lesspaidartist.forEach(t => {
            document.getElementById('resultarea').innerHTML += "<tr><td>" + t.key + "</td><td>" + t.value + " $</td></tr>";
        });
    }
    
}