// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


// EVENTI 
function ButtonClick(Ithis) {
    const dataDiv = $(Ithis).attr("data-div");
    const idVillage = $("#" + dataDiv + " #idVillage").val();
    const nome = $("#" + dataDiv + " #nome").val(); // trovare om prendere id utente da menu
    const luogo = $("#" + dataDiv + " #luogo").val();
    const posti = $("#" + dataDiv + " #posti").val();

    if (posti == "" || posti < 1) {
        alert("Non è possibile procedere con la prenotazione per mancanza di dati o dati non corretti nella registrazione.");
        return;
    };

    // creo il json con i dati
    const body = {};
    body.idVillage = idVillage;
    body.Nome = nome;
    body.Luogo = luogo;
    body.Posti = posti;

    $.ajax({
        method: "POST",
        url: "/api/Prenota/InsertPrenotazione",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(body),
        dataType: "json",
        success: function (data, status) {
            console.log("body ", body);
            console.log("data ", data);
            console.log("status ", status);

            if (data.result === "Record inserito") {
                $("#posti").val("");
                alert(data.result);
            } else {
                alert("Errore: prenotazione non accetttata: \n" + data.result);
            };
            this.always();
        },
        error: function (error, status) {
            console.log("body ", body);
            console.log("error ", error);
            console.log("status ", status);

            //console.log("responseText ", error.responseText); // proprietà di obj error

            this.always();
        },
        always: function () { }
    });
}