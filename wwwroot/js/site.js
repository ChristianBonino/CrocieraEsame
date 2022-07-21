
function getTabellaPrenotazioni() {
    $.ajax({
        method: "GET",
        url: "/api/Person/GetAllPrenotazioni",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, status) {
            console.log(data);

            let tableData = `
                             <table id="Persons" class="table table-striped table-bordered" style="background-color: azure;">
                                <thead>
                                    <tr>
                                        <th>Nome</th>
                                        <th>Cognome</th>
                                        <th>ID Univoco</th>
                                        <th>Delete</th>
                                        <th>Edit</th>
                                    </tr>
                                </thead>
                                <tbody>`;

            for (var i = 0; i < data.length; i++) {
                tableData += `<tr>
                                <td> ${data[i].nome} </td>
                                <td> ${data[i].cognome} </td>
                                <td> ${data[i].id} </td>
                                <td id="${data[i].id}" onclick="deletePerson(this)">
                                    <i class="fa fa-trash text-primary" aria-hidden="true"></i> 
                                <td id="${data[i].id}" onclick="showModalForEditPerson(this)">
                                    <i class="fa fa-edit text-primary" aria-hidden="true"></i> </td>
                              </tr>`;
            }
            tableData += `<tbody>`;
            $("#myPrenota").append(tableData);
            $("button").hide();
            this.always();
        },
        error: function (error, status) {
            console.log(error);
            console.log(status);
            this.always();
        },
        always: function () { }
    });
};







































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