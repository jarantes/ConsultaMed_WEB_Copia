﻿//ocultar mensagem de sucesso e erro
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


function adicionaExame() {
    jQuery("#examesEncontrados :selected").each(function (i, selected) {
        jQuery("#examesSelecionados").append(selected);
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

function removeExame() {
    jQuery("#examesSelecionados :selected").each(function (i, selected) {
        jQuery("#examesEncontrados").append(selected);
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

function adicionaEspecialidade() {
    jQuery("#especialidadesEncontradas :selected").each(function (i, selected) {
        jQuery("#especialidadesSelecionadas").append(selected);
    });
}

function removeEspecialidade() {
    jQuery("#especialidadesSelecionadas :selected").each(function (i, selected) {
        jQuery("#especialidadesEncontradas").append(selected);
    });
}

function carregaListas2() {
    var especialidadeId = new Array;
    var exameId = new Array;
    jQuery("#examesSelecionados :selected").each(function (i, selected) {
        exameId[i] = selected.value;
    });
    jQuery("#especialidadesSelecionadas :selected").each(function (i, selected) {
        especialidadeId[i] = selected.value;
    });

    jQuery.ajaxSettings.traditional = true;

    $.post('../Especialidade/SalvarExame', { 'especialidadeId': especialidadeId, 'exameId': exameId });

    window.location.reload();
}

function carregaAgenda() {
    $.ajax({
        url: '../Consulta/CarregaAgenda',
        data: { agendaData: jQuery(".textData").val(), medicoUserId: jQuery("#MedicosEncontrados :selected").val() },
        dataType: "json",
        beforeSend: function () {
            $("#loading").fadeIn();
        },
        complete: function () {
            $("#loading").hide();
        },

        success: function (response) {
            jQuery("#AgendaEncontrada").empty();
            if (response.data[0] == null) {
                $('#dialog').modal({
                    keyboard: false
                });
                return;
            }
            $('#AgendaEncontrada').append("<option value=''>" + '[Selecione]' + "</option>");
            $.each(response.data, function (index, agenda) {
                if (agenda != null)
                    $('#AgendaEncontrada').append("<option value='" + agenda + "'>" + agenda + "</option>");
            });
        }
    });
}

function carregaConsultas() {
    $.ajax({
        url: '../Consulta/Gerenciar2',
        data: { id: jQuery("#listaMedicos :selected").val() },
    });
    $('#tabela').load();
}

function carregaMédico() {
    if (jQuery("#Especialidade").val() == "") {
        $('#MedicosEncontrados').empty();
        $("#escondido").slideUp();
        return;
    }
    jQuery.ajax({
        url: '../Usuario/CarregaMedico',
        data: 'Id=' + jQuery("#Especialidade").val(),
        dataType: "json",
        beforeSend: function () {
            $("#loadingMedico").fadeIn();
        },
        complete: function () {
            $("#loadingMedico").hide();
        },

        success: function (data) {
            jQuery("#MedicosEncontrados").empty();
            if (data.Result[0] == null) {
                $('#dialogMedico').modal({
                    keyboard: false
                });
                return;
            }

            $('#MedicosEncontrados').append("<option value=''>" + '[Selecione]' + "</option>");
            $.each(data.Result, function (index, medico) {

                $('#MedicosEncontrados').append("<option value='" + medico.UserId + "'>" + medico.Nome + " " + medico.Sobrenome + "</option>");
            });
        }
    });
}

function carregaEspecialidade() {
    if (jQuery("#Clinica").val() == "") {
        $('#Especialidade').empty();
        $('#MedicosEncontrados').empty();
        $("#escondido").slideUp();
        return;
    }
    jQuery.ajax({
        url: '../Especialidade/CarregaEspecialidade',
        data: 'Id=' + jQuery("#Clinica").val(),
        dataType: "json",
        beforeSend: function () {
            $("#loadingEspec").fadeIn();
        },
        complete: function () {
            $("#loadingEspec").hide();
        },

        success: function (data) {
            jQuery("#Especialidade").empty();
            if (data.Result[0] == null) {
                $('#dialogEspec').modal({
                    keyboard: false
                });
                return;
            }

            $('#Especialidade').append("<option value=''>" + '[Selecione]' + "</option>");
            $.each(data.Result, function (index, espec) {

                $('#Especialidade').append("<option value='" + espec.EspecialidadeId + "'>" + espec.Descricao + "</option>");
            });
        }
    });
}

function SessionaMedicoId() {
    $("#escondido").slideDown();
}

function LimparComboHorario() {
    jQuery("#AgendaEncontrada").empty();
}
