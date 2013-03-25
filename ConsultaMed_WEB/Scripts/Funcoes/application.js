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
    //$.ajax({
    //    url: "../Administrador/SaveCliConv",
    //    type: "POST",
    //    traditional: true,
    //    data: {
    //        "clinicaId": $.param({ clinicaId: clinicaId }),
    //        "convenioId": $.param({ convenioId: convenioId })
    //    },

}







