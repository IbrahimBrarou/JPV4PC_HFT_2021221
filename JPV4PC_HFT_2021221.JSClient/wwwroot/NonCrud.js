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
function ArtistEarnings() {

}
function Mostpaid() {

}
function Lesspaid() {

}
function display() {
    if (deciding ==="BF") {
        document.getElementById('headresult').innerHTML += "<tr><th>Fan Id</th><th>Number of reservations</th></tr> " ;
        document.getElementById('resultarea').innerHTML = "";
        bestfan.forEach(t => {
            document.getElementById('resultarea').innerHTML += "<tr><td>" + t.key + "</td><td>" + t.value + "</td></tr>";
        });
    }
    if (deciding === "WF") {
        document.getElementById('headresult').innerHTML += "<tr><th>Fan Id</th><th>Number of reservations</th></tr> ";
        document.getElementById('resultarea').innerHTML = "";
        worstfan.forEach(t => {
            document.getElementById('resultarea').innerHTML += "<tr><td>" + t.key + "</td><td>" + t.value + "</td></tr>";
        });
    }
    
}