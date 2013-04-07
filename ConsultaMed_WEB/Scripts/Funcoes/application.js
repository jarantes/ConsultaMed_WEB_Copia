//ocultar mensagem de sucesso e erro
$(function () {
    $("#fecharSuc")
        .click(function () {
            $("#mensagemSuc").hide("slow");
        });

    $("#fecharErr")
        .click(function () {
            $("#mensagemErr").hide("slow");
        });

    $("a.btnRemove")
        .click(function () {
            var id = $(this).data("id");
            var name = $(this).data("name");
            $("#user").html(name);
            $("#btnConfirm").attr("href", $("a.btnRemove").data("url") + id);
        });
});

function adicionaClinica() {
    jQuery("#clinicasEncontradas :selected").each(function (i, selected) {
        jQuery("#clinicasSelecionadas").append(selected);
    });
}

function adicionaConvenio() {
    jQuery("#conveniosEncontrados :selected").each(function (i, selected) {
        jQuery("#conveniosSelecionados").append(selected);
    });
}

function removeClinica() {
    jQuery("#clinicasSelecionadas :selected").each(function (i, selected) {
        jQuery("#clinicasEncontradas").append(selected);
    });
}

function removeConvenio() {
    jQuery("#conveniosSelecionados :selected").each(function (i, selected) {
        jQuery("#conveniosEncontrados").append(selected);
    });
}

function carregaListas() {
    var clinicaId = new Array;
    var convenioId = new Array;
    jQuery("#conveniosSelecionados :selected").each(function (i, selected) {
        convenioId[i] = selected.value;
    });
    jQuery("#clinicasSelecionadas :selected").each(function (i, selected) {
        clinicaId[i] = selected.value;
    });

    jQuery.ajaxSettings.traditional = true;

    $.post('../Clinica/SalvarConvenio', { 'clinicaId': clinicaId, 'convenioId': convenioId });

    window.location.reload();
}


//TODO mais parâmetros depois...        
function carregaAgenda() {
    jQuery.ajax({
        url: '../Consulta/CarregaAgenda',
        data: 'agendaData=' + jQuery(".textData").val(),
        dataType: "json",
        beforeSend: function () {
            $("#loading").fadeIn();
        },
        complete: function () {
            $("#loading").hide();
        },

        success: function (response) {
            var options = '';
            jQuery("#AgendaEncontrada").empty();
            if (response.data[0] == null) {
                $('#dialog').modal({
                    keyboard: false
                });
                return;
            }

            for (var i = 0; i < response.data.length; i++) {
                if (response.data[i] != null) {
                    options += '<option value="' + response.data[i].toString() + '">' + response.data[i].toString() + '</option>';
                }
            }
            jQuery("#AgendaEncontrada").html(options);
        }
    });
}

function LimparComboHorario() {
    jQuery("#AgendaEncontrada").empty();
}
