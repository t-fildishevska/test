let apiData;

let xhr = new XMLHttpRequest();
xhr.open("GET", "https://reqres.in/api/users", true);
xhr.onload = function(){
    let obj = JSON.parse(xhr.responseText);
    apiData = obj.data;
    addData(); 
};
xhr.send();

function addData(){
    for(let item of apiData){
      $("#table").append(
        `<tr>
            <td>${item.first_name}</td>
            <td>${item.last_name}</td>
            <td>${item.email}</td>
        </tr>`
      )
    }
}

let name = document.getElementById("name");
let surname = document.getElementById("surname");
let email = document.getElementById("email");

$("#add").click(function(){
    $("#table").append(
        `<tr>
            <td>${name.value}</td>
            <td>${surname.value}</td>
            <td>${email.value}</td>
        </tr>`
    )
})
